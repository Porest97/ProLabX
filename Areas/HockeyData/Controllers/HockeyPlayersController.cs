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
    public class HockeyPlayersController : Controller
    {
        private readonly HockeyDataContext _context;

        public HockeyPlayersController(HockeyDataContext context)
        {
            _context = context;
        }

        // GET: HockeyData/HockeyPlayers
        public async Task<IActionResult> Index()
        {
            return View(await _context.HockeyPlayers.ToListAsync());
        }

        // GET: HockeyData/HockeyPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyPlayer = await _context.HockeyPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyPlayer == null)
            {
                return NotFound();
            }

            return View(hockeyPlayer);
        }

        // GET: HockeyData/HockeyPlayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HockeyData/HockeyPlayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,SSN,Age")] HockeyPlayer hockeyPlayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hockeyPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hockeyPlayer);
        }

        // GET: HockeyData/HockeyPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyPlayer = await _context.HockeyPlayers.FindAsync(id);
            if (hockeyPlayer == null)
            {
                return NotFound();
            }
            return View(hockeyPlayer);
        }

        // POST: HockeyData/HockeyPlayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,SSN,Age")] HockeyPlayer hockeyPlayer)
        {
            if (id != hockeyPlayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hockeyPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HockeyPlayerExists(hockeyPlayer.Id))
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
            return View(hockeyPlayer);
        }

        // GET: HockeyData/HockeyPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyPlayer = await _context.HockeyPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyPlayer == null)
            {
                return NotFound();
            }

            return View(hockeyPlayer);
        }

        // POST: HockeyData/HockeyPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hockeyPlayer = await _context.HockeyPlayers.FindAsync(id);
            if (hockeyPlayer != null)
            {
                _context.HockeyPlayers.Remove(hockeyPlayer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HockeyPlayerExists(int id)
        {
            return _context.HockeyPlayers.Any(e => e.Id == id);
        }
    }
}
