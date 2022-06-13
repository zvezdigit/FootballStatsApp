using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models.Fixtures;
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
    public class FixtureService: IFixtureService
    {
        private readonly IRepository repository;

        public FixtureService(IRepository _repository)
        {
            this.repository = _repository;
        }

        public IEnumerable<FixtureViewModel> GetAllFixtures()
        {
            var fixtures = repository.All<Fixture>()
                .Include(x=>x.AwayTeam)
                .Include(x=>x.HomeTeam)
                .Include(x=>x.League)
                .Select(x=>new FixtureViewModel
                {
                    Id=x.Id,
                    League=new Models.Teams.PlayerLeagueViewModel { 
                        LeagueName = x.League.Name
                    },
                    AwayGoals = x.AwayGoals,
                    HomeGoals = x.HomeGoals,
                    Date = x.Date,
                    Referee = x.Referee,
                    AwayTeam = new Models.Teams.TeamViewModel
                    {
                        Name = x.AwayTeam.Name
                    },
                    HomeTeam = new Models.Teams.TeamViewModel
                    {
                        Name = x.HomeTeam.Name
                    }
                }).ToList();

            return fixtures;
        }

        public IEnumerable<FixtureViewModel> SearchFixturesByName(string name)
        {
            var fixtures = repository.All<Fixture>()
             .Include(x => x.AwayTeam)
             .Include(x => x.HomeTeam)
             .Include(x => x.League)
             .Where(x=>x.AwayTeam.Name.Contains(name) || x.HomeTeam.Name.Contains(name))
             .Select(x => new FixtureViewModel
             {
                 Id = x.Id,
                 League = new Models.Teams.PlayerLeagueViewModel
                 {
                     LeagueName = x.League.Name
                 },
                 AwayGoals = x.AwayGoals,
                 HomeGoals = x.HomeGoals,
                 Date = x.Date,
                 Referee = x.Referee,
                 AwayTeam = new Models.Teams.TeamViewModel
                 {
                     Name = x.AwayTeam.Name
                 },
                 HomeTeam = new Models.Teams.TeamViewModel
                 {
                     Name = x.HomeTeam.Name
                 }
             }).ToList();

            return fixtures;
        }
    }
}
