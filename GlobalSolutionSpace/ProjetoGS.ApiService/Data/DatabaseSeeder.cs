using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Services;

namespace ProjetoGS.ApiService.Data;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;
    private readonly IAuthService _authService;

    public DatabaseSeeder(AppDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task SeedAsync()
    {
        await _context.Database.MigrateAsync();

        if (!await _context.Usuarios.AnyAsync())
        {
            var adminUser = new Usuario
            {
                Nome = "Administrador Chefe",
                Email = "admin@novaeconomia.space",
                SenhaHash = _authService.HashPassword("Admin@123"),
                Perfil = "Administrador"
            };
            await _context.Usuarios.AddAsync(adminUser);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Categorias.AnyAsync())
        {
            var categorias = new List<CategoriaImpacto>
            {
                new CategoriaImpacto { Nome = "Saúde Global", Descricao = "Tecnologias que impactam tratamentos e diagnósticos" },
                new CategoriaImpacto { Nome = "Agricultura e Clima", Descricao = "Monitoramento e melhoria de safras" },
                new CategoriaImpacto { Nome = "Comunicações", Descricao = "Melhoria na transmissão de dados e internet" },
                new CategoriaImpacto { Nome = "Sustentabilidade", Descricao = "Gerenciamento de recursos naturais e energia" }
            };

            await _context.Categorias.AddRangeAsync(categorias);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Origens.AnyAsync())
        {
            var origens = new List<Origem>
            {
                new Origem { Nome = "Missão Apollo", Descricao = "Programa espacial americano que levou o homem à Lua." },
                new Origem { Nome = "Estação Espacial Internacional (ISS)", Descricao = "Laboratório espacial orbital colaborativo." },
                new Origem { Nome = "Satélites de Observação", Descricao = "Satélites em órbita da Terra usados para monitoramento." },
                new Origem { Nome = "Missão Artemis", Descricao = "Nova missão de exploração lunar da NASA." },
                new Origem { Nome = "Sondas Interplanetárias", Descricao = "Naves não tripuladas enviadas para explorar o sistema solar." },
                new Origem { Nome = "Outros", Descricao = "Outras origens espaciais." }
            };
            await _context.Origens.AddRangeAsync(origens);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Tecnologias.AnyAsync())
        {
            var categorias = await _context.Categorias.ToListAsync();
            var origens = await _context.Origens.ToListAsync();
            
            var tecnologias = new List<Tecnologia>
            {
                new Tecnologia 
                { 
                    Nome = "Termômetros Infravermelhos", 
                    Descricao = "Desenvolvidos inicialmente para medir a temperatura de estrelas.", 
                    OrigemId = origens.First(o => o.Nome == "Missão Apollo").Id,
                    CategoriaImpactoId = categorias.First(c => c.Nome == "Saúde Global").Id,
                    DataCadastro = DateTime.UtcNow.AddDays(-10)
                },
                new Tecnologia 
                { 
                    Nome = "Purificador de Água", 
                    Descricao = "Sistema de filtragem criado para reciclar água em missões espaciais.", 
                    OrigemId = origens.First(o => o.Nome == "Estação Espacial Internacional (ISS)").Id,
                    CategoriaImpactoId = categorias.First(c => c.Nome == "Sustentabilidade").Id,
                    DataCadastro = DateTime.UtcNow.AddDays(-5)
                },
                new Tecnologia 
                { 
                    Nome = "Sensores CMOS (Câmeras)", 
                    Descricao = "Câmeras miniaturizadas inicialmente para sondas espaciais.", 
                    OrigemId = origens.First(o => o.Nome == "Satélites de Observação").Id,
                    CategoriaImpactoId = categorias.First(c => c.Nome == "Comunicações").Id,
                    DataCadastro = DateTime.UtcNow.AddDays(-2)
                }
            };

            await _context.Tecnologias.AddRangeAsync(tecnologias);
            await _context.SaveChangesAsync();
        }
    }
}
