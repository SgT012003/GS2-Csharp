using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public interface IOrigemRepository
{
    Task<IEnumerable<Origem>> GetAllAsync();
    Task<Origem?> GetByIdAsync(int id);
    Task AddAsync(Origem origem);
    Task UpdateAsync(Origem origem);
    Task DeleteAsync(int id);
}
