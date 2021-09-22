using System;

namespace TrainModel
{
    public class Arc
    {
        public Node Parent { get; set; }
        public Node Child { get; set; }
        public int Weight { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Arc;
            if (item == null)
                return false;
            return ((Parent.Equals(item.Parent) && Child.Equals(item.Child)) || (Child.Equals(item.Parent) && Parent.Equals(item.Child)));
        }
        public override int GetHashCode()
        {
            return Parent.GetHashCode() + Child.GetHashCode() + Weight.GetHashCode();
        }
    }
    public class ArcTime
    {
        public int ParentTime { get; set; }
        public int ChildTime { get; set; }
        public ArcTime() { }
        public string Direction { get; set; }
        public ArcTime(int t1, int t2, string direction)
        {
            ParentTime = t1;
            ChildTime = t2;
            Direction = direction;
        }
    }
}