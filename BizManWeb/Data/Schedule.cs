using BizManWeb.Models;
using BizManWebRC2.Extensions;
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
    public class Schedule
    {
        private IHostingEnvironment _hostingEnvironment;

        public Schedule(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        private static List<Round> _data = null;

        public List<Round> Data
        {
            get
            {
                if(Schedule._data == null)
                {
                    Schedule._data = ParseSchedule();
                }
                return Schedule._data;
            }
        }

        private List<Round> ParseSchedule()
        {
            Teams t = new Teams(_hostingEnvironment);
            List<Round> rounds = new List<Round>();
            var fileInfo = _hostingEnvironment.WebRootFileProvider.GetFileInfo("data/schedule.json");
            using (var reader = new JsonTextReader(new StreamReader(fileInfo.CreateReadStream())))
            {
                var jsonArray = JArray.Load(reader);
                foreach(var item in jsonArray)
                {
                    var r = new Round();
                    r.Matches = new List<Match>();
                    if(item["Date"] !=null)
                    {
                        r.Date = item["Date"].Value<DateTime>();                       
                    }
                    if (rounds.Count >= 1 && r.Date == DateTime.MinValue && rounds.Last().Date != DateTime.MinValue)
                        r.IsCurrentRound = true;
                    r.ID = r.Order = item["ID"].Value<int>();
                    var matches = item["Matches"].Values<int>().GetEnumerator();
                    var teeOrder = 1;
                    while(matches.MoveNext())
                    {
                        var id1 = matches.Current;
                        matches.MoveNext();
                        var id2 = matches.Current;         
                        var m = new Match();
                        m.TeeOrder = teeOrder++;
                        m.Teams = new List<MatchTeam>()
                        {
                            new MatchTeam() { Team = t.Data.First(team => team.ID == id1), Match = m },
                            new MatchTeam() { Team = t.Data.First(team => team.ID == id2), Match = m }
                        };
                        r.Matches.Add(m);
                    }
                    ((List<Match>)r.Matches).Sort((m1, m2) => m1.TeeOrder.CompareTo(m2.TeeOrder));
                    rounds.Insert(0,r); 
                }
                rounds.Sort((r1, r2) => r1.ID.CompareTo(r2.ID));
            }
            return rounds;
        }
    }
}
