using System.ComponentModel.DataAnnotations;

namespace ProLab.Areas.HockeyData.Models.DataModels
{
    public class HockeyGameStatus
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Status")]
        public string HockeyGameStatusName { get; set; }
    }
}
