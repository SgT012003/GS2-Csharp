using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Tecnologia> Tecnologias { get; set; }
    public DbSet<CategoriaImpacto> Categorias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Origem> Origens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
