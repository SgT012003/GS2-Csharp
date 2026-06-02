using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoGS.Web.Models;

namespace ProjetoGS.Web.Controllers;

[Authorize(Roles = "Administrador")]
public class TecnologiasController : Controller
{
    private readonly HttpClient _httpClient;

    public TecnologiasController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    private async Task PopulateOrigensViewBagAsync(int? selectedId = null)
    {
        var origens = await _httpClient.GetFromJsonAsync<IEnumerable<OrigemDTO>>("/api/origens");
        ViewBag.Origens = new SelectList(origens, "Id", "Nome", selectedId);
    }

    private async Task PopulateCategoriasViewBagAsync(int? selectedId = null)
    {
        var categorias = await _httpClient.GetFromJsonAsync<IEnumerable<CategoriaImpactoDTO>>("/api/categorias");
        ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", selectedId);
    }

    public async Task<IActionResult> Index()
    {
        var tecnologias = await _httpClient.GetFromJsonAsync<IEnumerable<TecnologiaDTO>>("/api/tecnologias");
        return View(tecnologias);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateOrigensViewBagAsync();
        await PopulateCategoriasViewBagAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TecnologiaDTO model)
    {
        if (ModelState.IsValid)
        {
            model.DataCadastro = DateTime.UtcNow;
            var response = await _httpClient.PostAsJsonAsync("/api/tecnologias", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Erro ao criar registro na API.");
        }

        await PopulateOrigensViewBagAsync(model.OrigemId);
        await PopulateCategoriasViewBagAsync(model.CategoriaImpactoId);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var tecnologia = await _httpClient.GetFromJsonAsync<TecnologiaDTO>($"/api/tecnologias/{id}");
        if (tecnologia == null) return NotFound();

        await PopulateOrigensViewBagAsync(tecnologia.OrigemId);
        await PopulateCategoriasViewBagAsync(tecnologia.CategoriaImpactoId);
        return View(tecnologia);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TecnologiaDTO model)
    {
        if (id != model.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/tecnologias/{id}", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Erro ao atualizar registro na API.");
        }

        await PopulateOrigensViewBagAsync(model.OrigemId);
        await PopulateCategoriasViewBagAsync(model.CategoriaImpactoId);
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var tecnologia = await _httpClient.GetFromJsonAsync<TecnologiaDTO>($"/api/tecnologias/{id}");
        if (tecnologia == null) return NotFound();

        return View(tecnologia);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/tecnologias/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return BadRequest();
    }
}
