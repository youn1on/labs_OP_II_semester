namespace Labyrinths.Model.Structures
{
    public class Queue<T>
    {

        public int Count { get; protected set; }
        public Node<T>? Head { get; set; }
        private Node<T>? _tail;

        public Queue()
        {
            Count = 0;
            Head = null;
            _tail = null;
        }

        public virtual void Push(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (Head == null)
            {
                Head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }
            Count++;
        }

        public T Pop()
        {
            if (Head == null) throw new IndexOutOfRangeException();
            Node<T> headNode = Head;
            Head = Head.Next;
            Count--;
            return headNode.Value;
        }
    }
}
