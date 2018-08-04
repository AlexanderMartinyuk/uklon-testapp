using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IRegionService
    {
        IEnumerable<RegionModel> GetAllRegions();

        RegionModel GetRegionByCode(long code);
    }
}
