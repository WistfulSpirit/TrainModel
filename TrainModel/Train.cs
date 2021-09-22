using System.Collections.Generic;
using System.Linq;

namespace TrainModel
{
   
    public class Train
    {
        private List<Node> path = new List<Node>();
        private List<Arc> pathArcs = new List<Arc>();

        public List<Node> Path { get => path; }
        public List<Arc> PathArcs { get => pathArcs; }
        public Train(List<Node> path) {
            this.path = path;
            for(int i = 0; i < path.Count - 1; i++) {
                pathArcs.Add(path[i].Arcs.Where(a => a.Child.Name == path[i + 1].Name).FirstOrDefault());
            }
        }
        public List<int> GetTimeToNode(Node node) {
            int spentTime = 0;
            List<int> times = new List<int>();
            if (pathArcs.First().Parent == node)
                times.Add(spentTime);
            foreach(var n in pathArcs) {
                spentTime += n.Weight;
                if (n.Child == node) { 
                    times.Add(spentTime);
                }
            }
            return times;
        }
        public List<ArcTime> GetTimeToArc(Arc arc) {
            int prevWeight = 0;
            ArcTime spentTime = new ArcTime();
            List<ArcTime> times = new List<ArcTime>();
            foreach (var carc in pathArcs) {
                spentTime.ParentTime += prevWeight;
                spentTime.ChildTime += carc.Weight;
                spentTime.Direction = carc.Parent.Name + carc.Child.Name;
                if (carc.Equals(arc)) {
                    times.Add(new ArcTime(spentTime.ParentTime, spentTime.ChildTime, spentTime.Direction));
                }
                prevWeight = carc.Weight;
            }
            return times;
        }
        
        public Train() { }
        public void AppendNode(Node node) {
            path.Add(node);
            ReculcPathArcs();
        }
        private void ReculcPathArcs() {
            pathArcs.Clear();
            for (int i = 0; i < path.Count - 1; i++)
            {
                pathArcs.Add(path[i].Arcs.Where(a => a.Child.Name == path[i + 1].Name).FirstOrDefault());
            }
        }
        public List<Arc> FindSameArcs(Train train2) {
            return PathArcs.Intersect(train2.PathArcs, EqualityComparer<Arc>.Default).ToList();
        }
    }
}
