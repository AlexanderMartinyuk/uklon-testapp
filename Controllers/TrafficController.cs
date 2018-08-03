using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class TrafficController : Controller
    {
        // GET api/regions
        [HttpGet]
        [Route("api/regions/all")]
        public IEnumerable<RegionModel> GetAllRegions()
        {
            var regionService = new RegionsService();

            return regionService.GetAllRegions();
        }

        [HttpGet]
        [Route("api/traffic/all")]
        public IEnumerable<TrafficModel> GetTrafficForAllRegions()
        {
            var trafficService = new StubTrafficService();

            return trafficService.GetAllTraffic();
        }

        [HttpGet]
        [Route("api/traffic/{regionCode}")]
        public TrafficModel GetTrafficForRegion(long regionCode)
        {
            var regionService = new StubTrafficService();

            return regionService.GetTrafficForRegion(regionCode);
        }
    }
}
