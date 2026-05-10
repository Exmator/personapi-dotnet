using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class TelefonoController : Controller
{
    private readonly ITelefonoRepository _repo;

    public TelefonoController(ITelefonoRepository repo) => _repo = repo;

    public IActionResult Index() => View(_repo.GetAll());

    public IActionResult Details(string num)
    {
        var telefono = _repo.GetById(num);
        return telefono == null ? NotFound() : View(telefono);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Telefono telefono)
    {
        if (!ModelState.IsValid) return View(telefono);
        _repo.Create(telefono);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(string num)
    {
        var telefono = _repo.GetById(num);
        return telefono == null ? NotFound() : View(telefono);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(string num, Telefono telefono)
    {
        if (num != telefono.Num) return BadRequest();
        if (!ModelState.IsValid) return View(telefono);
        _repo.Update(telefono);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(string num)
    {
        var telefono = _repo.GetById(num);
        return telefono == null ? NotFound() : View(telefono);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string num)
    {
        _repo.Delete(num);
        return RedirectToAction(nameof(Index));
    }
}
