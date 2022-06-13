using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data.Entities
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int? Founded { get; set; }
        public string Country { get; set; }
        public bool National { get; set; }
        
        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }

        public Venue Venue { get; set; }

        public ICollection<Fixture> HomeFixtures = new List<Fixture>();
        public ICollection<Fixture> AwayFixtures = new List<Fixture>();

    }
}
