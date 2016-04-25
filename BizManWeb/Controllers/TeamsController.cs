using BizManWeb.Data;
using BizManWeb.ViewModels.Teams;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Controllers
{
    public class TeamsController :Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public TeamsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            Teams teams = new Teams(_hostingEnvironment);
            var viewModel = new IndexViewModel();
            viewModel.Teams = teams.Data;            
            return View(viewModel);
        }
    }
}
