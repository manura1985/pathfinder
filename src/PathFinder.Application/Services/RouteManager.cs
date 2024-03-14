using PathFinder.Abstraction.Services;
using PathFinder.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Application.Services
{
    public class RouteManager : IRouteManager
    {
        private Map _map;
        private List<Node> _nodes;
        private Dictionary<Node, Node> _optimalRoutes;
        private Dictionary<Node, int> _optimalDistances;

        public Response GetOptimalRoute(Map map, Node source, Node destination)
        {
            ValidateRequest(map, source, destination);

            _map = map;
            _nodes = _map;
            SetDefaultDistances();
            SetDefaultRoutes();

            _optimalDistances[source] = 0;

            var nearestNeighbour = source;
            while (_nodes.Count != 0)
            {
                SetRouteDetails(nearestNeighbour);
                _nodes.Remove(nearestNeighbour);
                nearestNeighbour = GetNearestNeighbour();
            }

            var response = GetResponse(source, destination);

            return response;
        }

        private Response GetResponse(Node source, Node destination)
        {
            var response = new Response
            {
                Source = source.GetName(),
                Destination = destination.GetName(),
                Distance = _optimalDistances[destination],
                OptimalRoute = new LinkedList<string>()
            };

            SetOptimalRoute(destination, response.OptimalRoute);

            response.OptimalRoute.AddFirst(source.GetName());

            return response;
        }

        private void SetOptimalRoute(Node destination, LinkedList<string> route)
        {
            if (_optimalRoutes[destination] == null)
            {
                return;
            }
            route.AddFirst(destination.GetName());
            SetOptimalRoute(_optimalRoutes[destination], route);
        }

        private Node GetNearestNeighbour()
        {
            var nearestNeighbour = _nodes.FirstOrDefault();

            foreach (var node in _nodes)
            {
                if (_optimalDistances[node] < _optimalDistances[nearestNeighbour])
                    nearestNeighbour = node;
            }

            return nearestNeighbour;
        }

        private void SetRouteDetails(Node source)
        {
            foreach (var neighbor in source.GetNeighbours())
            {
                if (_optimalDistances[source] + neighbor.Value < _optimalDistances[neighbor.Key])
                {
                    _optimalDistances[neighbor.Key] = neighbor.Value + _optimalDistances[source];
                    _optimalRoutes[neighbor.Key] = source;
                }
            }
        }

        private void ValidateRequest(Map map, Node source, Node destination)
        {
            if (map is null)
            { 
                throw new ArgumentNullException(nameof(map));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
        }

        private void SetDefaultDistances()
        {
            var distances = new Dictionary<Node, int>();

            foreach (Node n in _map)
            {
                distances.Add(n, int.MaxValue);
            }

            _optimalDistances = distances;
        }

        private void SetDefaultRoutes()
        {
            var routes = new Dictionary<Node, Node>();

            foreach (Node n in _map)
            {
                routes.Add(n, null);
            }

            _optimalRoutes = routes;
        }
    }
}
