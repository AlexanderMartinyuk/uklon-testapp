using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class StubTrafficService : ITrafficService
    {
        private readonly IRegionService _regionService;
        private readonly Dictionary<long, string> _data = new Dictionary<long, string>
                                                             {
                                                                 [0] = "Немає заторів",
                                                                 [1] = "Невеликі затори",
                                                                 [2] = "Середні затори",
                                                                 [3] = "Серйозні затори"
                                                             };

        public StubTrafficService(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public IEnumerable<TrafficModel> GetAllTraffic()
        {
            var result = new List<TrafficModel>();

            foreach (var region in _regionService.GetAllRegions())
            {
                result.Add(GetTrafficForRegion(region));
            }

            return result;
        }

        public TrafficModel GetTrafficForRegion(RegionModel region)
        {
            var result = _data.ElementAt(new Random().Next(0, _data.Count));

            return new TrafficModel(result.Key, result.Value);
        }
    }
}
