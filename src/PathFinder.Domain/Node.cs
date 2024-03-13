using System.Collections.Generic;

namespace PathFinder.Domain
{
    public class Node
    {
        private string _name;
        private Dictionary<Node, int> _neighbours;

        public Node(string name)
        {
            _name = name;
            _neighbours = new Dictionary<Node, int>();
        }

        public void AddNeighbour(Node node, int distance, bool isBidirectional = false) 
        {
            _neighbours.Add(node, distance);

            if (isBidirectional)
            {
                node._neighbours.Add(this, distance);
            }
        }

        public string GetName()
        { 
            return _name;
        }

        public Dictionary<Node, int> GetNeighbours()
        {
            return _neighbours;
        }
    }
}
