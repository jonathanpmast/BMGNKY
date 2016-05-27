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
        public ICollection<Round> Rounds { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
