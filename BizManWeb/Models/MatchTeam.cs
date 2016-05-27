using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class MatchTeam
    {
        public int ID { get; set; }
        public Match Match { get; set; }
        public Team Team { get; set; }
    }
}
