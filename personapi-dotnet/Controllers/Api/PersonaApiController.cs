using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class PersonaApiController : ControllerBase
{
    private readonly IPersonaRepository _repo;

    public PersonaApiController(IPersonaRepository repo) => _repo = repo;

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{cc}")]
    public IActionResult GetById(int cc)
    {
        var persona = _repo.GetById(cc);
        return persona == null ? NotFound() : Ok(persona);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Persona persona)
    {
        _repo.Create(persona);
        return CreatedAtAction(nameof(GetById), new { cc = persona.Cc }, persona);
    }

    [HttpPut("{cc}")]
    public IActionResult Update(int cc, [FromBody] Persona persona)
    {
        if (cc != persona.Cc) return BadRequest();
        _repo.Update(persona);
        return NoContent();
    }

    [HttpDelete("{cc}")]
    public IActionResult Delete(int cc)
    {
        _repo.Delete(cc);
        return NoContent();
    }
}
