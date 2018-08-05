using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAPI.Controllers;
using WebAPI.Dto;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Tests
{
    [TestClass] 
    public class TrafficControllerTests
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
        public void ShouldReturnAllRegionsSuccesfully()
        {
            var regionService = new Mock<IRegionService>();
            regionService.Setup(p => p.GetAllRegions()).Returns(new List<RegionModel>{ _region1 });

            var trafficService = new Mock<ITrafficService>();
            
            TrafficController controller = new TrafficController(regionService.Object, trafficService.Object);

            var result = controller.GetAllRegions();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var dtos = (okResult.Value as IEnumerable<RegionDto>).ToList();
            Assert.AreEqual(1, dtos.Count);
            Assert.AreEqual(_region1.Code, dtos.ElementAt(0).Code);
            Assert.AreEqual(_region1.Name, dtos.ElementAt(0).Name);
        }

        [TestMethod]
        public async Task ShouldReturnTrafficForRegionSuccesfully()
        {
            var regionService = new Mock<IRegionService>();
            regionService.Setup(p => p.GetRegionByCode(_region1.Code)).Returns(_region1);

            var trafficService = new Mock<ITrafficService>();
            trafficService.Setup(p => p.GetTrafficForRegionAsync(_region1)).Returns(
                Task.Run(() => _traffic1));

            TrafficController controller = new TrafficController(regionService.Object, trafficService.Object);
            var result = await controller.GetTrafficForRegionAsync(_region1.Code);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var dto = okResult.Value as TrafficWithRegionDto;
            Assert.AreEqual(_region1.Code, dto.Region.Code);
            Assert.AreEqual(_region1.Name, dto.Region.Name);
            Assert.AreEqual(_traffic1.Level, dto.Traffic.Level);
            Assert.AreEqual(_traffic1.Hint, dto.Traffic.Hint);
        }

        [TestMethod]
        public async Task ShouldReturnAllTrafficSuccesfully()
        {
            var regionService = new Mock<IRegionService>();
            regionService.Setup(p => p.GetRegionByCode(_region1.Code)).Returns(_region1);

            var trafficService = new Mock<ITrafficService>();
            trafficService.Setup(p => p.GetAllTrafficAsync()).Returns(
                Task.Run(() => new List<TrafficModel>{ _traffic1 }.AsEnumerable()));

            TrafficController controller = new TrafficController(regionService.Object, trafficService.Object);
            var result = await controller.GetTrafficForAllRegionsAsync();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var dtos = (okResult.Value as IEnumerable<TrafficWithRegionDto>).ToList();
            Assert.AreEqual(1, dtos.Count);
            Assert.AreEqual(_region1.Code, dtos.ElementAt(0).Region.Code);
            Assert.AreEqual(_region1.Name, dtos.ElementAt(0).Region.Name);
            Assert.AreEqual(_traffic1.Level, dtos.ElementAt(0).Traffic.Level);
            Assert.AreEqual(_traffic1.Hint, dtos.ElementAt(0).Traffic.Hint);
        }
    }
}
