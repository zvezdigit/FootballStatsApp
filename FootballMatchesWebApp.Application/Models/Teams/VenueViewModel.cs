using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Models.Teams
{
    public class VenueViewModel
    {
        public int VenueId { get; set; }

        public string VenueName { get; set; }

        public string City { get; set; }

        public int? Capacity { get; set; }
    }
}
