using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data.Data.DTO
{
    public class PlayerStats
    {
        public Team Team { get; set; }
        public PlayerGame Games { get; set; }
        public PlayerGoal Goals { get; set; }

        public League League { get; set; }
    }
}
