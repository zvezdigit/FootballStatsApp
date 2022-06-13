﻿using FootballMatchesWebApp.Application.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Application.Interfaces
{
    public interface ITeamService
    {
        IEnumerable<TeamViewModel> GetAllTeams();

        IEnumerable<TeamViewModel> SearchTeamsByName(string name);

        IEnumerable<TeamPlayerViewModel> TeamPlayers(int teamId);

        IEnumerable<TeamPlayerViewModel> SearchPlayerByName(int teamId, string name);

        TeamPlayerStatsViewModel GetTeamPlayerStats(int playerId);

        IEnumerable<TopScorersViewModel> TopScorers();

        IEnumerable<TopTeamViewModel> TopTeams();




    }
}
