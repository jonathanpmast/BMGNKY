using BizManWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizManWeb.Data
{
    public class BMGContext : DbContext
    {
        public BMGContext(DbContextOptions<BMGContext> options) : base(options)
        {

        }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<GolferMatchScore> Scores { get; set; }
        public DbSet<MatchTeam> MatchTeams { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
