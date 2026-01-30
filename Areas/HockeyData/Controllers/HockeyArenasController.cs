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
    public class HockeyArenasController : Controller
    {
        private readonly HockeyDataContext _context;

        public HockeyArenasController(HockeyDataContext context)
        {
            _context = context;
        }

        // GET: HockeyData/HockeyArenas
        public async Task<IActionResult> IndexHockeyArenas()
        {
            return View(await _context.HockeyArenas.ToListAsync());
        }

        // GET: HockeyData/HockeyArenas/Details/5
        public async Task<IActionResult> DetailsHockeyArena(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyArena = await _context.HockeyArenas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyArena == null)
            {
                return NotFound();
            }

            return View(hockeyArena);
        }

        // GET: HockeyData/HockeyArenas/Create
        public IActionResult CreateHockeyArena()
        {
            return View();
        }

        // POST: HockeyData/HockeyArenas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHockeyArena([Bind("Id,HockeyArenaName,Location,Capacity")] HockeyArena hockeyArena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hockeyArena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexHockeyArenas));
            }
            return View(hockeyArena);
        }

        // GET: HockeyData/HockeyArenas/Edit/5
        public async Task<IActionResult> EditHockeyArena(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyArena = await _context.HockeyArenas.FindAsync(id);
            if (hockeyArena == null)
            {
                return NotFound();
            }
            return View(hockeyArena);
        }

        // POST: HockeyData/HockeyArenas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHockeyArena(int id, [Bind("Id,HockeyArenaName,Location,Capacity")] HockeyArena hockeyArena)
        {
            if (id != hockeyArena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hockeyArena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HockeyArenaExists(hockeyArena.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexHockeyArenas));
            }
            return View(hockeyArena);
        }

        // GET: HockeyData/HockeyArenas/Delete/5
        public async Task<IActionResult> DeleteHockeyArena(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyArena = await _context.HockeyArenas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyArena == null)
            {
                return NotFound();
            }

            return View(hockeyArena);
        }

        // POST: HockeyData/HockeyArenas/Delete/5
        [HttpPost, ActionName("DeleteHockeyArena")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hockeyArena = await _context.HockeyArenas.FindAsync(id);
            if (hockeyArena != null)
            {
                _context.HockeyArenas.Remove(hockeyArena);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexHockeyArenas));
        }

        private bool HockeyArenaExists(int id)
        {
            return _context.HockeyArenas.Any(e => e.Id == id);
        }
    }
}
