﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;
using System.Xml;
using System.Xml.Linq;

namespace WebAPI.Services
{
    public class YandexTrafficService
    {
        private const string GET_TRAFFIC_DATA_URL = "https://export.yandex.com/bar/reginfo.xml?region={0}&bustCache={1}";

        public IEnumerable<TrafficModel> GetAllTraffic()
        {
            var regionService = new RegionsService();
            var result = new List<TrafficModel>();

            foreach (var region in regionService.GetAllRegions())
            {
                result.Add(GetTrafficForRegion(region.Code));
            }

            return result;
        }

        public TrafficModel GetTrafficForRegion(long regionCode)
        {
            var httpClient = new HttpClient(new HttpClientHandler
            {
                Proxy = new WebProxy
                {
                    Address = new Uri("http://88.99.149.188:31288"),
                    UseDefaultCredentials = true
                }
            });

            var url = GetRequestUrl(regionCode);
            var result = httpClient.GetAsync(url).Result;
            var stream = result.Content.ReadAsStreamAsync().Result;

            return GetTrafficModelFromXml(stream);
        }

        private TrafficModel GetTrafficModelFromXml(Stream stream)
        {
            var xmldoc = XDocument.Load(stream);
            var trafficElement = xmldoc.Element("info")?.Element("traffic");

            if (trafficElement == null)
            {
                throw new Exception("Traffic element cannot be found.");
            }

            var regionElement = trafficElement.Element("region");
            if (regionElement == null)
            {
                // data is absent for specified region
                return null;
            }

            var level = regionElement.Element("level")?.Value;
            var hint = regionElement.Element("hint")?.Value;

            return new TrafficModel(long.Parse(level), hint);
        }

        private string GetRequestUrl(long regionCode)
        {
            return string.Format(GET_TRAFFIC_DATA_URL, regionCode, string.Empty);
        }
    }
}
