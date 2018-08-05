using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class TrafficCache : ITrafficCache
    {
        public async Task<TrafficModel> GetByRegionCode(long regionCode)
        {
            using (var db = new CacheStorageContext())
            {
                return await db.Traffics.SingleOrDefaultAsync(t => t.RegionCode == regionCode);                
            }
        }

        public async Task Save(TrafficModel model)
        {
            using (var db = new CacheStorageContext())
            {
                var original = await db.Traffics.SingleOrDefaultAsync(t => t.RegionCode == model.RegionCode);
                if (original == null)
                {
                    model.UpdatedAt = DateTime.Now;
                    db.Traffics.Add(model);
                }
                else
                {
                    original.UpdatedAt = DateTime.Now;
                    original.Hint = model.Hint;
                    original.Level = model.Level;
                    db.Traffics.Update(original);
                }

                await db.SaveChangesAsync();              
            }
        }

        public void InitDatabase()
        {
            using (var db = new CacheStorageContext())
            {
                db.Database.EnsureCreated();                
            }
        }
    }
}
