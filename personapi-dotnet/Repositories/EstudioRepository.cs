using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories;

public class EstudioRepository : IEstudioRepository
{
    private readonly PersonaDbContext _context;

    public EstudioRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Estudio> GetAll() => _context.Estudios.ToList();

    public Estudio? GetById(int idProf, int ccPer) => _context.Estudios.Find(idProf, ccPer);

    public void Create(Estudio estudio)
    {
        _context.Estudios.Add(estudio);
        _context.SaveChanges();
    }

    public void Update(Estudio estudio)
    {
        _context.Estudios.Update(estudio);
        _context.SaveChanges();
    }

    public void Delete(int idProf, int ccPer)
    {
        var estudio = _context.Estudios.Find(idProf, ccPer);
        if (estudio != null)
        {
            _context.Estudios.Remove(estudio);
            _context.SaveChanges();
        }
    }
}
