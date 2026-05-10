using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class PersonaRepository : IPersonaRepository
{
    private readonly PersonaDbContext _context;

    public PersonaRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Persona> GetAll() => _context.Personas.ToList();

    public Persona? GetById(int cc) => _context.Personas.Find(cc);

    public void Create(Persona persona)
    {
        _context.Personas.Add(persona);
        _context.SaveChanges();
    }

    public void Update(Persona persona)
    {
        _context.Personas.Update(persona);
        _context.SaveChanges();
    }

    public void Delete(int cc)
    {
        var persona = _context.Personas.Find(cc);
        if (persona != null)
        {
            _context.Personas.Remove(persona);
            _context.SaveChanges();
        }
    }
}
