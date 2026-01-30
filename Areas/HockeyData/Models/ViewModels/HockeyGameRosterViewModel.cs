using ProLab.Areas.HockeyData.Models.DataModels;
using System.Collections.Generic;

namespace ProLab.Areas.HockeyData.Models.ViewModels
{
    public class HockeyGameRosterViewModel
    {
        public int HockeyGameId { get; set; }
        public int HockeyTeamId { get; set; }

        public string TeamName { get; set; }

        public List<RosterPlayerRow> Players { get; set; } = new();
        public List<RosterPlayerRow> AvailablePlayers { get; set; } = new();
        public HockeyTeam HomeTeam { get; internal set; }
        public HockeyTeam AwayTeam { get; internal set; }
        public List<HockeyGameRoster> Roster { get; internal set; }
    }
    public class RosterPlayerRow
    {
        public int HockeyPlayerId { get; set; }
        public string PlayerName { get; set; }

        public bool IsStarting { get; set; }
        public bool IsGoalie { get; set; }
        public int? JerseyNumber { get; set; }
    }
}