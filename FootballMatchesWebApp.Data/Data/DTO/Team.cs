using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Newtonsoft.Json;

namespace FootballMatchesWebApp.Data.Data.DTO
{
    public class Team
    {

        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Founded { get; set; }
        public string Country { get; set; }
        public bool National { get; set; }

       

    }

    public class TeamDetails
    {
        public Team Team { get; set; }
        public TeamVenue Venue { get; set; }
    }
}
