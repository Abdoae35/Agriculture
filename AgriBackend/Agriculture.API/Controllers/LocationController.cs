namespace Agriculture.API.Controllers;

    [ApiController]
    [Route("[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationManager _manager;

        public LocationController(ILocationManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LocationNameAddDto dto)
        {
            await _manager.AddNewLocationAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _manager.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _manager.GetLocationByIdAsync(id);
            if (location == null)
                return NotFound();

            await _manager.DeleteLocationAsync(location);
            return Ok();
        }
    }
