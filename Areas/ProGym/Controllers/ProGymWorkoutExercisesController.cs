using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProLab.Areas.ProGym.Data;
using ProLab.Areas.ProGym.Models.DataModels;
using System.Threading.Tasks;

[Area("ProGym")]
public class ProGymWorkoutExercisesController : Controller
{
    private readonly ProGymContext _context;

    public ProGymWorkoutExercisesController(ProGymContext context)
    {
        _context = context;
    }

    public IActionResult Create(int workoutId)
    {
        ViewData["WorkoutId"] = workoutId;
        ViewData["Exercises"] =
            new SelectList(_context.ProGymExercises, "Id", "ProGymExerciseName");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProGymWorkoutExercise model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction(
            "Edit",
            "ProGymWorkOuts",
            new { id = model.ProGymWorkOutId }
        );
    }
}