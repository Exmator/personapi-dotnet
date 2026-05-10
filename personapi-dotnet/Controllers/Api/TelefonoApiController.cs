using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class TelefonoApiController : ControllerBase
{
    private readonly ITelefonoRepository _repo;

    public TelefonoApiController(ITelefonoRepository repo) => _repo = repo;

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{num}")]
    public IActionResult GetById(string num)
    {
        var telefono = _repo.GetById(num);
        return telefono == null ? NotFound() : Ok(telefono);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Telefono telefono)
    {
        _repo.Create(telefono);
        return CreatedAtAction(nameof(GetById), new { num = telefono.Num }, telefono);
    }

    [HttpPut("{num}")]
    public IActionResult Update(string num, [FromBody] Telefono telefono)
    {
        if (num != telefono.Num) return BadRequest();
        _repo.Update(telefono);
        return NoContent();
    }

    [HttpDelete("{num}")]
    public IActionResult Delete(string num)
    {
        _repo.Delete(num);
        return NoContent();
    }
}
