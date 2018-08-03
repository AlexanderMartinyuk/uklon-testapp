using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TrafficModel
    {
        public TrafficModel(long level, string hint)
        {
            this.Level = level;
            this.Hint = hint;
        }

        public long Level { get; set; }
        public string Hint { get; set; }

        // can be extended with real data accroding the requirements
    }
}
