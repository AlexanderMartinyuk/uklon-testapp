using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface ITrafficService
    {
        Task<IEnumerable<TrafficModel>> GetAllTrafficAsync();

        Task<TrafficModel> GetTrafficForRegionAsync(RegionModel region);
    }
}