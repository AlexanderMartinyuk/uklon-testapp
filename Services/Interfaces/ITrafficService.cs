using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface ITrafficService
    {
        IEnumerable<TrafficModel> GetAllTraffic();

        TrafficModel GetTrafficForRegion(RegionModel region);
    }
}
