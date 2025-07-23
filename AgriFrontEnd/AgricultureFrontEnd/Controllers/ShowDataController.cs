using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")]
public class ShowDataController : Controller
{
    private readonly HttpClient _httpClient;

    public ShowDataController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7197/");
    }

    public async Task<IActionResult> AllTrees()
    {
        var response = await _httpClient.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
        return View(response);
    }

    public async Task<IActionResult> AllLocations()
    {
        var response = await _httpClient.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");
        return View(response);
    }

    
    [HttpGet]
    public async Task<IActionResult> DeleteTree(int id)
    {
        var response = await _httpClient.DeleteAsync($"Tree/Delete/{id}");

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Tree deleted successfully.";
        }
        else
        {
            TempData["Error"] = "Failed to delete tree.";
        }

        return RedirectToAction("AllTrees");
    }

   
    [HttpGet]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        var response = await _httpClient.DeleteAsync($"Location/Delete/{id}");

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Location deleted successfully.";
        }
        else
        {
            TempData["Error"] = "Failed to delete location.";
        }

        return RedirectToAction("AllLocations");
    }
}
