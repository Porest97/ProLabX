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
    public class HockeySeriesController : Controller
    {
        private readonly HockeyDataContext _context;

        public HockeySeriesController(HockeyDataContext context)
        {
            _context = context;
        }

        // GET: HockeyData/HockeySeries
        public async Task<IActionResult> Index()
        {
            return View(await _context.HockeySeries.ToListAsync());
        }

        // GET: HockeyData/HockeySeries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeySeries = await _context.HockeySeries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeySeries == null)
            {
                return NotFound();
            }

            return View(hockeySeries);
        }

        // GET: HockeyData/HockeySeries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HockeyData/HockeySeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HockeySeriesName,HockeySeriesShortDescription,HockeySeriesDescription,GameTime")] HockeySeries hockeySeries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hockeySeries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hockeySeries);
        }

        // GET: HockeyData/HockeySeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeySeries = await _context.HockeySeries.FindAsync(id);
            if (hockeySeries == null)
            {
                return NotFound();
            }
            return View(hockeySeries);
        }

        // POST: HockeyData/HockeySeries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HockeySeriesName,HockeySeriesShortDescription,HockeySeriesDescription,GameTime")] HockeySeries hockeySeries)
        {
            if (id != hockeySeries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hockeySeries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HockeySeriesExists(hockeySeries.Id))
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
            return View(hockeySeries);
        }

        // GET: HockeyData/HockeySeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hockeySeries = await _context.HockeySeries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hockeySeries == null)
            {
                return NotFound();
            }

            return View(hockeySeries);
        }

        // POST: HockeyData/HockeySeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hockeySeries = await _context.HockeySeries.FindAsync(id);
            if (hockeySeries != null)
            {
                _context.HockeySeries.Remove(hockeySeries);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HockeySeriesExists(int id)
        {
            return _context.HockeySeries.Any(e => e.Id == id);
        }
    }
}
