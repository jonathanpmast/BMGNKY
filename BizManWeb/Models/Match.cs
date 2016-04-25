using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Match
    {       
        public List<Team> Teams { get; private set; } = new List<Team>();
        public Round MatchRound { get; set; }
        public int TeeOrder { get; set; }

    }
}
