using Microsoft.AspNetCore.Mvc;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TecnologiasController : ControllerBase
{
    private readonly ITecnologiaRepository _repository;

    public TecnologiasController(ITecnologiaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tecnologias = await _repository.GetAllAsync();
        return Ok(tecnologias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tecnologia = await _repository.GetByIdAsync(id);
        if (tecnologia == null) return NotFound();
        return Ok(tecnologia);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tecnologia tecnologia)
    {
        await _repository.AddAsync(tecnologia);
        return CreatedAtAction(nameof(GetById), new { id = tecnologia.Id }, tecnologia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tecnologia tecnologia)
    {
        if (id != tecnologia.Id) return BadRequest();
        await _repository.UpdateAsync(tecnologia);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var tecnologias = await _repository.GetAllAsync();
        
        var stats = new
        {
            TotalTecnologias = tecnologias.Count(),
            PorSetor = tecnologias.GroupBy(t => t.CategoriaImpacto?.Nome ?? "Sem Categoria")
                                  .Select(g => new { Setor = g.Key, Quantidade = g.Count() }),
            UltimasCadastradas = tecnologias.OrderByDescending(t => t.DataCadastro).Take(5).Select(t => new
            {
                t.Id,
                t.Nome,
                OrigemNome = t.Origem?.Nome ?? "Desconhecido",
                t.DataCadastro
            })
        };

        return Ok(stats);
    }
}
