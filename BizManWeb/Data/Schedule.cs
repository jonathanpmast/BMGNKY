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
            using (var reader = new JsonTextReader(new StreamReader(_hostingEnvironment.MapPath("data/schedule.json"))))
            {
                var jsonArray = JArray.Load(reader);
                foreach(var item in jsonArray)
                {
                    var r = new Round();
                    if(item["Date"] !=null)
                    {
                        r.Date = item["Date"].Value<DateTime>();                       
                    }
                    if (rounds.Count >= 1 && r.Date == DateTime.MinValue && rounds.Last().Date != DateTime.MinValue)
                        r.IsCurrentRound = true;
                    r.ID = item["ID"].Value<int>();
                    var matches = item["Matches"].Values<int>().GetEnumerator();
                    var teeOrder = 1;
                    while(matches.MoveNext())
                    {
                        var id1 = matches.Current;
                        matches.MoveNext();
                        var id2 = matches.Current;         
                        var m = new Match();
                        m.TeeOrder = teeOrder++;
                        m.Teams.AddRange(t.Data.Where(team => team.ID == id1 || team.ID == id2));
                        r.AddMatch(m);
                    }
                    r.Matches.Sort((m1, m2) => m1.TeeOrder.CompareTo(m2.TeeOrder));
                    rounds.Add(r);
                }
            }
            return rounds;
        }
    }
}
