using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ProLab.Areas.HockeyData.Models.DataModels
{
    public class HockeyGame
    {
        [Key]
        public int Id { get; set; }

        // ===== UTC I DB =====
        [Display(Name = "Datum & Tid")]
        public DateTimeOffset GameDateTime { get; set; }

        // ===== LOKAL TID FÖR UI =====
        [NotMapped]
        public DateTime GameDateTimeLocal
        {
            get
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(
                    RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                        ? "W. Europe Standard Time"
                        : "Europe/Stockholm"
                );

                return TimeZoneInfo.ConvertTimeFromUtc(
                    GameDateTime.UtcDateTime,
                    tz
                );
            }
        }

        // ===== IDENT =====
        public string? GameNumber { get; set; }

        // ===== RELATIONER =====
        public int? HockeyGameStatusId { get; set; }
        public HockeyGameStatus? GameStatus { get; set; }

        public int? HockeySeriesId { get; set; }
        public HockeySeries? HockeySeries { get; set; }

        public int? HockeyArenaId { get; set; }
        public HockeyArena? HockeyArena { get; set; }

        public int? HockeyTeamId { get; set; }
        public HockeyTeam? HomeTeam { get; set; }

        public int? HockeyTeamId1 { get; set; }
        public HockeyTeam? AwayTeam { get; set; }

        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }

        [NotMapped]
        public string Result => $"{HomeTeamScore} - {AwayTeamScore}";
    }
}