using System;
using System.Linq;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class TrafficCache : ITrafficCache, IDisposable
    {
        private readonly CacheStorageContext _context;

        public TrafficCache()
        {
            _context = new CacheStorageContext();
        }

        public virtual TrafficModel GetByRegionCode(long regionCode)
        {
            lock (_context)
            {
                return _context.Traffics.SingleOrDefault(t => t.RegionCode == regionCode);
            }           
        }

        public virtual void Save(TrafficModel model)
        {
            lock (_context)
            {
                var original = _context.Traffics.SingleOrDefault(t => t.RegionCode == model.RegionCode);
                if (original == null)
                {
                    model.UpdatedAt = DateTime.Now;
                    _context.Traffics.Add(model);
                }
                else
                {
                    original.UpdatedAt = DateTime.Now;
                    original.Hint = model.Hint;
                    original.Level = model.Level;
                    _context.Traffics.Update(original);
                }

                _context.SaveChangesAsync();
            }
        }

        public virtual void InitDatabase()
        {
            _context.Database.EnsureCreated();                
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
