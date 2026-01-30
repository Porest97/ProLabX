using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProLab.Areas.ProGym.Data;
using ProLab.Areas.ProGym.Models.DataModels;
using System.Threading.Tasks;

[Area("ProGym")]
public class ProGymExercisesController : Controller
{
    private readonly ProGymContext _context;

    public ProGymExercisesController(ProGymContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
        => View(await _context.ProGymExercises.ToListAsync());

    public IActionResult Create()
        => View();

    [HttpPost]
    public async Task<IActionResult> Create(ProGymExercise model)
    {
        if (!ModelState.IsValid) return View(model);

        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
        => View(await _context.ProGymExercises.FindAsync(id));
}