using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProLab.Areas.HockeyData.Data;
using ProLab.Areas.HockeyData.Models.DataModels;
using ProLab.Services.Time;
using System.Linq;
using System.Threading.Tasks;

namespace ProLab.Areas.HockeyData.Controllers
{
    [Area("HockeyData")]
    public class HockeyGamesController : Controller
    {
        private readonly HockeyDataContext _context;

        public HockeyGamesController(HockeyDataContext context)
        {
            _context = context;
        }

        // ===== LIST + SEARCH =====
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.HockeyGames
                .Include(g => g.GameStatus)
                .Include(g => g.HockeySeries)
                .Include(g => g.HockeyArena)
                .Include(g => g.HomeTeam)
                .Include(g => g.AwayTeam)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(g =>
                    g.GameNumber!.Contains(searchString) ||
                    g.HomeTeam!.HockeyTeamName.Contains(searchString) ||
                    g.AwayTeam!.HockeyTeamName.Contains(searchString) ||
                    g.HockeyArena!.HockeyArenaName.Contains(searchString) ||
                    g.HockeySeries!.HockeySeriesName.Contains(searchString)
                );
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await query.OrderBy(g => g.GameDateTime).ToListAsync());
        }

        // ===== DETAILS =====
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.HockeyGames
                .Include(g => g.GameStatus)
                .Include(g => g.HockeySeries)
                .Include(g => g.HockeyArena)
                .Include(g => g.HomeTeam)
                .Include(g => g.AwayTeam)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        // ===== CREATE =====
        public IActionResult Create()
        {
            PopulateDropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HockeyGame model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns(model);
                return View(model);
            }

            model.GameDateTime = TimeService.LocalToUtc(model.GameDateTime.DateTime);

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ===== EDIT =====
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.HockeyGames.FindAsync(id);
            if (game == null) return NotFound();

            game.GameDateTime = TimeService.UtcToLocal(game.GameDateTime);

            PopulateDropDowns(game);
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HockeyGame model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                PopulateDropDowns(model);
                return View(model);
            }

            var entity = await _context.HockeyGames.FindAsync(id);
            if (entity == null) return NotFound();

            entity.GameDateTime = TimeService.LocalToUtc(model.GameDateTime.DateTime);
            entity.GameNumber = model.GameNumber;
            entity.HockeyGameStatusId = model.HockeyGameStatusId;
            entity.HockeySeriesId = model.HockeySeriesId;
            entity.HockeyArenaId = model.HockeyArenaId;
            entity.HockeyTeamId = model.HockeyTeamId;
            entity.HockeyTeamId1 = model.HockeyTeamId1;
            entity.HomeTeamScore = model.HomeTeamScore;
            entity.AwayTeamScore = model.AwayTeamScore;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ===== DELETE =====
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var game = await _context.HockeyGames
                .Include(g => g.HomeTeam)
                .Include(g => g.AwayTeam)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null) return NotFound();

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.HockeyGames.FindAsync(id);
            if (game != null)
            {
                _context.HockeyGames.Remove(game);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ===== DROPDOWNS =====
        private void PopulateDropDowns(HockeyGame? game = null)
        {
            ViewData["HockeyTeamId"] =
                new SelectList(_context.HockeyTeams, "Id", "HockeyTeamName", game?.HockeyTeamId);

            ViewData["HockeyTeamId1"] =
                new SelectList(_context.HockeyTeams, "Id", "HockeyTeamName", game?.HockeyTeamId1);

            ViewData["HockeyGameStatusId"] =
                new SelectList(_context.HockeyGameStatuses, "Id", "HockeyGameStatusName", game?.HockeyGameStatusId);

            ViewData["HockeyArenaId"] =
                new SelectList(_context.HockeyArenas, "Id", "HockeyArenaName", game?.HockeyArenaId);

            ViewData["HockeySeriesId"] =
                new SelectList(_context.HockeySeries, "Id", "HockeySeriesName", game?.HockeySeriesId);
        }
    }
}