using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class ProfesionRepository : IProfesionRepository
{
    private readonly PersonaDbContext _context;

    public ProfesionRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Profesion> GetAll() => _context.Profesions.ToList();

    public Profesion? GetById(int id) => _context.Profesions.Find(id);

    public void Create(Profesion profesion)
    {
        _context.Profesions.Add(profesion);
        _context.SaveChanges();
    }

    public void Update(Profesion profesion)
    {
        _context.Profesions.Update(profesion);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var profesion = _context.Profesions.Find(id);
        if (profesion != null)
        {
            _context.Profesions.Remove(profesion);
            _context.SaveChanges();
        }
    }
}
