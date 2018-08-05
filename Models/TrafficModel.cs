using System;

namespace WebAPI.Models
{
    public class TrafficModel
    {
        public TrafficModel(long regionCode, long level = -1, string hint = null)
        {
            Level = level;
            Hint = hint;
            RegionCode = regionCode;
        }

        public long RegionCode { get; set; }
        public long Level { get; set; }
        public string Hint { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsEmpty()
        {
            return Level == -1 && Hint == null;
        }
    }
}