using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data.Entities
{
    public class Fixture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Referee { get; set; }
        
        public int AwayGoals { get; set; }

        public int HomeGoals { get; set; }

        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }

        public League League { get; set; }

        [ForeignKey(nameof(Team))]
        public int? HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        [ForeignKey(nameof(Team))]
        public int? AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
    }
}
