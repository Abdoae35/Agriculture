using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgricultureFrontEnd.Models.Vm.AfforestationVM;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")] // Entire controller restricted to Admins
public class SearchController : Controller
{
    private readonly HttpClient _client;

    public SearchController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://localhost:5200/");
    }

    [HttpGet]
    public IActionResult GetStatistics()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetStatistics(AfforestationSearchVM date)
    {
        if (date.FromDate == null || date.ToDate == null)
        {
            ModelState.AddModelError("", "Please select both dates.");
            return View(date);  
        }

        var response = await _client.PostAsJsonAsync("api/afforestation/search", date);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Server error, please try again later.");
            return View(date);
        }

        var result = await response.Content.ReadFromJsonAsync<List<AfforestationReadVM>>();

        var summaries = result.GroupBy(a => a.TreeName)
            .Select(t => new TreeSummaryReadVM
            {
                TreeName = t.Key,
                TotalPlanted = t.Sum(a => a.Number),
            }).ToList();

        var page = new AfforestationPageVM
        {
            TreeDetails = result,
            TreeSummaries = summaries
        };

        return View("SearchResults", page);
    }
}