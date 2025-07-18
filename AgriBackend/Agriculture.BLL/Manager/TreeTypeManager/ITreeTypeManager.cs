using Agriculiture.BLL.Dtos.TreeTypeDto;

namespace Agriculiture.BLL.Manager.TreeTypeManager;

public interface ITreeTypeManager
{
    Task<List<TreeTypeReadDto>> GetAllTreeType();
    Task<TreeTypeReadDto?> GetTreeById(int id);
    Task DeleteTreeType(int id);
    Task AddNewType(TreeTypeAddDto treeTypeAddDto);
    void UpdateTree(TreeTypeReadDto treeDto);
}