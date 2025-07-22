using AgricultureFrontEnd.Models.Vm.TreeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")]
public class AddTreeTypeController : Controller
{
    private readonly HttpClient _client;

    public AddTreeTypeController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7197/");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");

        var model = new TreeTypePageVM
        {
            AddModel = new TreeTypeAddVM(),
            AllTypes = types ?? new List<TreeTypeReadVM>()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TreeTypePageVM model)
    {
        if (string.IsNullOrWhiteSpace(model.AddModel.Type))
        {
            ModelState.AddModelError("AddModel.Type", "Tree Type Name cannot be empty.");
        }

        if (!ModelState.IsValid)
        {
            var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
            model.AllTypes = types ?? new List<TreeTypeReadVM>();
            return View(model);
        }

        await _client.PostAsJsonAsync("TreeType/AddNewType", model.AddModel);

        return RedirectToAction(nameof(Create));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        await _client.DeleteAsync($"TreeType/DeleteTypes/{id}");
        return RedirectToAction(nameof(Create));
    }
}