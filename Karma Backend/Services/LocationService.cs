using Karma_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Karma_Backend.Services
{
    public class LocationService
    {
        private readonly KarmaDbContext _dbContext;
        private readonly GeocodingService _geocodingService;

        public LocationService(KarmaDbContext dbContext, GeocodingService geocodingService)
        {
            _dbContext = dbContext;
            _geocodingService = geocodingService;
        }

        public async Task<Location> SaveLocationAsync(string address)
        {
            // Get the geocode (latitude and longitude) from the API
            var (latitude, longitude) = await _geocodingService.GetGeocodeAsync(address);

            // Create the new location object
            var location = new Location
            {
                Address = address,
                Latitude = latitude,
                Longitude = longitude
            };

            // Save the location to the database
            _dbContext.Locations.Add(location);
            await _dbContext.SaveChangesAsync();
            return location;
        }
        public async Task<List<Location>> GetAll()
        {
            // Retrieve all locations from the database
            return await _dbContext.Locations.ToListAsync();
        }
    }
}
