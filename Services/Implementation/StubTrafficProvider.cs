using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class StubTrafficProvider : ITrafficProvider
    {
        private readonly Dictionary<long, string> _data = new Dictionary<long, string>
                                                             {
                                                                 [0] = "Немає заторів",
                                                                 [1] = "Невеликі затори",
                                                                 [2] = "Середні затори",
                                                                 [3] = "Серйозні затори"
                                                             };
        protected readonly IRegionService RegionService;

        public StubTrafficProvider(IRegionService regionService)
        {
            RegionService = regionService;
        }

        public virtual IEnumerable<TrafficModel> GetAllTraffic()
        {
            var result = new List<TrafficModel>();

            foreach (var region in RegionService.GetAllRegions())
            {
                result.Add(GetTraffic(region.Code));
            }

            return result;
        }

        public virtual TrafficModel GetTraffic(long regionCode)
        {
            var result = _data.ElementAt(new Random().Next(0, _data.Count));

            return new TrafficModel(result.Key, result.Value, regionCode);
        }
    }
}
