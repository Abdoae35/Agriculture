using Microsoft.AspNetCore.Authorization;

namespace AgricultureFrontEnd.Controllers;
[Authorize(Roles = "Admin")]
public class RegisterController : Controller
{
    private readonly HttpClient _client;

    public RegisterController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://localhost:5200/");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        await _client.PostAsJsonAsync("User/Register/register", model);
        return RedirectToAction(nameof(Success));
    }
    
    public IActionResult Success()
    {
        return View();
    }
}