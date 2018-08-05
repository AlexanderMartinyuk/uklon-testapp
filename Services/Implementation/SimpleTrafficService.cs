using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class SimpleTrafficService : ITrafficService
    {
        protected readonly IRegionService RegionService;
        protected readonly ITrafficProvider TrafficProvider;

        public SimpleTrafficService(ITrafficProvider trafficProvider, IRegionService regionService)
        {
            TrafficProvider = trafficProvider;
            RegionService = regionService;
        }

        public virtual async Task<IEnumerable<TrafficModel>> GetAllTrafficAsync()
        {
            var regions = RegionService.GetAllRegions();
            return await Task.WhenAll(regions.Select(async region =>
                await GetTrafficForRegionAsync(region)));
        }

        public virtual async Task<TrafficModel> GetTrafficForRegionAsync(RegionModel region)
        {
            return await TrafficProvider.GetTrafficAsync(region.Code);
        }
    }
}