using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface ITrafficProvider
    { 
        Task<TrafficModel> GetTrafficAsync(long regionCode);
    }
}
