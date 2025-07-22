namespace Agriculiture.BLL.Manager.AfforestationAgricultureAchievementManager;

public class AfforestationAgricultureAchievementManager: IAfforestationAgricultureAchievementManager
{
    private readonly IAfforestationAgricultureAchievementRepo _repo;

    public AfforestationAgricultureAchievementManager(IAfforestationAgricultureAchievementRepo repo)
    {
        _repo = repo;
    }

    public List<AfforestationAgricultureAchievementReadDto> Search(AfforestationSearchRequest request)
    {
        IQueryable<AfforestationAgricultureAchievement> query = _repo.GetAll()
            .Include(x => x.TreeType)
            .Include(x => x.TreeName)
            .Include(x => x.LocationName)
            .Include(x => x.LocationType)
            .Include(x => x.User);

        if (request.FromDate.HasValue)
            query = query.Where(x => x.DateOfPlanted >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(x => x.DateOfPlanted <= request.ToDate.Value);

        if (request.SelectedUserId.HasValue)
            query = query.Where(x => x.UserId == request.SelectedUserId);

        if (request.SelectedLocationId.HasValue)
            query = query.Where(x => x.LocationNameId == request.SelectedLocationId);

        if (request.SelectedTreeId.HasValue)
            query = query.Where(x => x.TreeNameId == request.SelectedTreeId);

        return query.Select(x => new AfforestationAgricultureAchievementReadDto()
        {
            Id = x.Id,
            DateOfPlanted = x.DateOfPlanted,
            TreeName = x.TreeName.Name,
            LocationName = x.LocationName.Name,
            Number = x.Number,
            TreeNameId = x.TreeNameId,
            UserName = x.User.Name
        }).ToList();
    }

    

    public IQueryable<AfforestationAgricultureAchievementReadDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public AfforestationAgricultureAchievementReadDto GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void update(AfforestationUpdateDto afforestationUpdateDto)
    {
        throw new NotImplementedException();
    }

    public async Task insert(AfforestationAddDto afforestationAddDto)
    {
        var newAgri = new AfforestationAgricultureAchievement()
        {
            DateOfPlanted = afforestationAddDto.DateOfPlanted,
            TreeTypeId = afforestationAddDto.TreeTypeId,
            LocationTypeId = afforestationAddDto.LocationTypeId,
            Number = afforestationAddDto.Number,
            TreeNameId = afforestationAddDto.TreeNameId,
            LocationNameId = afforestationAddDto.LocationNameId,
            UserId = afforestationAddDto.UserId
        };
        await _repo.Insert(newAgri);
    }

    public void delete(int id)
    {
        throw new NotImplementedException();
    }
}