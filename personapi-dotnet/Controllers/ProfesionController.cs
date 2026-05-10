using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class ProfesionController : Controller
{
    private readonly IProfesionRepository _repo;

    public ProfesionController(IProfesionRepository repo) => _repo = repo;

    public IActionResult Index() => View(_repo.GetAll());

    public IActionResult Details(int id)
    {
        var profesion = _repo.GetById(id);
        return profesion == null ? NotFound() : View(profesion);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Profesion profesion)
    {
        if (!ModelState.IsValid) return View(profesion);
        _repo.Create(profesion);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var profesion = _repo.GetById(id);
        return profesion == null ? NotFound() : View(profesion);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Profesion profesion)
    {
        if (id != profesion.Id) return BadRequest();
        if (!ModelState.IsValid) return View(profesion);
        _repo.Update(profesion);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var profesion = _repo.GetById(id);
        return profesion == null ? NotFound() : View(profesion);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _repo.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
