using System.ComponentModel.DataAnnotations;

namespace ProLab.Areas.HockeyData.Models.DataModels
{
    public class HockeyArena
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Arena")]
        public string HockeyArenaName { get; set; }

        [Display(Name = "Ort")]
        public string Location { get; set; }

        [Display(Name = "Kapacitet")]
        public int? Capacity { get; set; }
    }
}