using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Models.Teams
{
    public class TopScorersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Nationality { get; set; }

        public int? GoalsScored { get; set; }
    }
}
