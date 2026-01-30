using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProLab.Areas.ProGym.Data;
using ProLab.Areas.ProGym.Models.DataModels;
using ProLab.Services.Time;
using System.Linq;
using System.Threading.Tasks;

namespace ProLab.Areas.ProGym.Controllers
{
    [Area("ProGym")]
    public class ProGymWorkOutsController : Controller
    {
        private readonly ProGymContext _context;

        public ProGymWorkOutsController(ProGymContext context)
        {
            _context = context;
        }

        // ===== LIST =====
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProGymWorkOuts
                .OrderByDescending(x => x.StartDate)
                .ToListAsync());
        }

        // ===== CREATE GET =====
        public IActionResult Create()
        {
            return View();
        }

        // ===== CREATE POST =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProGymWorkOut model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.StartDate = TimeService.LocalToUtc(model.StartDate.DateTime);
            model.EndDate = TimeService.LocalToUtc(model.EndDate.DateTime);

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ===== DETAILS =====
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.ProGymWorkOuts.FindAsync(id);
            return item == null ? NotFound() : View(item);
        }

        // ===== EDIT GET =====
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.ProGymWorkOuts.FindAsync(id);
            if (item == null)
                return NotFound();

            item.StartDate = TimeService.UtcToLocal(item.StartDate);
            item.EndDate = TimeService.UtcToLocal(item.EndDate);

            return View(item);
        }

        // ===== EDIT POST =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProGymWorkOut model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var entity = await _context.ProGymWorkOuts.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.StartDate = TimeService.LocalToUtc(model.StartDate.DateTime);
            entity.EndDate = TimeService.LocalToUtc(model.EndDate.DateTime);
            entity.Location = model.Location;
            entity.Description = model.Description;
            entity.Status = model.Status;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ===== DELETE =====
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ProGymWorkOuts.FindAsync(id);
            if (item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}