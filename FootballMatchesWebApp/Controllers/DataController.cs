using FootballMatchesWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchesWebApp.Controllers
{
    public class DataController : Controller
    {
        private readonly DataImporter dataImporter;

        public DataController(DataImporter dataImporter)
        {
            this.dataImporter = dataImporter;
        }

        [HttpGet]
        public async Task<IActionResult> Seed()
        {
            await dataImporter.ImportDataAsync();

            return View();
        }
    }
}
