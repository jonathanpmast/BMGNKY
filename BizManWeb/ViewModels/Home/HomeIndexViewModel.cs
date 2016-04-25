using BizManWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public Round CurrentRound { get; set; }
        public DateTime CurrentRoundDate
        {
            get
            {
                return GetNextWeek(DayOfWeek.Wednesday);
            }
        }
        public Round LastRound { get; set; }        
        private DateTime GetNextWeek(DayOfWeek dayOfWeek)
        {
            DateTime today = DateTime.Today;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilNext = ((int)dayOfWeek - (int)today.DayOfWeek + 7) % 7;
            return today.AddDays(daysUntilNext);
        }
    }
}
