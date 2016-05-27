using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class GolferMatchScore
    {
        public int ID { get; set; }

        [Required]
        public int GolferID { get; set; }
        [Required]
        [InverseProperty("Scores")]
        public Golfer Golfer { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int MatchID { get; set; }
        [Required]
        public Match Match { get; set; }

        public Golfer SubbedFor { get; set; }
        
    }
}
