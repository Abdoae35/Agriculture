
namespace Agriculture.DAL.Repository.TreeNameRepo;

public interface ITreeNameRepo
{
    public IQueryable<TreeName> GetAll();
    
  
    public TreeName GetById(int id);
    public void Insert(TreeName tree);
    
    public void Update(TreeName tree);
    public Task Delete(TreeName tree);
    TreeType GetByTypeName(string typeName);
    

    
}