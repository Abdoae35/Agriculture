

namespace Agriculiture.BLL.Manager.LocationTypeManager;

public interface ILocationTypeManager
{
    Task<List<LocationTypeReadDto>> GetAllLocationType();
    Task<LocationTypeReadDto> GetLocationTypeById(int id);
    Task DeleteLocationTypeById(int id);
    Task AddNewLocationType(LocationTypeAddDto treeTypeAddDto);
    void UpdateLocation(LocationTypeReadDto treeDto);
    
}