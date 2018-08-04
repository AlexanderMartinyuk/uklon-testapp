using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class StubTrafficService
    {
        private readonly Dictionary<long, string> DATA = new Dictionary<long, string>
                                                             {
                                                                 [0] = "Немає заторів",
                                                                 [1] = "Невеликі затори",
                                                                 [2] = "Середні затори",
                                                                 [3] = "Серйозні затори"
                                                             };

        public IEnumerable<TrafficModel> GetAllTraffic()
        {
            var regionService = new RegionsService();
            var result = new List<TrafficModel>();

            foreach (var region in regionService.GetAllRegions())
            {
                result.Add(GetTrafficForRegion(region.Code));
            }

            return result;
        }

        public TrafficModel GetTrafficForRegion(long regionCode)
        {
            var result = DATA.ElementAt(new Random().Next(0, DATA.Count));

            return new TrafficModel(result.Key, result.Value);
        }
    }
}
