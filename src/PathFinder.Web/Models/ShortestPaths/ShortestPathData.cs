namespace PathFinder.Web.Models.ShortestPaths
{
    public class ShortestPathData
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public int Distance { get; set; }

        public string Description { get; set; }
    }
}