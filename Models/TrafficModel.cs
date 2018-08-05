using System;

namespace WebAPI.Models
{
    public class TrafficModel
    {
        public long RegionCode { get; set; }
        public long Level { get; set; }
        public string Hint { get; set; }
        public DateTime UpdatedAt { get; set; }

        public TrafficModel(long level, string hint, long regionCode)
        {
            this.Level = level;
            this.Hint = hint;
            this.RegionCode = regionCode;
        }
    }
}
