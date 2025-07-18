namespace Agriculture.DAL.Repository.AfforestationAgricultureAchievementRepo;

public class AfforestationAgricultureAchievementRepo : IAfforestationAgricultureAchievementRepo
{
    public readonly AgriContext _context;

    public AfforestationAgricultureAchievementRepo(AgriContext context)
    {
        _context = context;
    }


    public IQueryable<AfforestationAgricultureAchievement> GetAll()
    {
       return _context.AfforestationAgricultureAchievements.AsNoTracking();
    }

    public AfforestationAgricultureAchievement? GetById(int id)
    {
       return _context.AfforestationAgricultureAchievements.Find(id);
    }

    public async Task Insert(AfforestationAgricultureAchievement entity)
    {
         await _context.AfforestationAgricultureAchievements.AddAsync(entity);
         await _context.SaveChangesAsync(); 
    }

    public void Update(AfforestationAgricultureAchievement entity)
    {
        _context.AfforestationAgricultureAchievements.Update(entity);
        _context.SaveChanges(); 
    }

    public void Delete(AfforestationAgricultureAchievement entity)
    {
        _context.AfforestationAgricultureAchievements.Remove(entity);
        _context.SaveChanges();
    }
}