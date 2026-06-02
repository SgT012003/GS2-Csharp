using Microsoft.AspNetCore.Mvc;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrigensController : ControllerBase
{
    private readonly IOrigemRepository _repository;

    public OrigensController(IOrigemRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var origens = await _repository.GetAllAsync();
        return Ok(origens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var origem = await _repository.GetByIdAsync(id);
        if (origem == null) return NotFound();
        return Ok(origem);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Origem origem)
    {
        await _repository.AddAsync(origem);
        return CreatedAtAction(nameof(GetById), new { id = origem.Id }, origem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Origem origem)
    {
        if (id != origem.Id) return BadRequest();
        await _repository.UpdateAsync(origem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
