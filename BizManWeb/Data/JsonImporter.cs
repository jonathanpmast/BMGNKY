using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Data
{
    public class JsonImporter : IJsonDataImporter
    {
        private BMGContext _context;
        private IHostingEnvironment _hostingEnvironment;
        private ILogger _logger;
        public JsonImporter(IHostingEnvironment hostingEnvironment, BMGContext context, ILoggerFactory loggerFactory)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _logger = loggerFactory.CreateLogger<JsonImporter>();
        }

        public async Task ImportAsync()
        {
            var golfers = new Golfers(_hostingEnvironment).Data.ToList();
            var rounds = new Schedule(_hostingEnvironment).Data.ToList();
            var teams = new Teams(_hostingEnvironment).Data.ToList();

            foreach(var golfer in golfers)
            {
                golfer.ID = 0;
                _context.Golfers.Add(golfer);
            }

            foreach(var team in teams)
            {
                team.ID = 0;
                _context.Teams.Add(team);
            }

            foreach(var round in rounds)
            {
                round.ID = 0;
                _context.Rounds.Add(round);
            }

            await _context.SaveChangesAsync();
        }
    }
}
