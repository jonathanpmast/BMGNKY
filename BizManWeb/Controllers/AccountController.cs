using BizManWeb.Data;
using BizManWeb.Models;
using BizManWeb.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private IHostingEnvironment _hostingEnvironment;
        private BMGContext _context;
        public AccountController(
            ILoggerFactory loggerFactory,
            IHostingEnvironment hostingEnvironment,
            BMGContext bmgContext)
        {
            _logger = loggerFactory.CreateLogger<AccountController>();
            _hostingEnvironment = hostingEnvironment;
            _context = bmgContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = new AuthenticationProperties() { RedirectUri = redirectUrl };
            properties.Items["LoginProvider"] = provider;
            return new ChallengeResult(provider, properties);
        }

        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var login = _context.Logins.FirstOrDefault(l => l.Identifier == User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (login != null)
            {
                await AuthenticateLoginAsync(login);

                return Redirect(returnUrl ?? new PathString("/"));
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = User.Identity.AuthenticationType;

                var suspectedGolfer = _context.Golfers.FirstOrDefault(g => g.Name == User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
                var name = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                var email = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

                if (suspectedGolfer != null)
                {
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel
                    {
                        Name = name,
                        Email = email,
                        SuspectedGolfer = suspectedGolfer
                    });
                }
                else
                {
                    var newLogin = await CreateLogin(identifier: User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                        email: email,
                        loginType: User.Identity.AuthenticationType == "Google" ? LoginType.Google : LoginType.Facebook);
                    await AuthenticateLoginAsync(newLogin);
                    return Redirect(returnUrl ?? new PathString("/"));
                }

            }
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel viewModel, string ConfirmGolfer)
        {
            if (ConfirmGolfer.Equals("No", StringComparison.OrdinalIgnoreCase))
            {

                return LocalRedirect("/");
            }
            if (User.Identity.IsAuthenticated && User.HasClaim(c => c.Type == Constants.ClaimTypes.GolferId))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var login = await CreateLogin(
                identifier: User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                loginType: User.Identity.AuthenticationType == "Google" ? LoginType.Google : LoginType.Facebook,
                email: User.Claims.First(c => c.Type == ClaimTypes.Email).Value,
                golfer: viewModel.SuspectedGolfer);
            // external login
            _logger.LogInformation(6, "User created an account using {Name} provider.", User.Identity.AuthenticationType);

            await AuthenticateLoginAsync(login);

            return LocalRedirect(ViewData["ResturnUrl"].ToString());
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(Constants.CookieMiddleWareIdentifier);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task AuthenticateLoginAsync(Login login)
        {
            ClaimsIdentity ci = new ClaimsIdentity(User.Identity.AuthenticationType, ClaimTypes.Name, ClaimTypes.Role);
            ci.AddClaim(User.Claims.First(c => c.Type == ClaimTypes.Name));
            ci.AddClaim(User.Claims.First(c => c.Type == ClaimTypes.Email));
            ci.AddClaim(User.Claims.First(c => c.Type == ClaimTypes.GivenName));
            ci.AddClaim(User.Claims.First(c => c.Type == ClaimTypes.Surname));
            ci.AddClaim(new Claim(Constants.ClaimTypes.LoginId, login.ID.ToString()));
            if (login.Golfer != null)
                ci.AddClaim(new Claim(Constants.ClaimTypes.GolferId, login.Golfer.ID.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, User.Identity.AuthenticationType));

            //TODO: Make this less dumb
            if (login.Email == @"jonathanpmast@gmail.com")
                ci.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));

            await HttpContext.Authentication.SignInAsync(Constants.CookieMiddleWareIdentifier, new ClaimsPrincipal(ci));
        }

        private async Task<Login> CreateLogin(string identifier, string email, LoginType loginType, Golfer golfer = null)
        {
            var newLogin = new Login()
            {
                Identifier = identifier,
                Email = email,
                Golfer = golfer,
                LoginType = loginType
            };
            _context.Logins.Add(newLogin);
            await _context.SaveChangesAsync();
            var login = _context.Logins.Single(l => l.Identifier == identifier);
            return login;
        }
    }
}
