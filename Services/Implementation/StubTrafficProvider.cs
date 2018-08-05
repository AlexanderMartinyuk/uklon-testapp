using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public virtual async Task<TrafficModel> GetTrafficAsync(long regionCode)
        {
            return await Task.Run(() =>
            {
                var result = _data.ElementAt(new Random().Next(0, _data.Count));
                return ModelsFactory.NewTrafficModel(regionCode, result.Key, result.Value);
            });
        }
    }
}