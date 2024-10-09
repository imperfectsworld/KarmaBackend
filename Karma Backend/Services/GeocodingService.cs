using Karma_Backend.Models;
using Newtonsoft.Json;
namespace Karma_Backend.Services  

{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;
       // private readonly string _googleApiKey= Secret.ApiKey;
        public GeocodingService(HttpClient httpClient, string googleApiKey)
        {
            _httpClient = httpClient;
          //  _googleApiKey = googleApiKey;
        }
        public async Task<(double Latitude, double Longitude)> GetGeocodeAsync(string address)
        {
            address = address.Replace(' ', '+');

            var requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={Secret.ApiKey}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var geocodeResult = JsonConvert.DeserializeObject<GeocodeResponse>(responseContent);

            if (geocodeResult.Status == "OK")
            {
                var location = geocodeResult.Results.FirstOrDefault()?.Geometry.Location;
                return (location.Lat, location.Lng);
            }

            throw new Exception("Unable to geocode address.");
        }
    }
}
public class GeocodeResponse
{
    public string Status { get; set; }
    public GeocodeResult[] Results { get; set; }
}

public class GeocodeResult
{
    public GeocodeGeometry Geometry { get; set; }
}

public class GeocodeGeometry
{
    public GeocodeLocation Location { get; set; }
}

public class GeocodeLocation
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}
public class GoogleApiSettings
{
    public string ApiKey { get; set; }
}
