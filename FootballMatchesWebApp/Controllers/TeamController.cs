using FootballMatchesWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FootballMatchesWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FootballMatchesWebApp.Application.Models.Teams;
using FootballMatchesWebApp.Application.Models;

namespace FootballMatchesWebApp.Controllers
{
    public class TeamController: Controller

    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService _teamService)
        {
            this.teamService = _teamService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int p = 1, int s = 10)
        {
            ViewBag.IsSearch = false;

            var allTeams = await teamService.GetAllTeams(p,s);

            return View(allTeams);
        }

        [HttpPost]
        public async Task<IActionResult> All(SearchTeamViewFormModel model)
        {
            ViewBag.IsSearch = true;

            if (!String.IsNullOrEmpty(model.TeamName))
            {
                var teams = teamService.SearchTeamsByName(model.TeamName);

                return View(new PagedListViewModel<TeamViewModel>
                {
                    Items = teams.ToList(),
                    PageNo = 1,
                    PageSize = 10,
                    TotalRecords = teams.Count()
                });
            }

            return await All();
        }

        [HttpGet]
        public IActionResult TeamPlayers(int teamId)
        {
            ViewBag.TeamId = teamId;

            var teamPlayers = teamService.TeamPlayers(teamId);

            return View(teamPlayers);
        }

        [HttpPost]
        public IActionResult TeamPlayers(SearchTeamPlayerViewModel model)
        {
            ViewBag.TeamId = model.TeamId;

            if (!String.IsNullOrEmpty(model.PLayerName))
            {
                var players = teamService.SearchPlayerByName(model.TeamId, model.PLayerName);
                return View(players);
            }

            return Redirect("/Team/TeamPlayers");
        }

        [HttpGet]
        public IActionResult PlayerStats(int playerId)
        {
            var playerStats = teamService.GetTeamPlayerStats(playerId);

            return View(playerStats);
        }

        [HttpGet]
        public IActionResult TopScorers()
        {
            var topScorers = teamService.TopScorers();

            return View(topScorers);
        }

        [HttpGet]
        public IActionResult TopTeams()
        {
            var topTeams = teamService.TopTeams();

            return View(topTeams);
        }
    }
}
