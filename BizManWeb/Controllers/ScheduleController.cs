using BizManWeb.Data;
using BizManWeb.ViewModels.Schedule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BizManWeb.Controllers
{
    public class ScheduleController : Controller
    {
        BMGContext _context;
        public ScheduleController(BMGContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new ScheduleIndexViewModel();
            vm.Rounds = _context.Rounds
                            .OrderBy(r => r.Order)
                            .Include(r => r.Matches)
                            .ThenInclude(m=>m.Teams)
                            .ThenInclude(t=>t.Team)
                            .ThenInclude(t=> t.Golfers)
                            .ToList();
            vm.Rounds.OrderBy(r=>r.Order).First(r => r.Date == DateTime.MinValue).IsCurrentRound = true;
            vm.Teams = _context.Teams.OrderBy(t=> t.TeamNumber).Include(t=> t.Golfers).ToList(); 
            return View(vm);
        }
    }
}
