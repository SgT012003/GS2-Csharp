using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<CategoriaImpacto>> GetAllAsync();
    Task<CategoriaImpacto?> GetByIdAsync(int id);
    Task<CategoriaImpacto> AddAsync(CategoriaImpacto categoria);
    Task UpdateAsync(CategoriaImpacto categoria);
    Task DeleteAsync(int id);
}
