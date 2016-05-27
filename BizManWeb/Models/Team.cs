using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Team
    {
        public int ID { get; set; }

        public int TeamNumber { get; set; }

        public ICollection<Golfer> Golfers { get; set; }
        
        public ICollection<MatchTeam> Matches { get; set; }
    }
}
