namespace WebAPI.Models
{
    public static class ModelsFactory
    {
        public static RegionModel NewRegionModel(long code, string name)
        {
            return new RegionModel(code, name);
        }

        public static TrafficModel NewTrafficModel(long regionCode, long level, string hint)
        {
            return new TrafficModel(regionCode, level, hint);
        }

        public static TrafficModel NewEmptyTrafficModel(long regionCode)
        {
            return new TrafficModel(regionCode);
        }
    }
}