using System.Collections.Generic;
using System.Linq;

namespace PathFinder.Domain
{
    public class Map
    {
        private readonly List<Node> _nodes;

        public Map() : this(new List<Node>())
        {
        }

        public Map(List<Node> nodes)
        {
            _nodes = nodes;
        }
        public void Add(Node node)
        {
            _nodes.Add(node);
        }

        public List<Node> GetNodes()
        {
            return _nodes.ToList();
        }

        public int Count()
        {
            return _nodes.Count;
        }
    }
}
