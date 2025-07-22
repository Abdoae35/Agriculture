using AgricultureFrontEnd.Models.Vm.LocationVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")]
public class AddLocationTypeController : Controller
{
    private readonly HttpClient _client;

    public AddLocationTypeController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7197/");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");

        var model = new LocationTypePageVM
        {
            AddModel = new LocationTypeAddVM(),
            AllTypes = types ?? new List<LocationTypeReadVm>()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LocationTypePageVM model)
    {
        if (string.IsNullOrWhiteSpace(model.AddModel.LocationType))
        {
            ModelState.AddModelError("AddModel.LocationType", "Location Type Name cannot be empty.");
        }

        if (!ModelState.IsValid)
        {
            var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
            model.AllTypes = types ?? new List<LocationTypeReadVm>();
            return View(model);
        }

        await _client.PostAsJsonAsync("LocationType/AddNewType", model.AddModel);

        return RedirectToAction(nameof(Create));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _client.DeleteAsync($"LocationType/Delete/{id}");
        return RedirectToAction(nameof(Create));
    }
}