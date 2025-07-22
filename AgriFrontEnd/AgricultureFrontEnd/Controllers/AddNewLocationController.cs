using AgricultureFrontEnd.Models.Vm.LocationVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgricultureFrontEnd.Controllers;


[Authorize(Roles = "Admin")]
public class AddNewLocationController : Controller
{
   private readonly HttpClient _client;

   public AddNewLocationController(HttpClient client)
   {
      _client = client;
      _client.BaseAddress = new Uri("https://localhost:7197/");
   }



   public async Task<IActionResult> Create()
   {
      var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
      
      ViewBag.TypesList = new SelectList(types, "LocationType", "LocationType");
      
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Create(LocationAddVm model)
   {
      if (!ModelState.IsValid)
      {
         var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
         ViewBag.TypesList = new SelectList(types, "LocationType", "LocationType");

      }

      var payload = new
      {
         name = model.Name,
         locationType = model.LocationType
      };
      await _client.PostAsJsonAsync("Location/Add", payload);
      return RedirectToAction(nameof(Success));
   }
   
   public IActionResult Success()
   {
      return View();
   }

}