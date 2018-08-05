using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface ITrafficCache
    {
        Task<TrafficModel> GetByRegionCode(long regionCode);
        Task Save(TrafficModel model);
        void InitDatabase();
    }
}
