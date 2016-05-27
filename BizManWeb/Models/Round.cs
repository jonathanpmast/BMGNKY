using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Round
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } = DateTime.MinValue;

        public int Order { get; set; }

        [NotMapped]
        public bool HasBeenPlayed
        {
            get { return Date != DateTime.MinValue; }
        }

        [NotMapped]
        public bool IsCurrentRound { get; set; } = false;
        public ICollection<Match> Matches { get; set; }

        
    }
}
