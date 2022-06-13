using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models.Teams;
using FootballMatchesWebApp.Data.Data.Common;
using FootballMatchesWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Services
{

    public class TeamService : ITeamService
    {
        private readonly IRepository repository;
        

        public TeamService(IRepository _repository)
        {
              repository = _repository;
        }

        public IEnumerable<TeamViewModel> GetAllTeams()
        {
            return repository.All<Team>()
                 .Include(x => x.Venue)
                 .Select(team => new TeamViewModel
                 {
                     TeamId = team.Id,
                     Code = team.Code,
                     Country = team.Country,
                     Founded = team.Founded,
                     Name = team.Name,
                     Venue = new VenueViewModel {  VenueName = team.Venue.Name }
                 }).ToList();
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
    }
}
