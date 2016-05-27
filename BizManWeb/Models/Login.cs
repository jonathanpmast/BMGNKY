using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Models
{
    public enum LoginType
    {
        Google,
        Facebook
    }

    public class Login
    {
        public int ID { get; set; }
        [Required]
        public string Identifier { get; set; }
        public Golfer Golfer { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public LoginType LoginType { get; set; }
    }
}
