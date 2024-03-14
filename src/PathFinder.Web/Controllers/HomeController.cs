using PathFinder.Abstraction.Services;
using PathFinder.DataSeeder;
using PathFinder.Web.Models.ShortestPaths;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PathFinder.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRouteManager _routeManager;
        private readonly IMapGenerator _mapGenerator;

        public HomeController(IRouteManager routeManager, IMapGenerator mapGenerator)
        {
            _routeManager = routeManager;
            _mapGenerator = mapGenerator;
        }

        public ActionResult Index()
        {
            var map = _mapGenerator.SeedMap();

            var nodes = map;

            return View(nodes);
        }

        public PartialViewResult FindRoute(string source, string destination)
        {
            var map = _mapGenerator.SeedMap();

            var nodes = map;

            // ToDo: Should validate the request and return a message as a response to display in the UI
            var sourceName = nodes.First(i => i.GetName() == source);
            var destinationNode = nodes.First(i => i.GetName() == destination);

            var route = _routeManager.GetOptimalRoute(map, sourceName, destinationNode);

            // in web api we shall return the serialized original route.
            var pathdata = new ShortestPathData
            {
                Source = route.Source,
                Destination = route.Destination,
                Distance = route.Distance,
            };

            // ToDo: Is this view logic
            var routeBuilder = new StringBuilder($"Most optimal route from {route.Source} to {route.Destination} is : ");

            var optimalRoute = route.OptimalRoute.ToList();

            for (int node = 0; node < optimalRoute.Count; node++)
            {
                if (node != optimalRoute.Count - 1)
                {
                    routeBuilder.Append($"{optimalRoute[node]},");
                }
                else
                {
                    routeBuilder.Append($"{optimalRoute[node]}");
                }
            }

            pathdata.Description = routeBuilder.ToString();

            return PartialView(pathdata);
        }
    }
}