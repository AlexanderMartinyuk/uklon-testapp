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

        public static TrafficDto GetTrafficDto(TrafficModel trafficModel)
        {
            if (trafficModel.IsEmpty())
            {
                return null;
            }

            return new TrafficDto
            {
                Level = trafficModel.Level,
                Hint = trafficModel.Hint,                
            };
        }

        public static TrafficWithRegionDto GetTrafficWithRegionDto(TrafficModel traffic, RegionModel region)
        {
            return new TrafficWithRegionDto
            {
                Region = GetRegionDto(region),
                Traffic = GetTrafficDto(traffic)
            };
        }
    }
}
