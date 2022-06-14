using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Models
{
    public class PagedListViewModel<T>
    {
        public int PageNo { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
