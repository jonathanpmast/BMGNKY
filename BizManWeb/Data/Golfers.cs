using BizManWeb.Models;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Data
{
    public class Golfers
    {
        public Golfers(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private static List<Golfer> _data = null;
        private IHostingEnvironment _hostingEnvironment;

        public List<Golfer> Data
        {
            get
            {
                if(Golfers._data == null)
                {
                    Golfers._data = ParseGolfers();
                }
                return Golfers._data;
            }
        }

        private List<Golfer> ParseGolfers()
        {
            return JsonConvert.DeserializeObject<List<Golfer>>(File.ReadAllText(_hostingEnvironment.MapPath("data/golfers.json")));
        }
    }
}
