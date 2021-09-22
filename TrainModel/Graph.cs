using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainModel
{
    public class Graph
    {
        public Dictionary<string, Node> AllNodes = new Dictionary<string, Node>();
        public Graph(List<Node> nodes){
            AllNodes = nodes.ToDictionary(n => n.Name);
        }
    }
}
