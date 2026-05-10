using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ProfesionApiController : ControllerBase
{
    private readonly IProfesionRepository _repo;

    public ProfesionApiController(IProfesionRepository repo) => _repo = repo;

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var profesion = _repo.GetById(id);
        return profesion == null ? NotFound() : Ok(profesion);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Profesion profesion)
    {
        _repo.Create(profesion);
        return CreatedAtAction(nameof(GetById), new { id = profesion.Id }, profesion);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Profesion profesion)
    {
        if (id != profesion.Id) return BadRequest();
        _repo.Update(profesion);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        return NoContent();
    }
}
