namespace Labyrinths.Model.Structures
{
    public class Node<T>
    {
        public T Value { get; }
        public Node<T>? Next { get; set; }
        public double Criteria { get; }


        public Node(T value, double criteria = 0)
        {
            Value = value;
            Next = null;
            Criteria = criteria;
        }
    }
}