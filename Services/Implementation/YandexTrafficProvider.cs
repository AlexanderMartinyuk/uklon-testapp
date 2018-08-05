using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class YandexTrafficProvider : StubTrafficProvider
    {
        private const string TrafficDataApiUrl = "https://export.yandex.com/bar/reginfo.xml?region={0}&bustCache={1}";
        private string _proxy;

        public YandexTrafficProvider(string proxy, IRegionService regionService) : base(regionService)
        {
            _proxy = proxy;
        }

        public override TrafficModel GetTraffic(long regionCode)
        {
            var httpClient = new HttpClient(new HttpClientHandler
            {
                Proxy = new WebProxy
                {
                    // use proxy to access Yandex in Ukraine
                    Address = new Uri(_proxy),
                    UseDefaultCredentials = true
                }
            });

            var url = GetRequestUrl(regionCode);
            var responce = httpClient.GetAsync(url).Result;
            var stream = responce.Content.ReadAsStreamAsync().Result;

            return GetTrafficModelFromXml(stream, regionCode); ;
        }

        private TrafficModel GetTrafficModelFromXml(Stream stream, long regionCode)
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
            var hint = regionElement.Elements("hint").Single(el => (string)el.Attribute("lang") == "uk").Value;

            return new TrafficModel(long.Parse(level), hint, regionCode);
        }

        private string GetRequestUrl(long regionCode)
        {
            return string.Format(TrafficDataApiUrl, regionCode, string.Empty);
        }
    }
}
