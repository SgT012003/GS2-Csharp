using Microsoft.AspNetCore.Mvc;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repository;

    public CategoriasController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categorias = await _repository.GetAllAsync();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null) return NotFound();
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoriaImpacto categoria)
    {
        await _repository.AddAsync(categoria);
        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoriaImpacto categoria)
    {
        if (id != categoria.Id) return BadRequest();
        await _repository.UpdateAsync(categoria);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
