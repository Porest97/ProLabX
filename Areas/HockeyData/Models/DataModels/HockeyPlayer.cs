using ProLab.Models.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProLab.Areas.HockeyData.Models.DataModels
{
    public class HockeyPlayer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        [Display(Name = "Namn")]
        public string FullName { get { return string.Format("{0} {1} {2}", FirstName, " ", LastName); } }

        [Display(Name = "Född")]
        public string SSN { get; set; }

        [Display(Name = "Ålder")]
        public string Age { get; set; }       
    }
}