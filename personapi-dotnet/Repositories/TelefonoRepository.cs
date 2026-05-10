using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class TelefonoRepository : ITelefonoRepository
{
    private readonly PersonaDbContext _context;

    public TelefonoRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Telefono> GetAll() => _context.Telefonos.ToList();

    public Telefono? GetById(string num) => _context.Telefonos.Find(num);

    public void Create(Telefono telefono)
    {
        _context.Telefonos.Add(telefono);
        _context.SaveChanges();
    }

    public void Update(Telefono telefono)
    {
        _context.Telefonos.Update(telefono);
        _context.SaveChanges();
    }

    public void Delete(string num)
    {
        var telefono = _context.Telefonos.Find(num);
        if (telefono != null)
        {
            _context.Telefonos.Remove(telefono);
            _context.SaveChanges();
        }
    }
}
