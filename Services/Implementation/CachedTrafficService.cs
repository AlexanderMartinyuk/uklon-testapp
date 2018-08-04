using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class CachedTrafficService : ICachedTrafficService
    {
        private readonly ITrafficService _trafficService;

        public CachedTrafficService(ITrafficService trafficService)
        {
            _trafficService = trafficService;
        }

        public IEnumerable<TrafficModel> GetAllTraffic()
        {
            return _trafficService.GetAllTraffic();
        }

        public TrafficModel GetTrafficForRegion(RegionModel region)
        {
            return _trafficService.GetTrafficForRegion(region);
        }
    }
}
