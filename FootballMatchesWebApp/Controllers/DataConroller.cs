using FootballMatchesWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace FootballMatchesWebApp.Controllers
{
    public class DataConroller : Controller
    {
        private readonly DataImporter dataImporter;

        public DataConroller(DataImporter dataImporter)
        {
            this.dataImporter = dataImporter;
        }

        [HttpGet]
        public async Task<IActionResult> Seed()
        {
            await dataImporter.ImportDataAsync();
            return Ok();
        }
    }
}
