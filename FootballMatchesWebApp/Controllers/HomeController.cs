using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models.DataImport;
using FootballMatchesWebApp.Data;
using FootballMatchesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FootballMatchesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataImporter dataImporter;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IFixtureService fixtureService;
       
        public HomeController(ILogger<HomeController> logger, DataImporter dataImporter 
            ,IHttpClientFactory httpClientFactory, IFixtureService fixtureService)
        {
            _logger = logger;
            this.dataImporter = dataImporter;
            this.httpClientFactory = httpClientFactory;
            this.fixtureService = fixtureService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Import()
        {
            var leagues = fixtureService.GetAllLeagues();
            ViewBag.Leagues = leagues;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(ImportDataFormViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            client.BaseAddress = new Uri("https://v3.football.api-sports.io");
            client.DefaultRequestHeaders.Add("x-apisports-key", "50bf08bb7425183811257ad2d0667ea1"); //to add user secrets

            int requestCounter = 0;

            var teams = await client.GetStringAsync($"/teams?leauge={model.LeagueId}&season={model.Season}");            
            var fixtures = await client.GetStringAsync($"/fixtures?leauge={model.LeagueId}&season={model.Season}");

            requestCounter += 2;

            var allPlayers = new List<string>();
            for (int i = 1; i < 10; i++) 
            {
                var players = await client.GetStringAsync($"/players?leauge={model.LeagueId}&season={model.Season}&page={i}");
                allPlayers.Add(players);

                requestCounter++;
                
                if(requestCounter % 10 == 0)
                {
                    await Task.Delay(TimeSpan.FromSeconds(61));
                }
            }

          

            if (!ModelState.IsValid)
            {
                ViewBag.Leagues = fixtureService.GetAllLeagues().ToList();
                return View(model);
            }

            await dataImporter.ImportDataAsync(model.LeagueId, fixtures, teams, allPlayers.ToArray());
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}