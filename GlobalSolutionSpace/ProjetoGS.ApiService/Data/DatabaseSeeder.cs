using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        if (!await context.Categorias.AnyAsync())
        {
            var categorias = new List<CategoriaImpacto>
            {
                new CategoriaImpacto { Nome = "Saúde Global", Descricao = "Tecnologias que impactam tratamentos e diagnósticos" },
                new CategoriaImpacto { Nome = "Agricultura e Clima", Descricao = "Monitoramento e melhoria de safras" },
                new CategoriaImpacto { Nome = "Comunicações", Descricao = "Melhoria na transmissão de dados e internet" },
                new CategoriaImpacto { Nome = "Sustentabilidade", Descricao = "Gerenciamento de recursos naturais e energia" }
            };

            await context.Categorias.AddRangeAsync(categorias);
            await context.SaveChangesAsync();
        }

        if (!await context.Tecnologias.AnyAsync())
        {
            var categorias = await context.Categorias.ToListAsync();
            
            var tecnologias = new List<Tecnologia>
            {
                new Tecnologia 
                { 
                    Nome = "Termômetros Infravermelhos", 
                    Descricao = "Desenvolvidos inicialmente para medir a temperatura de estrelas.", 
                    OrigemEspacial = "Missão Apollo",
                    CategoriaImpactoId = categorias.First(c => c.Nome == "Saúde Global").Id,
                    DataCadastro = DateTime.UtcNow.AddDays(-10)
                },
                new Tecnologia 
                { 
                    Nome = "Purificador de Água", 
                    Descricao = "Sistema de filtragem criado para reciclar água em missões espaciais.", 
                    OrigemEspacial = "Estação Espacial Internacional (ISS)",
                    CategoriaImpactoId = categorias.First(c => c.Nome == "Sustentabilidade").Id,
                    DataCadastro = DateTime.UtcNow.AddDays(-5)
                },
                new Tecnologia 
                { 
                    Nome = "Sensores CMOS (Câmeras)", 
                    Descricao = "Câmeras miniaturizadas inicialmente para sondas espaciais.", 
                    OrigemEspacial = "Satélites de Observação",
                    CategoriaImpactoId = categorias.First(c => c.Nome == "Comunicações").Id,
                    DataCadastro = DateTime.UtcNow.AddDays(-2)
                }
            };

            await context.Tecnologias.AddRangeAsync(tecnologias);
            await context.SaveChangesAsync();
        }
    }
}
