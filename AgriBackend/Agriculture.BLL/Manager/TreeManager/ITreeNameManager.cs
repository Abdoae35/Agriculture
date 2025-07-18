using Agriculiture.BLL.Dtos.TreeDto;
using Agriculture.DAL.Models;

namespace Agriculiture.BLL.Manager.TreeManager;

public interface ITreeNameManager
{
    Task<List<TreeReadDto>> GetAllTrees();
    Task<TreeName?> GetTreeById(int id);
    Task DeleteTree(TreeName tree);
    void AddNewTree(TreeNameAddDto treeDto);
    void UpdateTree(TreeUpdateDto tree);
}