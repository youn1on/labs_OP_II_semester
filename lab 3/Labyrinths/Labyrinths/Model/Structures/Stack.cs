namespace Labyrinths.Model.Structures
{
    public class Stack<T> : Queue<T>
    {
        public Stack() : base() { }

        public override void Push(T value)
        {
            Node<T> newNode = new Node<T>(value);

            newNode.Next = Head;
            Head = newNode;
            Count++;
        }
    }
}