using PathFinder.Domain;

namespace PathFinder.Application.Services
{
    public interface IRouteManager
    {
        Response GetOptimalRoute(Map map, Node source, Node destination);
    }
}
