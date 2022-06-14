using FootballMatchesWebApp.Application.Models;
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

        Task<PagedListViewModel<FixtureViewModel>> GetAllFixtures(int pageNo, int pageSize);

        Task<PagedListViewModel<FixtureViewModel>> SearchFixturesByName(string name, int pageNo, int pageSize);
    }
}
