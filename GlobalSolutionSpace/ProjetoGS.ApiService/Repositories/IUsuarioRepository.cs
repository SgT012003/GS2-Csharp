using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
}
