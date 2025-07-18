namespace Agriculture.DAL.Repository.TreeNameRepo;
public class TreeNameRepo : ITreeNameRepo
{
    private readonly AgriContext _context;

    public TreeNameRepo(AgriContext context)
    {
        _context = context;
    }

    public IQueryable<TreeName> GetAll()
    {
        return _context.TreeNames.AsNoTracking()
            .Include(a => a.Type);
    }

    public TreeName? GetById(int id)
    {
        return _context.TreeNames.Find(id);
    }

    public void Insert(TreeName tree)
    {
        _context.TreeNames.Add(tree);
        _context.SaveChanges();
    }

    public void Update(TreeName tree)
    {
        _context.TreeNames.Update(tree);
        _context.SaveChanges();
    }

    public async Task Delete(TreeName tree)
    {
        _context.TreeNames.Remove(tree);
        await _context.SaveChangesAsync();
    }

    public TreeType? GetByTypeName(string typeName)
    {
        return _context.TreeTypes.FirstOrDefault(t => t.Type == typeName);
    }
}
