using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class ProfesionController : BaseController
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
        try
        {
            _repo.Create(profesion);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", DbErrorMessage(ex));
            return View(profesion);
        }
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
        try
        {
            _repo.Update(profesion);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", DbErrorMessage(ex));
            return View(profesion);
        }
    }

    public IActionResult Delete(int id)
    {
        var profesion = _repo.GetById(id);
        return profesion == null ? NotFound() : View(profesion);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            var profesion = _repo.GetById(id);
            if (profesion == null) return NotFound();
            ModelState.AddModelError("", DbErrorMessage(ex));
            return View(profesion);
        }
    }
}
