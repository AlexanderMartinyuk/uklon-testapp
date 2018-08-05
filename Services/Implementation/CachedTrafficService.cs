using System;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class CachedTrafficService : SimpleTrafficService
    {
        protected readonly ITrafficCache TrafficCache;
        private int CachedValueActualInSeconds = 60;

        public CachedTrafficService(ITrafficCache trafficCache, ITrafficProvider trafficService, IRegionService regionService) 
            : base(trafficService, regionService)
        {
            TrafficCache = trafficCache;
        }

        public override async Task<TrafficModel> GetTrafficForRegionAsync(RegionModel region)
        {
            var traffic = TrafficCache.GetByRegionCode(region.Code);
            if (IsCachedValueActual(traffic))
            {
                return traffic;
            }

            traffic = await TrafficProvider.GetTrafficAsync(region.Code);
            if (traffic == null)
            {
                return null;
            }

            TrafficCache.Save(traffic);
            return TrafficCache.GetByRegionCode(traffic.RegionCode);
        }

        private bool IsCachedValueActual(TrafficModel traffic)
        {
            if (traffic == null)
            {
                return false;
            }

            var age = DateTime.Now - traffic.UpdatedAt;
            return age <= TimeSpan.FromSeconds(CachedValueActualInSeconds); ;
        }
    }
}
