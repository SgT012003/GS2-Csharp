using Microsoft.AspNetCore.Mvc;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories;
using ProjetoGS.ApiService.Services;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioRepository _repository;
    private readonly IAuthService _authService;

    public UsuariosController(IUsuarioRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _repository.GetAllAsync();
        return Ok(usuarios.Select(u => new { u.Id, u.Nome, u.Email, u.Perfil })); // Omitir SenhaHash
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = await _repository.GetByEmailAsync(request.Email);
        if (usuario == null)
            return Unauthorized(new { message = "Credenciais inválidas." });

        if (!_authService.VerifyPassword(request.Senha, usuario.SenhaHash))
            return Unauthorized(new { message = "Credenciais inválidas." });

        return Ok(new { usuario.Id, usuario.Nome, usuario.Email, usuario.Perfil });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (await _repository.GetByEmailAsync(request.Email) != null)
            return BadRequest(new { message = "Email já em uso." });

        var usuario = new Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = _authService.HashPassword(request.Senha),
            Perfil = "Pesquisador" // Padrão
        };

        await _repository.AddAsync(usuario);
        return Ok(new { message = "Usuário cadastrado com sucesso." });
    }

    [HttpPut("{id}/role")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleRequest request)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return NotFound();

        usuario.Perfil = request.Perfil;
        await _repository.UpdateAsync(usuario);

        return Ok(new { message = "Perfil atualizado com sucesso." });
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class RegisterRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class UpdateRoleRequest
{
    public string Perfil { get; set; } = string.Empty;
}
