using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces;

public interface IEstudioRepository
{
    IEnumerable<Estudio> GetAll();
    Estudio? GetById(int idProf, int ccPer);
    void Create(Estudio estudio);
    void Update(Estudio estudio);
    void Delete(int idProf, int ccPer);
}
