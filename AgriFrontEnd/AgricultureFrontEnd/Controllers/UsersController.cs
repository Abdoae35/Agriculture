using Microsoft.AspNetCore.Mvc;
using AgricultureFrontEnd.Models.Vm.UsersVM;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UsersController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ShowUsers()
    {
        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<List<UserReadVM>>(
            "https://localhost:7197/User/GetAll");

        return View(response);
    }

    public async Task<IActionResult> DeleteUser(int id)
    {
        using var client = _httpClientFactory.CreateClient();

        if (id <= 0)
        {
            TempData["Error"] = "Invalid user ID.";
            return RedirectToAction(nameof(ShowUsers));
        }

        var url = $"https://localhost:7197/User/DeleteUser/{id}";
        Console.WriteLine($"Attempting DELETE request to URL: {url}");

        var response = await client.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            TempData["Error"] = $"Failed to delete user with ID {id}.";
        }
        else
        {
            TempData["Success"] = $"User with ID {id} deleted successfully.";
        }

        return RedirectToAction(nameof(ShowUsers));
    }
}
