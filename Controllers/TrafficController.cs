using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    public class TrafficController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly ICachedTrafficService _trafficService;

        public TrafficController(IRegionService regionsService, ICachedTrafficService trafficService)
        {
            _regionService = regionsService;
            _trafficService = trafficService;
        }

        [HttpGet]
        [Route("api/regions/all")]
        public IEnumerable<RegionModel> GetAllRegions()
        {
            return _regionService.GetAllRegions();
        }

        [HttpGet]
        [Route("api/traffic/all")]
        public IEnumerable<TrafficModel> GetTrafficForAllRegions()
        {
            return _trafficService.GetAllTraffic();
        }

        [HttpGet]
        [Route("api/traffic/{regionCode}")]
        public IActionResult GetTrafficForRegion(long regionCode)
        {
            var region = _regionService.GetRegionByCode(regionCode);
            if (region == null)
            {
                return NotFound();
            }

            var traffic = _trafficService.GetTrafficForRegion(region);
            if (traffic == null)
            {
                return NotFound();
            }

            return Ok(traffic);
        }
    }
}
