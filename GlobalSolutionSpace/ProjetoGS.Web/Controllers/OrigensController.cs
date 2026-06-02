using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoGS.Web.Models;

namespace ProjetoGS.Web.Controllers;

[Authorize(Roles = "Administrador")]
public class OrigensController : Controller
{
    private readonly HttpClient _httpClient;

    public OrigensController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IActionResult> Index()
    {
        var origens = await _httpClient.GetFromJsonAsync<IEnumerable<OrigemDTO>>("/api/origens");
        return View(origens);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrigemDTO model)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/origens", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var origem = await _httpClient.GetFromJsonAsync<OrigemDTO>($"/api/origens/{id}");
        if (origem == null) return NotFound();
        return View(origem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OrigemDTO model)
    {
        if (id != model.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/origens/{id}", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var origem = await _httpClient.GetFromJsonAsync<OrigemDTO>($"/api/origens/{id}");
        if (origem == null) return NotFound();
        return View(origem);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/origens/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return BadRequest();
    }
}
