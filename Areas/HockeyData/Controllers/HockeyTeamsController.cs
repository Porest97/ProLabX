using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProLab.Areas.HockeyData.Data;
using ProLab.Areas.HockeyData.Models.DataModels;

namespace ProLab.Areas.HockeyData.Controllers
{
    [Area("HockeyData")]
    public class HockeyTeamsController : Controller
    {
        private readonly HockeyDataContext _context;

        public HockeyTeamsController(HockeyDataContext context)
        {
            _context = context;
        }

        // GET: HockeyData/HockeyTeams
        public async Task<IActionResult> Index()
        {
            var hockeyDataContext = _context.HockeyTeams.Include(h => h.HomeArena);
            return View(await hockeyDataContext.ToListAsync());
        }

        // GET: HockeyData/HockeyTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyTeam = await _context.HockeyTeams
                .Include(h => h.HomeArena)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyTeam == null)
            {
                return NotFound();
            }

            return View(hockeyTeam);
        }

        // GET: HockeyData/HockeyTeams/Create
        public IActionResult Create()
        {
            ViewData["HockeyArenaId"] = new SelectList(_context.HockeyArenas, "Id", "HockeyArenaName");
            return View();
        }

        // POST: HockeyData/HockeyTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HockeyTeamName,CoachName,FoundedYear,HockeyArenaId,HockeyTeamBadgePath")] HockeyTeam hockeyTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hockeyTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HockeyArenaId"] = new SelectList(_context.HockeyArenas, "Id", "HockeyArenaName", hockeyTeam.HockeyArenaId);
            return View(hockeyTeam);
        }

        // GET: HockeyData/HockeyTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyTeam = await _context.HockeyTeams.FindAsync(id);
            if (hockeyTeam == null)
            {
                return NotFound();
            }
            ViewData["HockeyArenaId"] = new SelectList(_context.HockeyArenas, "Id", "HockeyArenaName", hockeyTeam.HockeyArenaId);
            return View(hockeyTeam);
        }

        // POST: HockeyData/HockeyTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HockeyTeamName,CoachName,FoundedYear,HockeyArenaId,HockeyTeamBadgePath")] HockeyTeam hockeyTeam)
        {
            if (id != hockeyTeam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hockeyTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HockeyTeamExists(hockeyTeam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HockeyArenaId"] = new SelectList(_context.HockeyArenas, "Id", "HockeyArenaName", hockeyTeam.HockeyArenaId);
            return View(hockeyTeam);
        }

        // GET: HockeyData/HockeyTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyTeam = await _context.HockeyTeams
                .Include(h => h.HomeArena)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyTeam == null)
            {
                return NotFound();
            }

            return View(hockeyTeam);
        }

        // POST: HockeyData/HockeyTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hockeyTeam = await _context.HockeyTeams.FindAsync(id);
            if (hockeyTeam != null)
            {
                _context.HockeyTeams.Remove(hockeyTeam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HockeyTeamExists(int id)
        {
            return _context.HockeyTeams.Any(e => e.Id == id);
        }
    }
}
