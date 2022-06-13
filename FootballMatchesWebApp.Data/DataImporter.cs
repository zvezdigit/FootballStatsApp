using FootballMatchesWebApp.Data.Data.DTO;
using Newtonsoft.Json;

namespace FootballMatchesWebApp.Data
{
    public class DataImporter
    {
        private readonly FootballMatchesDbContext dbContext;

        public DataImporter(FootballMatchesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task ImportDataAsync()
        {
            //1.load json data from files
            //2.Deserialze JSon to DTOs
            //3.Map Dtos to Entities
            //4.Save Entities to DbContext

            string inputJsonFixture = File.ReadAllText("../FootballMatchesWebApp.Data/Data/DataSets/fixtures.json");
            string inputJsonLeagues = File.ReadAllText("../FootballMatchesWebApp.Data/Data/DataSets/leagues.json");
            string[] inputJsonPLayers = Directory.GetFiles("../FootballMatchesWebApp.Data/Data/DataSets/players").Select(File.ReadAllText).ToArray();
            string inputJsonTeams = File.ReadAllText("../FootballMatchesWebApp.Data/Data/DataSets/teams.json");

            var fixtureDtos = JsonConvert.DeserializeObject<ApiResponse<FixtureDetails>>(inputJsonFixture).Data;
            var leagueDtos = JsonConvert.DeserializeObject<ApiResponse<LeagueDetails>>(inputJsonLeagues).Data;
            var teamDtos = JsonConvert.DeserializeObject<ApiResponse<TeamDetails>>(inputJsonTeams).Data;

            var playerDtos = inputJsonPLayers.Select(json =>
            {
                return JsonConvert.DeserializeObject<ApiResponse<PlayerDetails>>(json).Data;
            }).SelectMany(x => x);
            

            var fixtures = fixtureDtos.Select(f =>
            {
                return new Entities.Fixture
                {
                    Id = f.Fixture.Id.Value,
                    Date = f.Fixture.Date,
                    AwayGoals = f.Goals.Away,
                    HomeGoals = f.Goals.Home,
                    AwayTeamId = f.Teams.Away.Id.Value,
                    HomeTeamId = f.Teams.Home.Id.Value,
                    LeagueId = f.League.Id.Value,
                    Referee = f.Fixture.Referee
                };
            });

            var teams = teamDtos.Select(t =>
            {

                return new Entities.Team
                {
                    Id = t.Team.Id.Value,
                    Code = t.Team.Code,
                    Country = t.Team.Country,
                    Name = t.Team.Name,
                    Founded = t.Team.Founded,
                    National = t.Team.National,
                    VenueId = t.Venue.Id.Value
                };
            });

            var teamIds = teams.Select(t => t.Id).ToArray();

            var players = playerDtos.Select(p=>
            {
                var stats = p.Statistics.First(x => x.League.Id == 39);

                var playerStats = new Entities.PlayerStats
                {
                    Appearances = stats.Games.Appearences,
                    GoalsScored = stats.Goals.Total,
                    LeagueId = stats.League.Id.Value,
                    TeamId = stats.Team.Id.Value,
                    MinutesPlayed = stats.Games.Minutes,
                    Position = stats.Games.Position,
                    Season = stats.League.Season,
                };

                return new Entities.Player
                {
                    Id = p.Player.Id.Value,
                    Age = p.Player.Age,
                    Height = p.Player.Height,
                    Injured = p.Player.Injured,
                    Name = p.Player.Name,
                    Nationality=p.Player.Nationality,
                    Weight=p.Player.Weight,
                    PlayerStats = playerStats,
                };
            });

            players = players.Where(p => teamIds.Contains(p.PlayerStats.TeamId)).Distinct();

            var leagues = leagueDtos.Select(l =>
            {
                return new Entities.League
                {
                    Id = l.League.Id.Value,
                    Country = l.Country.Code,
                    Name = l.League.Name
                };
            });

            var venues = teamDtos.Select(v =>
            {
                return new Entities.Venue
                {
                    Id = v.Venue.Id.Value,
                    Name = v.Venue?.Name,
                    Address = v.Venue?.Address,
                    Capacity = v.Venue?.Capacity,
                    City = v.Venue?.City
                   
                };
            });

            await dbContext.Leagues.AddRangeAsync(leagues);
            await dbContext.Venues.AddRangeAsync(venues);
            await dbContext.Teams.AddRangeAsync(teams);
            await dbContext.Players.AddRangeAsync(players);
            await dbContext.Fixtures.AddRangeAsync(fixtures);

            await dbContext.SaveChangesAsync();
        }
    }
}
