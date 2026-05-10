using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class EstudioApiController : ControllerBase
{
    private readonly IEstudioRepository _repo;

    public EstudioApiController(IEstudioRepository repo) => _repo = repo;

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{idProf}/{ccPer}")]
    public IActionResult GetById(int idProf, int ccPer)
    {
        var estudio = _repo.GetById(idProf, ccPer);
        return estudio == null ? NotFound() : Ok(estudio);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Estudio estudio)
    {
        _repo.Create(estudio);
        return CreatedAtAction(nameof(GetById), new { idProf = estudio.IdProf, ccPer = estudio.CcPer }, estudio);
    }

    [HttpPut("{idProf}/{ccPer}")]
    public IActionResult Update(int idProf, int ccPer, [FromBody] Estudio estudio)
    {
        if (idProf != estudio.IdProf || ccPer != estudio.CcPer) return BadRequest();
        _repo.Update(estudio);
        return NoContent();
    }

    [HttpDelete("{idProf}/{ccPer}")]
    public IActionResult Delete(int idProf, int ccPer)
    {
        _repo.Delete(idProf, ccPer);
        return NoContent();
    }
}
