using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetTrafficForAllRegionsAsync()
        {
            var traffics = await _trafficService.GetAllTrafficAsync();
            return Ok(
                traffics.
                Where(traffic => traffic != null).
                Select(traffic =>
                {

                    var region = _regionService.GetRegionByCode(traffic.RegionCode);
                    return DtoBuilder.GetTrafficWithRegionDto(traffic, region);
                }
            ));
        }

        [HttpGet]
        [Route("api/traffic/{regionCode}")]
        public async Task<IActionResult> GetTrafficForRegionAsync(long regionCode)
        {
            var region = _regionService.GetRegionByCode(regionCode);
            if (region == null)
            {
                return NotFound();
            }

            var traffic = await _trafficService.GetTrafficForRegionAsync(region);
            if (traffic == null)
            {
                return NotFound();
            }

            return Ok(DtoBuilder.GetTrafficWithRegionDto(traffic, region));
        }
    }
}
