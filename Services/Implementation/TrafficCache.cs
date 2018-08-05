using System;
using System.Linq;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class TrafficCache : ITrafficCache
    {
        public TrafficModel GetByRegionCode(long regionCode)
        {
            using (var db = new CacheStorageContext())
            {
                return db.Traffics.SingleOrDefault(t => t.RegionCode == regionCode);                
            }
        }

        public void Save(TrafficModel model)
        {
            using (var db = new CacheStorageContext())
            {
                var original = db.Traffics.SingleOrDefault(t => t.RegionCode == model.RegionCode);
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

                db.SaveChanges();              
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
