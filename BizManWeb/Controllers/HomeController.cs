using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BizManWeb.Data;
using Microsoft.AspNet.Hosting;

namespace BizManWeb.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var Schedule = new Schedule(_hostingEnvironment);
            var vm = new ViewModels.Home.HomeIndexViewModel();
            vm.CurrentRound = Schedule.Data.First(r => r.Date == DateTime.MinValue);
            vm.LastRound = Schedule.Data.Last(r => r.Date != DateTime.MinValue);
            return View(vm);
        }
        
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
