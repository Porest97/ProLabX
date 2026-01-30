using ProLab.Areas.HockeyData.Models.DataModels;

public class HockeyGameRoster
{
    public int Id { get; set; }

    // Match
    public int HockeyGameId { get; set; }
    public HockeyGame HockeyGame { get; set; }

    // Lag (Hemma / Borta)
    public int HockeyTeamId { get; set; }
    public HockeyTeam HockeyTeam { get; set; }

    // Spelare
    public int HockeyPlayerId { get; set; }
    public HockeyPlayer HockeyPlayer { get; set; }

    // Matchspecifikt (redo för framtiden)
    public bool IsStarting { get; set; }      // startelva
    public bool IsGoalie { get; set; }         // målvakt
    public int? JerseyNumber { get; set; }     // tröjnummer i matchen
}