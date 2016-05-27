using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Golfer
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public int TeamID { get; set; }

        [InverseProperty("Golfers")]
        public Team Team { get; set; }

        public int Handicap { get; set; }

        public List<GolferMatchScore> Scores { get; set; }

        [NotMapped]
        public string LastName
        {
            get
            {
                var parts = Name.Split(' ');
                if (parts.Length == 1)
                    return "";
                return parts[1];
            }
        }

        [NotMapped]
        public string FirstInitialPlusLastName
        {
            get
            {
                return FirstName.First() + " " + LastName;
            }
        }

        [NotMapped]
        public string FirstName
        {
            get
            {
                return Name.Split(' ')[0];
            }
        }
    }
}
