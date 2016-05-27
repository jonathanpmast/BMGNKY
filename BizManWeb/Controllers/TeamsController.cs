using BizManWeb.Data;
using BizManWeb.ViewModels.Teams;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        BMGContext _context;
        public TeamsController(IHostingEnvironment hostingEnvironment, BMGContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Teams = _context.Teams.ToList();            
            return View(viewModel);
        }
    }
}
