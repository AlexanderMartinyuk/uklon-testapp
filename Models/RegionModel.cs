using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class RegionModel
    {
        public RegionModel(long code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        public long Code { get; set; }
        public string Name { get; set; }
    }
}
