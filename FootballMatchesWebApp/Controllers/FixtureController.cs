using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models;
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
        public async Task<IActionResult> All(int p = 1, int s = 10)
        {
            ViewBag.IsSearch = false;

            var allFixtures = await fixtureService.GetAllFixtures(p, s);

            return View(allFixtures);
        }

        [HttpPost]
        public async Task<IActionResult> All(SearchFixtureViewFormModel model)
        {
            ViewBag.IsSearch = true;

            if (!String.IsNullOrEmpty(model.TeamName))
            {
                var fixtures = fixtureService.SearchFixturesByName(model.TeamName);

                return  View(new PagedListViewModel<FixtureViewModel>
                {
                    Items = fixtures.ToList(),
                    PageNo = 1,
                    PageSize = 10,
                    TotalRecords = fixtures.Count()
                });
            }

            return await All();
        }

    }
}
