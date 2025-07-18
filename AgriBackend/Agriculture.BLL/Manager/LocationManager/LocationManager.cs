using Agriculiture.BLL.Dtos.LocationDto;
using Agriculture.DAL.Models;
using Agriculture.DAL.Repository.LocationNameRepo;
using Agriculture.DAL.Repository.TypeOfLocationRepo;
using Microsoft.EntityFrameworkCore;

namespace Agriculiture.BLL.Manager.LocationManager
{
    public class LocationManager : ILocationManager
    {
        private readonly ILocationNameRepo _locationNameRepo;
        private readonly ITypeOfLocationRepo _typeOfLocationRepo;

        public LocationManager(ILocationNameRepo locationNameRepo, ITypeOfLocationRepo typeOfLocationRepo)
        {
            _locationNameRepo = locationNameRepo;
            _typeOfLocationRepo = typeOfLocationRepo;
        }

        public async Task<List<LocationNameReadDto>> GetAllLocationsAsync()
        {
            var locations = await _locationNameRepo
                .GetAll()
                .Select(a => new LocationNameReadDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    LocationType = a.LocationType.LocationType
                })
                .ToListAsync();

            return locations;
        }

        public async Task<LocationName?> GetLocationByIdAsync(int id)
        {
            return await _locationNameRepo.GetByIdAsync(id);
        }

        public async Task DeleteLocationAsync(LocationName location)
        {
            var locationType = await _typeOfLocationRepo.GetByIdAsync(location.LocationTypeId);

            await _locationNameRepo.DeleteLocationAsync(location);

            var isTypeStillUsed = _locationNameRepo.GetAll().Any(t => t.LocationTypeId == location.LocationTypeId);

            if (!isTypeStillUsed && locationType != null)
            {
                await _typeOfLocationRepo.DeleteAsync(locationType);
            }
        }

        public async Task AddNewLocationAsync(LocationNameAddDto location)
        {
            if (string.IsNullOrWhiteSpace(location.LocationType))
                throw new ArgumentException("Location Type is required.");

            var existingType = _locationNameRepo.GetByTypeName(location.LocationType);

            if (existingType == null)
            {
                existingType = new TypeOfLocation
                {
                    LocationType = location.LocationType
                };

                await _typeOfLocationRepo.InsertAsync(existingType);
            }

            var newLocation = new LocationName
            {
                Name = location.Name,
                LocationTypeId = existingType.Id
            };

            await _locationNameRepo.InsertLocationAsync(newLocation);
        }

        public async Task UpdateLocationAsync(LocationNameUpdateDto location)
        {
            // Optional: Add validation for location.Id if needed

            var existing = await _locationNameRepo.GetByIdAsync(location.Id);
            if (existing == null)
                throw new Exception($"Location with ID {location.Id} not found.");

            existing.Name = location.Name;
            // Optionally update LocationTypeId if allowed

            await _locationNameRepo.UpdateAsync(existing);
        }
    }
}
