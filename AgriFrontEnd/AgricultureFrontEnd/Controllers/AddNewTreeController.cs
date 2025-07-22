using AgricultureFrontEnd.Models.Vm.TreeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgricultureFrontEnd.Controllers;


[Authorize(Roles = "Admin")]
public class AddNewTreeController : Controller
{
    private readonly HttpClient  _client;

    public AddNewTreeController(HttpClient  client)
    {
        _client = client; 
        _client.BaseAddress = new Uri("https://localhost:7197/");
    }

    public async Task<IActionResult> Create()
    {
        var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");

        
        ViewBag.TypeList = new SelectList(types, "Type", "Type");

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(TreeAddVm model)
    {
        if (!ModelState.IsValid)
        {
            var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
            ViewBag.TypeList = new SelectList(types, "Type", "Type");
            return View(model);
        }

        var payload = new
        {
            treeName = model.Name,
            treeTypeName = model.TreeType  
        };

        await _client.PostAsJsonAsync("/Tree/Add", payload);  

        return RedirectToAction("Success");
    }
    
    public IActionResult Success()
    {
        return View();
    }


    
   
}