using BizManWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.ViewModels.Schedule
{
    public class ScheduleIndexViewModel
    {
        public List<Round> Rounds { get; set; }
        public List<Team> Teams { get; set; }
    }
}
