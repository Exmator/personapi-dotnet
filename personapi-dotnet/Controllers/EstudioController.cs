using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers;

public class EstudioController : Controller
{
    private readonly IEstudioRepository _repo;

    public EstudioController(IEstudioRepository repo) => _repo = repo;

    public IActionResult Index() => View(_repo.GetAll());

    public IActionResult Details(int idProf, int ccPer)
    {
        var estudio = _repo.GetById(idProf, ccPer);
        return estudio == null ? NotFound() : View(estudio);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Estudio estudio)
    {
        if (!ModelState.IsValid) return View(estudio);
        _repo.Create(estudio);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int idProf, int ccPer)
    {
        var estudio = _repo.GetById(idProf, ccPer);
        return estudio == null ? NotFound() : View(estudio);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(int idProf, int ccPer, Estudio estudio)
    {
        if (idProf != estudio.IdProf || ccPer != estudio.CcPer) return BadRequest();
        if (!ModelState.IsValid) return View(estudio);
        _repo.Update(estudio);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int idProf, int ccPer)
    {
        var estudio = _repo.GetById(idProf, ccPer);
        return estudio == null ? NotFound() : View(estudio);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int idProf, int ccPer)
    {
        _repo.Delete(idProf, ccPer);
        return RedirectToAction(nameof(Index));
    }
}
