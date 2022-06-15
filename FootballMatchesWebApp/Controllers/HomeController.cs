using FootballMatchesWebApp.Application.Interfaces;
using FootballMatchesWebApp.Application.Models.DataImport;
using FootballMatchesWebApp.Data;
using FootballMatchesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FootballMatchesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataImporter dataImporter;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IFixtureService fixtureService;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, DataImporter dataImporter
            , IHttpClientFactory httpClientFactory, IFixtureService fixtureService, IConfiguration configuration)
        {
            _logger = logger;
            this.dataImporter = dataImporter;
            this.httpClientFactory = httpClientFactory;
            this.fixtureService = fixtureService;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

      
        
        //userInterface for data import manually 

        //[HttpGet]
        //public IActionResult Import()
        //{
        //    var leagues = fixtureService.GetAllLeagues().OrderBy(x => x.LeagueName).ToList();
        //    ViewBag.Leagues = leagues;
        //    return View();
        //}



        //import Data manually via UserInterface
    //        [HttpPost]
    //        public async Task<IActionResult> Import(ImportDataFormViewModel model)
    //    {
    //        var client = httpClientFactory.CreateClient();

    //        client.BaseAddress = new Uri("https://v3.football.api-sports.io");

    //        var apiKey = configuration["ApiKey"];
    //        client.DefaultRequestHeaders.Add("x-apisports-key", apiKey); //to add user secrets

    //        int requestCounter = 0;

    //        var teams = await client.GetStringAsync($"/teams?league={model.LeagueId}&season={model.Season}");
    //        var fixtures = await client.GetStringAsync($"/fixtures?league={model.LeagueId}&season={model.Season}");

    //        var allPlayers = new List<string>();

    //        var players = await client.GetStringAsync($"/players?league={model.LeagueId}&season={model.Season}&page=1");
    //        allPlayers.Add(players);

    //        int pages = JsonConvert.DeserializeObject<ApiResponse>(players)!.Paging.Total;

    //        requestCounter += 3;

    //        for (int i = 2; i < pages; i++)
    //        {
    //            players = await client.GetStringAsync($"/players?league={model.LeagueId}&season={model.Season}&page={i}");
    //            allPlayers.Add(players);

    //            requestCounter++;

    //            if (requestCounter % 10 == 0)
    //            {
    //                await Task.Delay(TimeSpan.FromSeconds(61));
    //            }
    //        }

    //        if (!ModelState.IsValid)
    //        {
    //            ViewBag.Leagues = fixtureService.GetAllLeagues().ToList();
    //            return View(model);
    //        }

    //        await dataImporter.ImportDataAsync(model.LeagueId, fixtures, teams, allPlayers.ToArray());
    //        return Ok();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //}

    //public class ApiResponse
    //{
    //    public Paging Paging { get; set; }
    //}

    //public class Paging
    //{
    //    public int Total { get; set; }
    //}
}
}