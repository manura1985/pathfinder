using PathFinder.Application.Common;
using System.Collections.Generic;

namespace PathFinder.Application.Services
{
    public class Response : BaseResponse
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public int Distance { get; set; }

        public LinkedList<string> OptimalRoute { get; set; }
    }
}
