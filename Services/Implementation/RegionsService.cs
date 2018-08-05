using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Csv;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class RegionsService : IRegionService
    {
        private readonly Dictionary<long, string> _data = new Dictionary<long, string>();

        public RegionsService()
        {
            ReadDataFromFile();
        }

        public IEnumerable<RegionModel> GetAllRegions()
        {
            var result = new List<RegionModel>();

            foreach (var region in _data)
            {
                result.Add(new RegionModel(region.Key, region.Value));
            }

            return result.AsReadOnly();
        }

        public RegionModel GetRegionByCode(long code)
        {
            if (_data.TryGetValue(code, out var name))
            {
                return new RegionModel(code, name);
            }

            return null;            
        }

        private void ReadDataFromFile()
        {
            var assemblyFullPath = System.Reflection.Assembly.GetAssembly(typeof(RegionsService)).Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyFullPath);
            var dataFilePath = Path.Combine(assemblyDirectory, "Services", "Data", "regions.csv");

            using (var stream = File.Open(dataFilePath, FileMode.Open, FileAccess.Read))
            {
                var lines = CsvReader.ReadFromStream(stream).ToList();
                foreach (var line in lines)
                {
                    var code = line[0];
                    var name = line[1];

                    if (!long.TryParse(code, out long codeNum))
                    {
                        throw new Exception($"Incorrect format of file: {dataFilePath}");
                    }

                    _data[codeNum] = name;
                }
            }
        }
    }
}