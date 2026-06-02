using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoGS.Web.Models;

namespace ProjetoGS.Web.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IActionResult> Index()
    {
        DashboardViewModel? stats = null;
        try
        {
            var response = await _httpClient.GetAsync("/api/tecnologias/stats");
            if (response.IsSuccessStatusCode)
            {
                stats = await response.Content.ReadFromJsonAsync<DashboardViewModel>();
            }
        }
        catch (Exception)
        {
            // API not available yet or DB error
        }

        return View(stats ?? new DashboardViewModel());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
