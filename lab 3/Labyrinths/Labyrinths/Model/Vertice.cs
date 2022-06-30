using System;

namespace Labyrinths.Model
{
    public class Vertice
    {
        public int X { get; }
        public int Y { get; }

        public int MinDistance = Int32.MaxValue/2;
        public bool Passed = false;
        public int Previous;

        public Vertice(int x, int y)
        {
            X = x;
            Y = y;
            Previous = -1;
        }
    }
}