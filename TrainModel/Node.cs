using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrainModel
{
    public class Node
    {
        public string Name;
        public List<Arc> Arcs = new List<Arc>();
        public Node(string name)
        {
            Name = name;
        }
        public Node AddArc(Node child, int w)
        {
            Arcs.Add(new Arc
            {
                Parent = this,
                Child = child,
                Weight = w
            });

            if (!child.Arcs.Exists(a => a.Parent == child && a.Child == this))
            {
                child.AddArc(this, w);
            }

            return this;
        }
        public override bool Equals(object obj)
        {
            var item = obj as Node;
            if (item == null)
                return false;
            return Name == item.Name;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}