using Agriculiture.BLL.Dtos.TreeTypeDto;
using Agriculiture.BLL.Manager.TreeTypeManager;

namespace Agriculture.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreeTypeController : ControllerBase
{
    private readonly ITreeTypeManager _manager;

    public TreeTypeController(ITreeTypeManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTreeType()
    {
        var trees= await _manager.GetAllTreeType();
        return Ok(trees);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewType([FromBody] TreeTypeAddDto treeDto)
    {
        await _manager.AddNewType(treeDto);
        return Ok($"Type {treeDto.Type} Added");
    }

    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteTypes(int id)
    {
        await _manager.DeleteTreeType(id);
        return Ok($"Type {id} Deleted");
    }
    
    
    
    
}