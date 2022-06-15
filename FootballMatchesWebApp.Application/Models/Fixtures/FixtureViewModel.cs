using FootballMatchesWebApp.Application.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Models.Fixtures
{
    public class FixtureViewModel
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Referee { get; set; }

        public int AwayGoals { get; set; }

        public int HomeGoals { get; set; }

        public PlayerLeagueViewModel League { get; set; }

        public TeamViewModel HomeTeam { get; set; }

        public TeamViewModel AwayTeam { get; set; }

    }
}
