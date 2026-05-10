using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class PersonaController : Controller
{
    private readonly IPersonaRepository _repo;

    public PersonaController(IPersonaRepository repo) => _repo = repo;

    public IActionResult Index() => View(_repo.GetAll());

    public IActionResult Details(int cc)
    {
        var persona = _repo.GetById(cc);
        return persona == null ? NotFound() : View(persona);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Persona persona)
    {
        if (!ModelState.IsValid) return View(persona);
        _repo.Create(persona);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int cc)
    {
        var persona = _repo.GetById(cc);
        return persona == null ? NotFound() : View(persona);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int cc, Persona persona)
    {
        if (cc != persona.Cc) return BadRequest();
        if (!ModelState.IsValid) return View(persona);
        _repo.Update(persona);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int cc)
    {
        var persona = _repo.GetById(cc);
        return persona == null ? NotFound() : View(persona);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int cc)
    {
        _repo.Delete(cc);
        return RedirectToAction(nameof(Index));
    }
}
