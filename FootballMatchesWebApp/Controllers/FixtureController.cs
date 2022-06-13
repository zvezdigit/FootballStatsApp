using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models.Fixtures;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchesWebApp.Controllers
{
    public class FixtureController: Controller
    {
        private readonly IFixtureService fixtureService;

        public FixtureController(IFixtureService _fixtureSErvice)
        {
            fixtureService = _fixtureSErvice;
        }

        [HttpGet]
        public IActionResult All()
        {
            var allFixtures = fixtureService.GetAllFixtures();

            return View(allFixtures);
        }

        [HttpPost]
        public IActionResult All(SearchFixtureViewFormModel model)
        {


            if (!String.IsNullOrEmpty(model.TeamName))
            {
                var teams = fixtureService.SearchFixturesByName(model.TeamName);
                return View(teams);
            }

            return All();
        }

    }
}
