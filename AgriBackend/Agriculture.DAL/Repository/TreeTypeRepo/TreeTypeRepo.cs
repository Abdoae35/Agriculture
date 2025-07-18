namespace Agriculture.DAL.Repository.TreeTypeRepo;

public class TreeTypeRepo :ITreeTypeRepo
{
    private readonly AgriContext _context;

    public TreeTypeRepo(AgriContext context)
    {
        _context = context;
    }

    public  IQueryable<TreeType> GetAll()
    {
        return _context.TreeTypes.AsNoTracking();
    }

    public TreeType? GetById(int id)
    {
        return _context.TreeTypes.Find(id);
    }

    public async Task InsertAsync(TreeType tree)
    {
        _context.TreeTypes.Add(tree);
       await _context.SaveChangesAsync();
        
    }

    public void Update(TreeType tree)
    {
       _context.SaveChanges();
    }

    public async Task DeleteAsync(TreeType tree)
    {
       _context.TreeTypes.Remove(tree);
       await _context.SaveChangesAsync();
    }
}