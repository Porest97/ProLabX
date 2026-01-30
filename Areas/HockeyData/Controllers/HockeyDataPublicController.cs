using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProLab.Areas.HockeyData.Services;
using System.Threading.Tasks;

namespace ProLab.Areas.HockeyData.Controllers
{
    [Area("HockeyData")]
    [AllowAnonymous]
    public class HockeyDataPublicController : Controller
    {
        private readonly HockeyDataStatsService _stats;

        public HockeyDataPublicController(HockeyDataStatsService stats)
        {
            _stats = stats;
        }

        // ===============================
        // MATCHER (RÖRS EJ)
        // ===============================
        public async Task<IActionResult> IndexHockeyGamesNHL()
        {
            var games = await _stats.GetPublicGamesAsync();
            return View(games); // -> IndexHockeyGamesNHL.cshtml
        }

        // ===============================
        // STANDINGS (NY)
        // ===============================
        public async Task<IActionResult> Standings()
        {
            var standings = await _stats.GetStandingsAsync();
            return View(standings); // -> Standings.cshtml
        }
    }
}