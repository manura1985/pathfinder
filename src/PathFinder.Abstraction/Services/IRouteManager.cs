using PathFinder.Application.Services;
using PathFinder.Domain;

namespace PathFinder.Abstraction.Services
{
    public interface IRouteManager
    {
        Response GetOptimalRoute(Map map, Node source, Node destination);
    }
}
