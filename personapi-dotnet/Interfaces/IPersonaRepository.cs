using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

public interface IPersonaRepository
{
    IEnumerable<Persona> GetAll();
    Persona? GetById(int cc);
    void Create(Persona persona);
    void Update(Persona persona);
    void Delete(int cc);
}
