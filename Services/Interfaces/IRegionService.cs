using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IRegionService
    {
        IEnumerable<RegionModel> GetAllRegions();

        RegionModel GetRegionByCode(long code);
    }
}