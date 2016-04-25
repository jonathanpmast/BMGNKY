using BizManWeb.Models;
using Microsoft.AspNet.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Data
{
    public class Teams
    {
        private IHostingEnvironment _hostingEnvironment;

        public Teams(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private static List<Team> _data = null;

        public List<Team> Data
        {
            get
            {
                if (Teams._data == null)
                {
                    Teams._data = PopulateData();
                }
                return Teams._data;
            }
        }

        private List<Team> PopulateData()
        {
            Golfers golfers = new Golfers(_hostingEnvironment);
            var data = new List<Team>();
            using (JsonTextReader textReader = new JsonTextReader(new StreamReader(_hostingEnvironment.MapPath("data/teams.json"))))
            {
                var jsonArray = JArray.Load(textReader);
                foreach(var item in jsonArray)
                {
                    var players = item["players"].Values<int>().ToList();
                    var id = item["id"].Value<int>();
                    data.Add(
                        new Team
                        {
                            ID = id,
                            GolferOne = golfers.Data.First(g => g.ID == players[0]),
                            GolferTwo = golfers.Data.First(g => g.ID == players[1])
                        });
                }
            }
            return data;
        }
    }
}
