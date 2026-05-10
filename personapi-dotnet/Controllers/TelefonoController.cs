using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class TelefonoController : BaseController
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
        try
        {
            _repo.Create(telefono);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", DbErrorMessage(ex));
            return View(telefono);
        }
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
        try
        {
            _repo.Update(telefono);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", DbErrorMessage(ex));
            return View(telefono);
        }
    }

    public IActionResult Delete(string num)
    {
        var telefono = _repo.GetById(num);
        return telefono == null ? NotFound() : View(telefono);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string num)
    {
        try
        {
            _repo.Delete(num);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            var telefono = _repo.GetById(num);
            if (telefono == null) return NotFound();
            ModelState.AddModelError("", DbErrorMessage(ex));
            return View(telefono);
        }
    }
}
