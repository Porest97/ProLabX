using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ProLab.Areas.ProGym.Models.DataModels
{
    public class ProGymWorkOut
    {
        [Key]
        public int Id { get; set; }

        // ===== UTC I DB =====
        [Display(Name = "Start")]
        public DateTimeOffset StartDate { get; set; }

        [Display(Name = "Slut")]
        public DateTimeOffset EndDate { get; set; }

        // ===== LOKAL TID FÖR UI =====
        [NotMapped]
        public DateTime StartDateLocal => ConvertToLocal(StartDate);

        [NotMapped]
        public DateTime EndDateLocal => ConvertToLocal(EndDate);

        private static DateTime ConvertToLocal(DateTimeOffset utc)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "W. Europe Standard Time"
                    : "Europe/Stockholm"
            );

            return TimeZoneInfo.ConvertTimeFromUtc(utc.UtcDateTime, tz);
        }

        // ===== INFO =====
        [Required]
        public string Location { get; set; } = "";

        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Scheduled";
    }
}