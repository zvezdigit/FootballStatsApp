using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Models.Teams
{
    public class TeamPlayerStatsViewModel
    {
        public int Season { get; set; }

        public int? GoalsScored { get; set; }

        public int? Appearances { get; set; }

        public int? MinutesPlayed { get; set; }

        public string? Position { get; set; }

        public PlayerLeagueViewModel League { get; set; }
    }
}
