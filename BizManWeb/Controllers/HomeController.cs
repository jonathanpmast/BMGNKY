using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BizManWeb.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
namespace BizManWeb.Controllers
{
    public class HomeController : Controller
    {
        BMGContext _context;
        public HomeController(BMGContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var vm = new ViewModels.Home.HomeIndexViewModel();
            vm.CurrentRound = _context.Rounds
                                .OrderBy(r => r.Order)
                                .Include(r => r.Matches)
                                .ThenInclude(m => m.Teams)
                                .ThenInclude(t => t.Team)
                                .ThenInclude(t => t.Golfers)
                                .FirstOrDefault(r => r.Date == DateTime.MinValue);
            vm.LastRound = _context.Rounds
                                .OrderBy(r => r.Order)
                                .Include(r => r.Matches)
                                .ThenInclude(m => m.Teams)
                                .ThenInclude(t => t.Team)
                                .ThenInclude(t => t.Golfers)
                                .LastOrDefault(r => r.Date != DateTime.MinValue);
            return View(vm);

        }

        public IActionResult Claims() => View();

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
