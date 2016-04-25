using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Team
    {
        public int ID { get; set; }
        public Golfer GolferOne { get; set; }
        public Golfer GolferTwo { get; set; }
    }
}
