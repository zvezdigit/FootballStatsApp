using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data.Entities
{
    public class PlayerStats
    {
   

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        public Team Team { get; set; }

        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }

        public League League { get; set; }

        public int Season { get; set; }

        public int? GoalsScored { get; set; }

        public int? Appearances { get; set; }

        public int? MinutesPlayed { get; set; }

        public string? Position { get; set; }
    }
}
