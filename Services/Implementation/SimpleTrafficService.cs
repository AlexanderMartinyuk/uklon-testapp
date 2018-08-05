using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class SimpleTrafficService : ITrafficService
    {
        protected readonly ITrafficProvider TrafficProvider;
        protected readonly IRegionService RegionService;

        public SimpleTrafficService(ITrafficProvider trafficProvider, IRegionService regionService)
        {
            TrafficProvider = trafficProvider;
            RegionService = regionService;
        }

        public virtual IEnumerable<TrafficModel> GetAllTraffic()
        {
            return RegionService.GetAllRegions().Select(GetTrafficForRegion).ToList();
        }

        public virtual TrafficModel GetTrafficForRegion(RegionModel region)
        {
            return TrafficProvider.GetTraffic(region.Code);
        }
    }
}
