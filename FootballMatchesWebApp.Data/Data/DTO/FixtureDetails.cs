using Newtonsoft.Json;

namespace FootballMatchesWebApp.Data.Data.DTO
{
    public class FixtureDetails
    {
        public Fixture Fixture { get; set; }

        public Goal Goals { get; set; }

        public League League { get; set; }

        [JsonProperty("teams")]
        public FixtureTeams Teams { get; set; }
    }
}
