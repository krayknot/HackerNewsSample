using System;
using System.Collections.Generic;

namespace Collabra_Test.Models
{
    public class HackerNewsStory
    {
        public string author { get; set; }
        public int descendants { get; set; }
        public int id { get; set; }
        public List<int> kids { get; set; }
        public int score { get; set; }
        public int time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }
}