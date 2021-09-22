using System.Collections.Generic;
using System.Linq;

namespace TrainModel
{
    public class CollisionDetector
    {
        private Graph graph;
        private List<Train> trains;
        public CollisionDetector(Graph graph, List<Train> trains) {
            this.graph = graph;
            this.trains = trains;
        }
        public bool DetectCollision() {
            for (int i = 0; i < trains.Count; i++) {
                for (int j = 1 + i; j < trains.Count; j++) { 
                    if (DetectTrainsCollision(trains[i], trains[j]))
                        return true;
                }
            }
            return false;
        }
        private bool DetectTrainsCollision(Train train1, Train train2) {
            if (HasSimilarPoints(train1, train2, out List<Node> points)) {
                foreach (var p in points) {
                    var t1Times = train1.GetTimeToNode(p);
                    var t2Times = train2.GetTimeToNode(p);
                    if (t1Times.Intersect(t2Times).Any()) {
                        return true;
                    }
                }
                var sa = train1.FindSameArcs(train2);
                return WillCollideOnArc(sa, train1, train2);
            }
            return false;
        }
        private bool WillCollideOnArc(List<Arc> arcs, Train train1, Train train2) {
            foreach (var arc in arcs) {
                var train1ArcTimes = train1.GetTimeToArc(arc);
                var train2ArcTimes = train2.GetTimeToArc(arc);
                for (int i = 0; i < train1ArcTimes.Count; i++) {
                    for (int j = 0; j < train2ArcTimes.Count; j++) {
                        if (DoTimeIntersect(train1ArcTimes[i], train2ArcTimes[j]))
                            return true;
                    }
                }
            }
            return false;
        }
        private bool DoTimeIntersect(ArcTime t1, ArcTime t2) {
            return t1.ChildTime >= t2.ParentTime && t1.Direction != t2.Direction;
        }
        public bool HasSimilarPoints(Train train1, Train train2, out List<Node> points) {
            points = train1.Path.Intersect(train2.Path).ToList<Node>();
            return points.Any();
        }
    }
}
