using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoriaImpacto>> GetAllAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<CategoriaImpacto?> GetByIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task<CategoriaImpacto> AddAsync(CategoriaImpacto categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task UpdateAsync(CategoriaImpacto categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
