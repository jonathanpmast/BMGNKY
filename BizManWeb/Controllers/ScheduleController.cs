using BizManWeb.Data;
using BizManWeb.ViewModels.Schedule;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Controllers
{
    public class ScheduleController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public ScheduleController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            Schedule s = new Schedule(_hostingEnvironment);
            Teams t = new Teams(_hostingEnvironment);
            var vm = new ScheduleIndexViewModel();
            vm.Rounds = s.Data;
            vm.Teams = t.Data;
            return View(vm);
        }
    }
}
