using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public interface ITecnologiaRepository
{
    Task<IEnumerable<Tecnologia>> GetAllAsync();
    Task<Tecnologia?> GetByIdAsync(int id);
    Task<Tecnologia> AddAsync(Tecnologia tecnologia);
    Task UpdateAsync(Tecnologia tecnologia);
    Task DeleteAsync(int id);
    Task<IEnumerable<Tecnologia>> GetStatsAsync();
}
