using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    public class TrafficController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly ITrafficService _trafficService;

        public TrafficController(IRegionService regionsService, ITrafficService trafficService)
        {
            _regionService = regionsService;
            _trafficService = trafficService;
        }

        [HttpGet]
        [Route("api/regions/all")]
        public IActionResult GetAllRegions()
        {
            return Ok(
                _regionService.GetAllRegions()
                    .Select(DtoBuilder.GetRegionDto));
        }

        [HttpGet]
        [Route("api/traffic/all")]
        public IActionResult GetTrafficForAllRegions()
        {
            return Ok(
                _trafficService.GetAllTraffic().Select(traffic =>
                {
                    var region = _regionService.GetRegionByCode(traffic.RegionCode);
                    return DtoBuilder.GetTrafficDto(region, traffic);
                }
            ));
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

            return Ok(DtoBuilder.GetTrafficDto(region, traffic));
        }
    }
}
