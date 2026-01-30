namespace ProLab.Areas.HockeyData.ViewModels.Public
{
    public class PublicStandingsRowVM
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = "";
        public string? BadgePath { get; set; }

        public int GamesPlayed { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDiff => GoalsFor - GoalsAgainst;
        public int Points { get; set; }
    }
}