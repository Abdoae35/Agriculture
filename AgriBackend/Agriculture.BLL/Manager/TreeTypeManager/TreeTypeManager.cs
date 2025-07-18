
namespace Agriculiture.BLL.Manager.TreeTypeManager;

public class TreeTypeManager : ITreeTypeManager
{
    public readonly ITreeTypeRepo _repo;

    public TreeTypeManager(ITreeTypeRepo repo)
    {
        _repo = repo;
    }

    public async Task<List<TreeTypeReadDto>> GetAllTreeType()
    {
       var trees= await _repo.GetAll().Select(a=> new TreeTypeReadDto
       {
           Id = a.Id,
           Type = a.Type
           
       }).ToListAsync();
       
       return trees;
    }

    public Task<TreeTypeReadDto?> GetTreeById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTreeType(int id)
    {
        var tree = _repo.GetById(id);
        await _repo.DeleteAsync(tree);
    }

    public async Task AddNewType(TreeTypeAddDto treeTypeAddDto)
    {
        var newTreeType = new TreeType
        {
            Type = treeTypeAddDto.Type
        };
       await _repo.InsertAsync(newTreeType);
    }


    public void UpdateTree(TreeTypeReadDto treeDto)
    {
        throw new NotImplementedException();
    }
}