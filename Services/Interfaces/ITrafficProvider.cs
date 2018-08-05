using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface ITrafficProvider
    { 
        TrafficModel GetTraffic(long regionCode);
    }
}
