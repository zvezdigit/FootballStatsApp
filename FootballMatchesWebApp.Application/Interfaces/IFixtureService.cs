using FootballMatchesWebApp.Application.Models.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Interfaces
{
    public interface IFixtureService
    {

        IEnumerable<FixtureViewModel> GetAllFixtures();

        IEnumerable<FixtureViewModel> SearchFixturesByName(string name);
    }
}
