using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public class OrigemRepository : IOrigemRepository
{
    private readonly AppDbContext _context;

    public OrigemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Origem>> GetAllAsync()
    {
        return await _context.Origens.ToListAsync();
    }

    public async Task<Origem?> GetByIdAsync(int id)
    {
        return await _context.Origens.FindAsync(id);
    }

    public async Task AddAsync(Origem origem)
    {
        await _context.Origens.AddAsync(origem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Origem origem)
    {
        _context.Origens.Update(origem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var origem = await _context.Origens.FindAsync(id);
        if (origem != null)
        {
            _context.Origens.Remove(origem);
            await _context.SaveChangesAsync();
        }
    }
}
