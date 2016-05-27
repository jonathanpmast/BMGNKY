using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Match
    {       
        public int ID { get; set; }

        public ICollection<MatchTeam> Teams { get; set; }
        
        public int MatchRoundID { get; set; }
       
        public Round MatchRound { get; set; }
        public int TeeOrder { get; set; }
        public List<GolferMatchScore> Scores { get; set; }
    }
}
