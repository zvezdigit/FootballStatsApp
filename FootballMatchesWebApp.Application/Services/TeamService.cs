using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models;
using FootballMatchesWebApp.Application.Models.Teams;
using FootballMatchesWebApp.Data.Data.Common;
using FootballMatchesWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace FootballMatchesWebApp.Application.Services
{

    public class TeamService : ITeamService
    {
        private readonly IRepository repository;
        

        public TeamService(IRepository _repository)
        {
              repository = _repository;
        }

        public async Task<PagedListViewModel<TeamViewModel>> GetAllTeams(int pageNo, int pageSize)
        {
            PagedListViewModel<TeamViewModel> result = new PagedListViewModel<TeamViewModel>()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = await repository.All<Team>().CountAsync();

            var teams = await repository.All<Team>()
                 .Include(x => x.Venue)
                 .OrderBy(x => x.Name)
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)                 
                 .Select(team => new TeamViewModel
                 {
                     TeamId = team.Id,
                     Code = team.Code,
                     Country = team.Country,
                     Founded = team.Founded,
                     Name = team.Name,
                     Venue = new VenueViewModel { VenueName = team.Venue.Name }
                 }).ToListAsync();

            result.Items = teams;

            return result;
        }              

        public IEnumerable<TeamViewModel> SearchTeamsByName(string name)
        {
            return repository.All<Team>()
                 .Include(x => x.Venue)
                 .Where(x => x.Name.Contains(name))
                 .Select(team => new TeamViewModel
                 {
                     TeamId = team.Id,
                     Code = team.Code,
                     Country = team.Country,
                     Founded = team.Founded,
                     Name = team.Name,
                     Venue = new VenueViewModel { VenueName = team.Venue.Name }
                 }).ToList();
        }

        public IEnumerable<TeamPlayerViewModel> TeamPlayers(int teamId)
        {
            var teamPlayers = repository.All<Player>()
                .Include(x=>x.PlayerStats)
                 .Where(x => x.PlayerStats.TeamId == teamId)
                 .Select(x => new TeamPlayerViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Age = x.Age,
                     Height = x.Height,
                     Weight = x.Weight,
                     Injured = x.Injured,
                     Nationality = x.Nationality
                 }).ToList();

            return teamPlayers;
        }

        public IEnumerable<TeamPlayerViewModel> SearchPlayerByName(int teamId, string name)
        {
            var teamPlayers = repository.All<Player>()
                .Include(x => x.PlayerStats)
                  .Where(x => x.PlayerStats.TeamId == teamId)
                  .Where(x=>x.Name.Contains(name))
                  .Select(x => new TeamPlayerViewModel
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Age = x.Age,
                      Height = x.Height,
                      Weight = x.Weight,
                      Injured = x.Injured,
                      Nationality = x.Nationality
                  }).ToList();

            return teamPlayers;
        }

        public TeamPlayerStatsViewModel GetTeamPlayerStats(int playerId)
        {
            var playerStats = repository.All<Player>()
                .Include(x=>x.PlayerStats)
                 .ThenInclude(x => x.League)
                 .Where(x => x.Id == playerId)
                 .Select(x => new TeamPlayerStatsViewModel
                 {
                     Appearances = x.PlayerStats.Appearances,
                     League = new PlayerLeagueViewModel
                     {
                         LeagueName = x.PlayerStats.League.Name,
                     },
                     GoalsScored = x.PlayerStats.GoalsScored,
                     MinutesPlayed = x.PlayerStats.MinutesPlayed,
                     Position = x.PlayerStats.Position,
                     Season = x.PlayerStats.Season
                 }).FirstOrDefault();

            return playerStats;
        }

        public IEnumerable<TopScorersViewModel> TopScorers()
        {
            var topScorers = repository.All<Player>()
                .Include(x => x.PlayerStats)
                 .ThenInclude(x => x.League)
                 .Select(x => new TopScorersViewModel
                 {
                     Id=x.Id,
                     Name=x.Name,
                     Nationality=x.Nationality,
                     GoalsScored = x.PlayerStats.GoalsScored,
                 }).OrderByDescending(x => x.GoalsScored)
                 .Take(10)
                  .ToList();

            return topScorers;
        }

        public IEnumerable<TopTeamViewModel> TopTeams()
        {
            var homeWins = repository.All<Fixture>()
                .Where(x => x.HomeGoals > x.AwayGoals)
                .GroupBy(x => x.HomeTeam.Name, x => x.HomeGoals)
                .Select(x => new { TeamName = x.Key, Goals = x.Sum() })
                .OrderByDescending(x => x.Goals);

            var awayWins = repository.All<Fixture>()
                .Where(x => x.AwayGoals > x.HomeGoals)
                .GroupBy(x => x.AwayTeam.Name, x => x.AwayGoals)
                .Select(x => new { TeamName = x.Key, Goals = x.Sum() })
                .OrderByDescending(x => x.Goals);

            var topTeams = homeWins.Concat(awayWins)
                .GroupBy(x => x.TeamName)
                .Select(x => new { TeamName = x.Key, Goals = x.Sum(y => y.Goals) })
                .OrderByDescending(x => x.Goals)
                .Take(10)
                .Select(x => new TopTeamViewModel
                {
                    Name = x.TeamName,
                    Goals = x.Goals
                })
                .ToArray();

            return topTeams;
        }
    }
}
