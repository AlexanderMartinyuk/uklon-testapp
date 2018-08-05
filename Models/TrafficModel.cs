using System;
using System.Net.Security;

namespace WebAPI.Models
{
    public class TrafficModel
    {
        public long RegionCode { get; set; }
        public long Level { get; set; }
        public string Hint { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsEmpty()
        {
            return Level == -1 && Hint == null;
        }

        public TrafficModel(long regionCode, long level = -1, string hint = null)
        {
            this.Level = level;
            this.Hint = hint;
            this.RegionCode = regionCode;
        }
    }
}
