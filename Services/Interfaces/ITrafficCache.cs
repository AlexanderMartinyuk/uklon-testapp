using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface ITrafficCache
    {
        TrafficModel GetByRegionCode(long regionCode);
        void Save(TrafficModel model);
        void InitDatabase();
    }
}
