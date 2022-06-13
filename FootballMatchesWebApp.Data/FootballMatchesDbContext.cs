using FootballMatchesWebApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data
{
    public class FootballMatchesDbContext: IdentityDbContext<ApplicationUser>
    {

        public FootballMatchesDbContext(DbContextOptions<FootballMatchesDbContext> options)
            : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Fixture> Fixtures { get; set; }

        public DbSet<League> Leagues { get; set; }

        public DbSet<PlayerStats> PlayerStats { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Player>().OwnsOne(x => x.PlayerStats);

            builder.Entity<Fixture>(f =>
            {
                

                f.HasOne(x => x.AwayTeam)
                .WithMany()
                .HasForeignKey(x => x.AwayTeamId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

                f.HasOne(x => x.HomeTeam)
                .WithMany()
                .HasForeignKey(x => x.HomeTeamId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);
            });

            base.OnModelCreating(builder);
        }

    }
}
