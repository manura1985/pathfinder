using PathFinder.Domain;

namespace PathFinder.DataSeeder
{
    public class MapGenerator : IMapGenerator
    {
        public Map SeedMap()
        {
            Map map = new Map();

            Node A = new Node("A");
            Node B = new Node("B");
            Node C = new Node("C");
            Node D = new Node("D");
            Node E = new Node("E");
            Node F = new Node("F");
            Node G = new Node("G");
            Node H = new Node("H");
            Node I = new Node("I");

            map.Add(A);
            map.Add(B);
            map.Add(C);
            map.Add(D);
            map.Add(E);
            map.Add(F);
            map.Add(G);
            map.Add(H);
            map.Add(I);

            A.AddNeighbour(B, 4, true);
            A.AddNeighbour(C, 6, true);

            B.AddNeighbour(F, 2, true);

            C.AddNeighbour(D, 8, true);

            D.AddNeighbour(E, 4, true);
            D.AddNeighbour(G, 1, true);

            E.AddNeighbour(B, 2);
            E.AddNeighbour(F, 3, true);
            E.AddNeighbour(I, 8, true);

            F.AddNeighbour(G, 4, true);
            F.AddNeighbour(H, 6, true);

            G.AddNeighbour(H, 5, true);
            G.AddNeighbour(I, 5, true);

            return map;
        }
    }
}
