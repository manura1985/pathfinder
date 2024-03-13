using Microsoft.Extensions.DependencyInjection;
using PathFinder.Application.Services;
using PathFinder.DataSeeder;
using System;
using System.Linq;
using System.Text;

namespace PathFinder.TestingConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();
            
            IMapGenerator mapGenerator = serviceProvider.GetRequiredService<IMapGenerator>();
            IRouteManager routeManager = serviceProvider.GetRequiredService<IRouteManager>();

            var map = mapGenerator.SeedMap();

            var nodes = map.GetNodes();

            var source = nodes.Where(i => i.GetName() == "D").First();
            var destination = nodes.Where(i => i.GetName() == "B").First();

            var route = routeManager.GetOptimalRoute(map, source, destination);

            Console.WriteLine(string.Format("Shortest distance to drive from {0} to {1} is: {2} KM", route.Source, route.Destination, route.Distance));

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

            Console.WriteLine(routeBuilder.ToString());
            Console.ReadLine();
        }

        private static IServiceProvider CreateServices()
        {
            var services = new ServiceCollection()
                .AddSingleton<IMapGenerator, MapGenerator>()
                .AddSingleton<IRouteManager, RouteManager>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
