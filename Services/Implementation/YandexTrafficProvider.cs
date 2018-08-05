using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementation
{
    public class YandexTrafficProvider : StubTrafficProvider
    {
        private const string TrafficDataApiUrl = "https://export.yandex.com/bar/reginfo.xml?region={0}&bustCache={1}";
        private readonly string _proxy;

        public YandexTrafficProvider(string proxy, IRegionService regionService) : base(regionService)
        {
            _proxy = proxy;
        }

        public override async Task<TrafficModel> GetTrafficAsync(long regionCode)
        {
            try { 
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
                var responce = await httpClient.GetAsync(url);
                var stream = responce.Content.ReadAsStreamAsync().Result;

                return await GetTrafficModelFromXmlAsync(stream, regionCode); ;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return new TrafficModel(regionCode);
        }

        private async Task<TrafficModel> GetTrafficModelFromXmlAsync(Stream stream, long regionCode)
        {
            var xmldoc = await XDocument.LoadAsync(stream, LoadOptions.PreserveWhitespace, CancellationToken.None);
            var trafficElement = xmldoc.Element("info")?.Element("traffic");

            if (trafficElement == null)
            {
                throw new Exception("Traffic element cannot be found.");
            }

            var regionElement = trafficElement.Element("region");
            if (regionElement == null)
            {
                // data is absent for specified region
                return new TrafficModel(regionCode);
            }

            var level = regionElement.Element("level")?.Value;
            var hint = regionElement.Elements("hint").Single(el => (string)el.Attribute("lang") == "uk").Value;

            return new TrafficModel(regionCode, long.Parse(level), hint);
        }

        private string GetRequestUrl(long regionCode)
        {
            return string.Format(TrafficDataApiUrl, regionCode, string.Empty);
        }
    }
}
