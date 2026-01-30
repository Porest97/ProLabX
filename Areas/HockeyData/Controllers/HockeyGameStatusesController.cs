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
    public class HockeyGameStatusesController : Controller
    {
        private readonly HockeyDataContext _context;

        public HockeyGameStatusesController(HockeyDataContext context)
        {
            _context = context;
        }

        // GET: HockeyData/HockeyGameStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.HockeyGameStatuses.ToListAsync());
        }

        // GET: HockeyData/HockeyGameStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyGameStatus = await _context.HockeyGameStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyGameStatus == null)
            {
                return NotFound();
            }

            return View(hockeyGameStatus);
        }

        // GET: HockeyData/HockeyGameStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HockeyData/HockeyGameStatuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HockeyGameStatusName")] HockeyGameStatus hockeyGameStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hockeyGameStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hockeyGameStatus);
        }

        // GET: HockeyData/HockeyGameStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyGameStatus = await _context.HockeyGameStatuses.FindAsync(id);
            if (hockeyGameStatus == null)
            {
                return NotFound();
            }
            return View(hockeyGameStatus);
        }

        // POST: HockeyData/HockeyGameStatuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HockeyGameStatusName")] HockeyGameStatus hockeyGameStatus)
        {
            if (id != hockeyGameStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hockeyGameStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HockeyGameStatusExists(hockeyGameStatus.Id))
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
            return View(hockeyGameStatus);
        }

        // GET: HockeyData/HockeyGameStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeyGameStatus = await _context.HockeyGameStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeyGameStatus == null)
            {
                return NotFound();
            }

            return View(hockeyGameStatus);
        }

        // POST: HockeyData/HockeyGameStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hockeyGameStatus = await _context.HockeyGameStatuses.FindAsync(id);
            if (hockeyGameStatus != null)
            {
                _context.HockeyGameStatuses.Remove(hockeyGameStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HockeyGameStatusExists(int id)
        {
            return _context.HockeyGameStatuses.Any(e => e.Id == id);
        }
    }
}
