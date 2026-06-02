using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoGS.Web.Models;

namespace ProjetoGS.Web.Controllers;

[Authorize(Roles = "Administrador")]
public class UsuariosController : Controller
{
    private readonly HttpClient _httpClient;

    public UsuariosController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IActionResult> Index()
    {
        var usuarios = await _httpClient.GetFromJsonAsync<IEnumerable<UsuarioDTO>>("/api/usuarios");
        return View(usuarios);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRole(int id, string perfil)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/usuarios/{id}/role", new { Perfil = perfil });
        
        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Perfil do usuário atualizado com sucesso.";
        }
        else
        {
            TempData["ErrorMessage"] = "Erro ao atualizar o perfil do usuário.";
        }
        
        return RedirectToAction(nameof(Index));
    }
}
