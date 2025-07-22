using AgricultureFrontEnd.Models.Vm.AfforestationVM;
using AgricultureFrontEnd.Models.Vm.TreeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "User")]
public class AddPlantController : Controller
{
    private readonly HttpClient _httpClient;

    public AddPlantController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7197/");
    }

    public async Task<IActionResult> Create()
    {
        var treeNames = await _httpClient.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
        var locationNames = await _httpClient.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");

        var formVM = new AfforestationFormVM
        {
            TreeNames = treeNames,
            LocationNames = locationNames
        };

        return View(formVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AfforestationFormVM form)
    {
        
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            return Unauthorized("User not logged in.");
        }

        var treeNames = await _httpClient.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
        var locationNames = await _httpClient.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");
        var treeTypes = await _httpClient.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
        var locationTypes = await _httpClient.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");

        var selectedTree = treeNames.FirstOrDefault(t => t.Id == form.TreeNameId);
        var selectedLocation = locationNames.FirstOrDefault(l => l.Id == form.LocationNameId);

        if (selectedTree == null || selectedLocation == null)
        {
            ModelState.AddModelError("", "Invalid selection of tree or location.");
            form.TreeNames = treeNames;
            form.LocationNames = locationNames;
            return View(form);
        }

        var selectedTreeType = treeTypes.FirstOrDefault(t =>
            t.Type.Trim().ToLower() == selectedTree.Type?.Trim().ToLower());

        var selectedLocationType = locationTypes.FirstOrDefault(l =>
            l.LocationType.Trim().ToLower() == selectedLocation.LocationType?.Trim().ToLower());

        var sendData = new AfforestationAddVM
        {
            TreeNameId = form.TreeNameId,
            TreeTypeId = selectedTreeType?.Id ?? 0,
            LocationNameId = form.LocationNameId,
            LocationTypeId = selectedLocationType?.Id ?? 0,
            Number = form.Number,
            DateOfPlanted = DateOnly.FromDateTime(form.DateOfPlanted),
            UserId = userId 
        };

        await _httpClient.PostAsJsonAsync("api/afforestation/Add", sendData);

        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }
}
