using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Round
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } = DateTime.MinValue;

        public bool HasBeenPlayed
        {
            get { return Date != DateTime.MinValue; }
        }

        public bool IsCurrentRound { get; set; } = false;
        public List<Match> Matches { get; private set; } = new List<Match>();

        public void AddMatch(Match match)
        {
            match.MatchRound = this;
            Matches.Add(match);
        }
    }
}
