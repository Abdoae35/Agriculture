using AgricultureFrontEnd.Models.Vm.AfforestationVM;
using AgricultureFrontEnd.Models.Vm.UsersVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace AgricultureFrontEnd.Controllers
{
    [Authorize(Roles = "Admin,Viewer")]
 
    public class SearchController : Controller
    {
        private readonly HttpClient _client;

        public SearchController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7197/");
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var model = new AfforestationSearchVM
            {
                Users = await LoadUsersAsync(),
                Locations = await LoadLocationsAsync(),
                Trees = await LoadTreesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetStatistics(AfforestationSearchVM model)
        {
            model.Users = await LoadUsersAsync();
            model.Locations = await LoadLocationsAsync();
            model.Trees = await LoadTreesAsync();

            if (model.FromDate == null || model.ToDate == null)
            {
                ModelState.AddModelError("", "Please select both dates.");
                return View(model);  
            }

            var response = await _client.PostAsJsonAsync("api/afforestation/search", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Server error, please try again later.");
                return View(model);
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

        private async Task<List<SelectListItem>> LoadUsersAsync()
        {
            var users = await _client.GetFromJsonAsync<List<UserReadVM>>("User/GetAll");

            
            var normalUsers = users
                .Where(u => u.Role == Roles.User)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }).ToList();

            normalUsers.Insert(0, new SelectListItem { Value = "", Text = "All Users" });

            return normalUsers;
        }

        private async Task<List<SelectListItem>> LoadLocationsAsync()
        {
            var locations = await _client.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");
            var items = locations.Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.Name }).ToList();
            items.Insert(0, new SelectListItem { Value = "", Text = "All Locations" });
            return items;
        }

        private async Task<List<SelectListItem>> LoadTreesAsync()
        {
            var trees = await _client.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
            var items = trees.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name }).ToList();
            items.Insert(0, new SelectListItem { Value = "", Text = "All Trees" });
            return items;
        }
    }
}
