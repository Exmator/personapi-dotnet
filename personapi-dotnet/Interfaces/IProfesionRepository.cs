using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

public interface IProfesionRepository
{
    IEnumerable<Profesion> GetAll();
    Profesion? GetById(int id);
    void Create(Profesion profesion);
    void Update(Profesion profesion);
    void Delete(int id);
}
