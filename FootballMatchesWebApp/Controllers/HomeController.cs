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

        public HomeController(ILogger<HomeController> logger, DataImporter dataImporter)
        {
            _logger = logger;
            this.dataImporter = dataImporter;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Import()
        {
            await dataImporter.ImportDataAsync();
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}