using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models;
using FootballMatchesWebApp.Application.Models.Fixtures;
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
    public class FixtureService: IFixtureService
    {
        private readonly IRepository repository;

        public FixtureService(IRepository _repository)
        {
            this.repository = _repository;
        }

        public async Task<PagedListViewModel<FixtureViewModel>> GetAllFixtures(int pageNo, int pageSize)
        {

            PagedListViewModel<FixtureViewModel> result = new PagedListViewModel<FixtureViewModel>()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = await repository.All<Fixture>().CountAsync();

            var fixtures = await repository.All<Fixture>()
                .Include(x=>x.AwayTeam)
                .Include(x=>x.HomeTeam)
                .Include(x=>x.League)
                .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
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
                }).ToListAsync();

            result.Items = fixtures;

            return result;
        }

        public IEnumerable<LeagueListViewModel> GetAllLeagues()
        {
            return repository.All<League>()
                 .Select(x => new LeagueListViewModel
                 {
                     LeagueId=x.Id,
                     LeagueName=x.Name
                 }).ToList();
        }

        public async Task<PagedListViewModel<FixtureViewModel>> SearchFixturesByName(string name, int pageNo, int pageSize)
        {
            PagedListViewModel<FixtureViewModel> result = new PagedListViewModel<FixtureViewModel>()
            {
                PageNo = pageNo,
                PageSize = pageSize,
                Model = name

            };

            result.TotalRecords = await repository.All<Fixture>()
             .Include(x => x.AwayTeam)
             .Include(x => x.HomeTeam)
             .Include(x => x.League)
             .Where(x => x.AwayTeam.Name.Contains(name) || x.HomeTeam.Name.Contains(name))
             .CountAsync();

            var fixtures = await repository.All<Fixture>()
             .Include(x => x.AwayTeam)
             .Include(x => x.HomeTeam)
             .Include(x => x.League)
             .Where(x=>x.AwayTeam.Name.Contains(name) || x.HomeTeam.Name.Contains(name))
             .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
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
             }).ToListAsync();

            result.Items=fixtures;

            return result;
        }
    }
}
