using System.Threading.Tasks;

namespace XamSnap
{
    public interface ILocationService
    {
        Task<Location> GetCurrentLocation();
    }
}

