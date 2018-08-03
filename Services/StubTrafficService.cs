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
                                                                 [3] = "Серйозні затори",
                                                                 [4] = "Місцями ускладнення"
                                                             };

        public IEnumerable<TrafficModel> GetAllTraffic()
        {
            var result = new List<TrafficModel>();

            return result;
        }

        public TrafficModel GetTrafficForRegion(long regionCode)
        {
            var result = DATA.ElementAt(new Random().Next(0, DATA.Count));

            return new TrafficModel(result.Key, result.Value);
        }
    }
}
