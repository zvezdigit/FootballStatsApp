using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data.Data.DTO
{
    public class League
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public int Season { get; set; }
    }

    public class LeagueDetails
    {
        public League League { get; set; }

        public Country Country { get; set; }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Flag { get; set; }
    }
}
