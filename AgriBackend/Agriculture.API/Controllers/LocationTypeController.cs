

namespace Agriculture.API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class LocationTypeController : ControllerBase
{
   private readonly ILocationTypeManager _manager;

   public LocationTypeController(ILocationTypeManager manager)
   {
      _manager = manager;
   }

   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var result = await _manager.GetAllLocationType();
      return Ok(result);
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int id)
   {
      var result = await _manager.GetLocationTypeById(id);
      return Ok(result);
   }

   [HttpPost]
   public async Task<IActionResult> AddNewType(LocationTypeAddDto locationTypeAddDto)
   {
      await _manager.AddNewLocationType(locationTypeAddDto);
      return Ok($"LocationType {locationTypeAddDto.LocationType}");
      
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
      await _manager.DeleteLocationTypeById(id);
      return Ok($"LocationType {id} deleted");
   }
   
   
   
   
   
}