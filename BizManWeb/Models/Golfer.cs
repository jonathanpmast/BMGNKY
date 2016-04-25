using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public class Golfer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Handicap { get; set; }
        public string LastName
        {
            get
            {
                return Name.Split(' ')[1];
            }
        }

        public string FirstInitialPlusLastName { get
            {
                return FirstName.First() + " " + LastName;
            }
        }
        public string FirstName
        {
            get
            {
                return Name.Split(' ')[0];
            }
        }
    }
}
