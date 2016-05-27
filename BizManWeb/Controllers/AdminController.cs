using BizManWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        ILogger _logger;
        private BMGContext _context;
        private IJsonDataImporter _jsonImporter;

        public AdminController(IJsonDataImporter jsonImporter, BMGContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AdminController>();
            _context = context;
            _jsonImporter = jsonImporter;
        }
        public IActionResult Index() => View();
        public IActionResult Data() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadData()
        {
            await _jsonImporter.ImportAsync();
            return View();
        }

    }
}
