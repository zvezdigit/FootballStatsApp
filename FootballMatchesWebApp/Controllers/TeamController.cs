using FootballMatchesWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FootballMatchesWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FootballMatchesWebApp.Application.Models.Teams;

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
        public IActionResult All()
        {
            var allTeams = teamService.GetAllTeams();

            return View(allTeams);
        }

        [HttpPost]
        public IActionResult All(SearchTeamViewFormModel model)
        {
          
                        
            if (!String.IsNullOrEmpty(model.TeamName))
            {
                var teams = teamService.SearchTeamsByName(model.TeamName);
                return View(teams);
            }

            return All();
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

        public IActionResult PlayerStats(int playerId)
        {
            var playerStats = teamService.GetTeamPlayerStats(playerId);

            return View(playerStats);
        }
    }
}
