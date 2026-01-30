using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProLab.Areas.HockeyData.Models.DataModels
{
    public class HockeyPlayersHockeyTeam
    {
        [Key]
        public int Id { get; set; }

        // 🔗 Player
        public int HockeyPlayerId { get; set; }

        [ForeignKey(nameof(HockeyPlayerId))]
        public HockeyPlayer HockeyPlayer { get; set; }

        // 🔗 Team
        public int HockeyTeamId { get; set; }

        [ForeignKey(nameof(HockeyTeamId))]
        public HockeyTeam HockeyTeam { get; set; }

        // 🧠 Redo för framtiden
        // public DateTime FromDate { get; set; }
        // public DateTime? ToDate { get; set; }
        // public string? Position { get; set; }
    }
}