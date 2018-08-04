using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class TrafficController : Controller
    {
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
            var trafficService = new YandexTrafficService();

            return trafficService.GetAllTraffic();
        }

        [HttpGet]
        [Route("api/traffic/{regionCode}")]
        public IActionResult GetTrafficForRegion(long regionCode)
        {
            var regionService = new RegionsService();
            var trafficService = new YandexTrafficService();

            var region = regionService.GetRegionByCode(regionCode);
            if (region == null)
            {
                return NotFound();
            }

            var traffic = trafficService.GetTrafficForRegion(regionCode);
            if (traffic == null)
            {
                return NotFound();
            }

            return Ok(traffic);
        }
    }
}
