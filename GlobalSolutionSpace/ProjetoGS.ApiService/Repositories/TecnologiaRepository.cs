using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public class TecnologiaRepository : ITecnologiaRepository
{
    private readonly AppDbContext _context;

    public TecnologiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tecnologia>> GetAllAsync()
    {
        return await _context.Tecnologias.Include(t => t.CategoriaImpacto).Include(t => t.Origem).ToListAsync();
    }

    public async Task<Tecnologia?> GetByIdAsync(int id)
    {
        return await _context.Tecnologias.Include(t => t.CategoriaImpacto).Include(t => t.Origem).FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tecnologia> AddAsync(Tecnologia tecnologia)
    {
        _context.Tecnologias.Add(tecnologia);
        await _context.SaveChangesAsync();
        return tecnologia;
    }

    public async Task UpdateAsync(Tecnologia tecnologia)
    {
        _context.Entry(tecnologia).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tecnologia = await _context.Tecnologias.FindAsync(id);
        if (tecnologia != null)
        {
            _context.Tecnologias.Remove(tecnologia);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Tecnologia>> GetStatsAsync()
    {
        return await _context.Tecnologias.Include(t => t.CategoriaImpacto).Include(t => t.Origem).ToListAsync();
    }
}
