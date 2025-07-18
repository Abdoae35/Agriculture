namespace Agriculture.DAL.Repository.AfforestationAgricultureAchievementRepo;

public interface IAfforestationAgricultureAchievementRepo
{
    public IQueryable<AfforestationAgricultureAchievement> GetAll();
    public AfforestationAgricultureAchievement GetById(int id);
    public Task Insert(AfforestationAgricultureAchievement entity);
    public void Update(AfforestationAgricultureAchievement entity);
    public void Delete(AfforestationAgricultureAchievement entity);
}