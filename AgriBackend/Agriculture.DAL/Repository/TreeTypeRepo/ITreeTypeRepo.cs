namespace Agriculture.DAL.Repository.TreeTypeRepo;

public interface ITreeTypeRepo
{
    public IQueryable<TreeType> GetAll();
    public TreeType GetById(int id);
    public Task InsertAsync(TreeType tree);
    public void Update(TreeType tree);
    public Task DeleteAsync(TreeType tree);
}