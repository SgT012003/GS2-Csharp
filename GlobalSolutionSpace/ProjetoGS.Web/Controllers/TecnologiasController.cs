using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoGS.Web.Controllers;

public class TecnologiasController : Controller
{
    private readonly HttpClient _httpClient;

    public TecnologiasController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IActionResult> Index()
    {
        // Example of fetching from API
        // var response = await _httpClient.GetAsync("/api/tecnologias");
        // var tecnologias = await response.Content.ReadFromJsonAsync<IEnumerable<Tecnologia>>();
        return View();
    }

    [Authorize(Roles = "Administrador")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public IActionResult Create(object model)
    {
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Administrador")]
    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public IActionResult Edit(int id, object model)
    {
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Administrador")]
    public IActionResult Delete(int id)
    {
        return View();
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Administrador")]
    public IActionResult DeleteConfirmed(int id)
    {
        return RedirectToAction(nameof(Index));
    }
}
