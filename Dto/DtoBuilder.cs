using WebAPI.Models;

namespace WebAPI.Dto
{
    public static class DtoBuilder
    {
        public static RegionDto GetRegionDto(RegionModel model)
        {
            return new RegionDto
            {
                Code = model.Code,
                Name = model.Name
            };
        }

        public static TrafficDto GetTrafficDto(RegionModel regionModel, TrafficModel trafficModel)
        {
            return new TrafficDto
            {
                Level = trafficModel.Level,
                Hint = trafficModel.Hint,
                Region = GetRegionDto(regionModel)
            };
        }
    }
}
