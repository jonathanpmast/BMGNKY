using BizManWeb.Models;
using Microsoft.AspNetCore.Hosting;
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
            var fileInfo = _hostingEnvironment.WebRootFileProvider.GetFileInfo("data/teams.json");
            using (JsonTextReader textReader = new JsonTextReader(new StreamReader(fileInfo.CreateReadStream())))
            {
                var jsonArray = JArray.Load(textReader);
                foreach (var item in jsonArray)
                {
                    var players = item["players"].Values<int>().ToList();
                    var id = item["id"].Value<int>();
                    data.Add(
                        new Team
                        {
                            ID = id,
                            TeamNumber = id,
                            Golfers = new List<Golfer>() {
                                golfers.Data.First(g => g.ID == players[0]),
                                golfers.Data.First(g => g.ID == players[1])
                            }
                        });
                }
            }
            return data;
        }
    }
}
