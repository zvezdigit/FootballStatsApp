using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Models.Teams
{
    public class TeamPlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string? Nationality { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public bool? Injured { get; set; }


    }
}
