using Agriculiture.BLL.Dtos.TreeDto;
using Agriculture.DAL.Models;
using Agriculture.DAL.Repository.TreeNameRepo;
using Agriculture.DAL.Repository.TreeTypeRepo;
using Microsoft.EntityFrameworkCore;

namespace Agriculiture.BLL.Manager.TreeManager;

public class TreeNameManager : ITreeNameManager
{
    private readonly ITreeNameRepo _treeNameRepo;
    private readonly ITreeTypeRepo _treeTypeRepo;

    public TreeNameManager(ITreeNameRepo treeNameRepo, ITreeTypeRepo treeTypeRepo)
    {
        _treeNameRepo = treeNameRepo;
        _treeTypeRepo = treeTypeRepo;
    }

    public async Task<List<TreeReadDto>> GetAllTrees()
    {
        var allTrees = _treeNameRepo.GetAll()
            .Include(a => a.Type)
            .Select(a => new TreeReadDto()
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type.Type,
            }).ToList();

        return await Task.FromResult(allTrees); 
    }

    public async Task<TreeName?> GetTreeById(int id)
    {
        return await Task.FromResult(_treeNameRepo.GetById(id)); 
    }

    public async Task DeleteTree(TreeName tree)
    {
        var treeType = _treeTypeRepo.GetById(tree.TypeId);

        await _treeNameRepo.Delete(tree);

        var isTypeStillUsed = _treeNameRepo.GetAll().Any(t => t.TypeId == tree.TypeId);

        if (!isTypeStillUsed && treeType != null)
        {
            await _treeTypeRepo.DeleteAsync(treeType);
        }
    }

    public void AddNewTree(TreeNameAddDto treeDto)
    {
        if (string.IsNullOrWhiteSpace(treeDto.TreeTypeName))
            throw new ArgumentException("TreeTypeName is required.");

        var existingType = _treeNameRepo.GetByTypeName(treeDto.TreeTypeName);

        if (existingType == null)
        {
            existingType = new TreeType
            {
                Type = treeDto.TreeTypeName
            };
            _treeTypeRepo.InsertAsync(existingType);
        }

        var newTree = new TreeName
        {
            Name = treeDto.TreeName,
            TypeId = existingType.Id
        };

        _treeNameRepo.Insert(newTree);
    }

    public void UpdateTree(TreeUpdateDto tree)
    {
        throw new NotImplementedException();
    }
}
