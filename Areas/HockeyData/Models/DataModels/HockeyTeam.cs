using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProLab.Areas.HockeyData.Models.DataModels
{
    public class HockeyTeam
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Team")]
        public string HockeyTeamName { get; set; }

        [Display(Name = "Coach")]
        public string CoachName { get; set; }

        [Display(Name = "Grundat")]
        public int FoundedYear { get; set; }

        [Display(Name = "Hem Arena")]
        public int? HockeyArenaId { get; set; }

        [ForeignKey("HockeyArenaId")]
        public HockeyArena HomeArena { get; set; }

        [Display(Name = "Lagmärke")]
        public string HockeyTeamBadgePath { get; set; } = null;

        // Upload – sparas inte i DB
        [NotMapped]
        [Display(Name = "Ladda upp lagmärke")]
        public IFormFile HockeyTeamBadgeFile { get; set; } = null;

        public ICollection<HockeyPlayersHockeyTeam> HockeyPlayers { get; set; }
        = new List<HockeyPlayersHockeyTeam>();
    }
}