namespace Labyrinths.Model.Structures
{
    public class PriorityQueue<T> : Queue<T>
    {
        public PriorityQueue() : base() { }

        public void Push(T value, double criteria)
        {
            Node<T> newNode = new Node<T>(value, criteria);
            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                Node<T> last = Head;
                while (last.Next != null || last.Next.Criteria <= newNode.Criteria) last = last.Next;
                newNode.Next = last.Next;
                last.Next = newNode;
            }

            Count++;
        }
    }
}