using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAPI.Controllers;
using WebAPI.Dto;
using WebAPI.Models;
using WebAPI.Services.Implementation;
using WebAPI.Services.Interfaces;

namespace WebAPI.Tests
{
    [TestClass]
    public class CachedTrafficServiceTests
    {
        private RegionModel _region1;
        private TrafficModel _traffic1;

        [TestInitialize]
        public void TestInitialize()
        {
            _region1 = ModelsFactory.NewRegionModel(1, "First Region");
            _traffic1 = ModelsFactory.NewTrafficModel(_region1.Code, 2, "No jams");
        }

        [TestMethod]
        public async Task ShouldGetTrafficForRegionWithEmptyCacheSuccessfully()
        {
            TrafficModel cachedValue = null;

            var trafficCache = new Mock<ITrafficCache>();
            trafficCache.Setup(p => p.GetByRegionCode(_region1.Code)).Returns(() =>
            {
                return cachedValue;
            });
            trafficCache.Setup(p => p.Save(_traffic1)).Callback((TrafficModel traffic) =>
            {
                cachedValue = traffic;
            });

            var trafficProvider = new Mock<ITrafficProvider>();
            trafficProvider.Setup(p => p.GetTrafficAsync(_region1.Code)).Returns(Task.Run(() => _traffic1));

            var regionService = new Mock<IRegionService>();

            var service = new CachedTrafficService(trafficCache.Object, trafficProvider.Object, regionService.Object);
            var result = await service.GetTrafficForRegionAsync(_region1);

            Assert.IsNotNull(result);
            Assert.AreEqual(_traffic1.RegionCode, result.RegionCode);
            Assert.AreEqual(_traffic1.Level, result.Level);
            Assert.AreEqual(_traffic1.Hint, result.Hint);

            trafficCache.Verify(tc => tc.Save(It.IsAny<TrafficModel>()), Times.Once());
            trafficProvider.Verify(tc => tc.GetTrafficAsync(_region1.Code), Times.Once());
        }

        [TestMethod]
        public async Task ShouldGetTrafficForRegionForOutdatedCacheSuccessfully()
        {
            _traffic1.UpdatedAt = DateTime.MinValue;

            var trafficCache = new Mock<ITrafficCache>();
            trafficCache.Setup(p => p.GetByRegionCode(_region1.Code)).Returns(_traffic1);
            trafficCache.Setup(p => p.Save(_traffic1));

            var trafficProvider = new Mock<ITrafficProvider>();
            trafficProvider.Setup(p => p.GetTrafficAsync(_region1.Code)).Returns(Task.Run(() => _traffic1));

            var regionService = new Mock<IRegionService>();

            var service = new CachedTrafficService(trafficCache.Object, trafficProvider.Object, regionService.Object);
            var result = await service.GetTrafficForRegionAsync(_region1);

            Assert.IsNotNull(result);
            Assert.AreEqual(_traffic1.RegionCode, result.RegionCode);
            Assert.AreEqual(_traffic1.Level, result.Level);
            Assert.AreEqual(_traffic1.Hint, result.Hint);

            trafficCache.Verify(tc => tc.Save(It.IsAny<TrafficModel>()), Times.Once());
            trafficProvider.Verify(tc => tc.GetTrafficAsync(_region1.Code), Times.Once());
        }

        [TestMethod]
        public async Task ShouldGetTrafficForRegionFromCacheSuccessfully()
        {
            _traffic1.UpdatedAt = DateTime.Now;

            var trafficCache = new Mock<ITrafficCache>();
            trafficCache.Setup(p => p.GetByRegionCode(_region1.Code)).Returns(_traffic1);
            
            var trafficProvider = new Mock<ITrafficProvider>();
            trafficProvider.Setup(p => p.GetTrafficAsync(_region1.Code)).Returns(Task.Run(() => _traffic1));

            var regionService = new Mock<IRegionService>();

            var service = new CachedTrafficService(trafficCache.Object, trafficProvider.Object, regionService.Object);
            var result = await service.GetTrafficForRegionAsync(_region1);

            Assert.IsNotNull(result);
            Assert.AreEqual(_traffic1.RegionCode, result.RegionCode);
            Assert.AreEqual(_traffic1.Level, result.Level);
            Assert.AreEqual(_traffic1.Hint, result.Hint);

            trafficCache.Verify(tc => tc.Save(It.IsAny<TrafficModel>()), Times.Never());
            trafficProvider.Verify(tc => tc.GetTrafficAsync(_region1.Code), Times.Never());
        }
    }
}
