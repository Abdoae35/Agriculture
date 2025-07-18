namespace Agriculture.API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class TreeController : ControllerBase
{
    private readonly ITreeNameManager _treeNameManager;

    public TreeController(ITreeNameManager treeNameManager)
    {
        _treeNameManager = treeNameManager;
    }

    [HttpPost]
    public IActionResult Add([FromBody] TreeNameAddDto nameAddTreeName)
    {
        _treeNameManager.AddNewTree(nameAddTreeName);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trees = await _treeNameManager.GetAllTrees();
        return Ok(trees);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tree = await _treeNameManager.GetTreeById(id);

        if (tree == null)
        {
            return NotFound($"Tree with ID {tree.Name} not found.");
        }

        await _treeNameManager.DeleteTree(tree);
        return Ok($"Tree with ID {tree.Name} deleted successfully.");
    }
}