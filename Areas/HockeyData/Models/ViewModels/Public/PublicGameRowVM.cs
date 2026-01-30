using System;

namespace ProLab.Areas.HockeyData.ViewModels.Public
{
    public class PublicGameRowVM
    {
        public DateTimeOffset GameTime { get; set; }

        public string HomeTeam { get; set; } = "";
        public string HomeTeamBadgePath { get; set; } = "";

        public string AwayTeam { get; set; } = "";
        public string AwayTeamBadgePath { get; set; } = "";

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public string Arena { get; set; } = "";
        public string Status { get; set; } = "";
    }
}