using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProLab.Areas.XLab.Data;
using ProLab.Areas.XLab.Models.DataModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProLab.Areas.XLab.Controllers
{
    [Area("XLab")]
    public class XLabVideosController : Controller
    {
        private readonly XLabContext _context;

        public XLabVideosController(XLabContext context)
        {
            _context = context;
        }

        // ================= LIST =================
        public async Task<IActionResult> Index()
        {
            return View(await _context.XLabVideos
                .OrderByDescending(x => x.UploadedAt)
                .ToListAsync());
        }

        // ================= CREATE =================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string? description, IFormFile videoFile)
        {
            if (videoFile == null || videoFile.Length == 0)
            {
                ModelState.AddModelError("", "Ingen videofil vald");
                return View();
            }

            var uploadsPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads/xlab/videos"
            );

            Directory.CreateDirectory(uploadsPath);

            var fileName = Path.GetRandomFileName() + Path.GetExtension(videoFile.FileName);
            var fullPath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            var video = new XLabVideo
            {
                Title = title,
                Description = description,
                FilePath = "/uploads/xlab/videos/" + fileName
            };

            _context.Add(video);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var video = await _context.XLabVideos.FindAsync(id);
            if (video == null) return NotFound();
            return View(video);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.XLabVideos.FindAsync(id);
            if (video == null) return NotFound();

            // radera filen
            if (!string.IsNullOrEmpty(video.FilePath))
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", video.FilePath);
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }

            _context.XLabVideos.Remove(video);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool XLabVideoExists(int id)
        {
            return _context.XLabVideos.Any(e => e.Id == id);
        }

        // ================= WATCH =================
        public async Task<IActionResult> Watch(int id)
        {
            var video = await _context.XLabVideos.FindAsync(id);
            if (video == null) return NotFound();

            return View(video);
        }
    }
}