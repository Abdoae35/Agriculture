using Agriculiture.BLL.Dtos.LocationDto;
using Agriculture.DAL.Models;

namespace Agriculiture.BLL.Manager.LocationManager;

public interface ILocationManager
{
    Task<List<LocationNameReadDto>> GetAllLocationsAsync();
    Task<LocationName?> GetLocationByIdAsync(int id);
    Task DeleteLocationAsync(LocationName location);
    Task AddNewLocationAsync(LocationNameAddDto location);
    Task UpdateLocationAsync(LocationNameUpdateDto location);
}