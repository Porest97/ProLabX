using System.ComponentModel.DataAnnotations;

namespace ProLab.Areas.HockeyData.Models.DataModels
{
    public class HockeySeries
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Serie")]
        public string HockeySeriesName { get; set; }
        [Display(Name = "Beskrivning")]
        public string HockeySeriesShortDescription { get; set; }
        [Display(Name = "Detaljerad Beskrivning")]
        public string HockeySeriesDescription { get; set; }
        [Display(Name = "Speltid")]
        public string GameTime { get; set; }
    }
}
