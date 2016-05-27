using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public LoginViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var user = User as ClaimsPrincipal;
            if (user?.Identities != null && user.Identities.Count() > 0 && user.Identity.IsAuthenticated)
                return View("LoggedIn");
            return View();
        }
    }
}
