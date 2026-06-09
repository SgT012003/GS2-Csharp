using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoGS.Web.Controllers;

public class AuthController : Controller
{
    private readonly HttpClient _httpClient;

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/usuarios/login", new { Email = email, Senha = password });
        
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<ProjetoGS.Web.Models.UsuarioDTO>();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Perfil)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Tecnologias");
            }
        }

        ViewBag.ErrorMessage = "Credenciais inválidas. Tente novamente.";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AutoLogin()
    {
        var response = await _httpClient.PostAsJsonAsync("/api/usuarios/login", new { Email = "admin@novaeconomia.space", Senha = "Admin@123" });
        
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<ProjetoGS.Web.Models.UsuarioDTO>();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Perfil)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Dashboard", "Home");
            }
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(ProjetoGS.Web.Models.RegisterRequestDTO model)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/usuarios/register", model);
        
        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Conta criada com sucesso! Faça login.";
            return RedirectToAction("Login");
        }

        ViewBag.ErrorMessage = "Erro ao criar conta. O e-mail pode já estar em uso.";
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
