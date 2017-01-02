using System.Threading.Tasks;
using Xamarin.Geolocation;
using System;

namespace XamSnap.iOS
{
    public class LocationService : ILocationService
    {
        private const int Timeout = 3000;
        private Geolocator _geolocator;

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                //NOTE: wait until here to create Gelocator, so that the iOS prompt appears on GPS request
                if (_geolocator == null)
                    _geolocator = new Geolocator();

                var location = await _geolocator.GetPositionAsync(Timeout);

                Console.WriteLine("GPS location: {0},{1}", location.Latitude, location.Longitude);

                return new Location
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                };
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error finding GPS location: " + exc);

                //If anything goes wrong, just return null as if it was not found
                return null;
            }
        }
    }
}

