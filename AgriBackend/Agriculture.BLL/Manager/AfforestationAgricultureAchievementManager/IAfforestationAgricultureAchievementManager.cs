namespace Agriculiture.BLL.Manager.AfforestationAgricultureAchievementManager;

public interface IAfforestationAgricultureAchievementManager
{
    public List<AfforestationAgricultureAchievementReadDto> Search(AfforestationSearchRequest request);
    public IQueryable<AfforestationAgricultureAchievementReadDto> GetAll();
    public AfforestationAgricultureAchievementReadDto GetById(int id);
    public void update(AfforestationUpdateDto afforestationUpdateDto);
    public Task insert(AfforestationAddDto afforestationAddDto);
    public void delete(int id);
    
}