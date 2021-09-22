using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrainModel
{
    class FileParser
    {
        public static Tuple<Graph, List<Train>> ParseJson(string path) {
            StreamReader sr = new StreamReader(path);
            var res = JsonConvert.DeserializeObject<JSONRepresentation>(sr.ReadToEnd());
            var nodes = res.Nodes;
            Graph graph = new Graph(nodes);
            foreach (var pair in res.Arcs) {
                var parentNode = graph.AllNodes[pair.Key[0].ToString()];
                var childNode = graph.AllNodes[pair.Key[1].ToString()];
                parentNode.AddArc(childNode, pair.Value);
            }
            List<Train> trains = new List<Train>();
            foreach (var item in res.Trains)
            {
                var train = new Train();
                foreach (var n in item.Trim())
                {
                    train.AppendNode(graph.AllNodes[n.ToString()]);
                }
                trains.Add(train);
            }
            return new Tuple<Graph, List<Train>>(graph, trains);
        }
                
    }
    public class JSONRepresentation { 
        public List<Node> Nodes { get; set; }
        public Dictionary<string, int> Arcs { get; set; }
        public List<string> Trains { get; set; }
    }
}
