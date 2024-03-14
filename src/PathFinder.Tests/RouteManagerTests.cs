using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathFinder.Abstraction.Services;
using PathFinder.Application.Services;
using PathFinder.DataSeeder;
using System.Linq;

namespace PathFinder.Tests
{
    [TestClass]
    public class RouteManagerTests
    {
        private readonly IRouteManager _routeManager;
        private readonly IMapGenerator _mapGenerator;

        public RouteManagerTests()
        {
            _routeManager = new RouteManager();
            _mapGenerator = new MapGenerator();
        }

        [TestMethod]
        [DataRow("A","B",4, new string[] { "A", "B" })]
        [DataRow("E", "B", 2, new string[] { "E", "B" })]
        [DataRow("B", "E", 5, new string[] { "B", "F", "E" })]
        // We can add more tests here. Even mock them if we are generate the map based on user input.
        public void GetOptimalRoutReturnsCorrectResponse(string source, string destination, int distance, string[] route)
        {
            var map = _mapGenerator.SeedMap();

            var response = _routeManager.GetOptimalRoute(map, map.First(i => i.GetName() == source), map.First(i => i.GetName() == destination));

            Assert.AreEqual(distance, response.Distance);

            var routeResponse = response.OptimalRoute.ToArray();

            Assert.IsTrue(routeResponse.SequenceEqual(route));
        }
    }
}
