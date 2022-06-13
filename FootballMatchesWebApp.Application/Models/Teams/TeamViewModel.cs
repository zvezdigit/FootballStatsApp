using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Models.Teams
{
    public class TeamViewModel
    {
        public int TeamId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int? Founded { get; set; }
        public string Country { get; set; }
        public bool National { get; set; }

        public VenueViewModel Venue { get; set; }
    }
}
