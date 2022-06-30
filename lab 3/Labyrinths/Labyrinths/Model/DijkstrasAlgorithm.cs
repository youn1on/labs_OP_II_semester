using System;
using Labyrinths.Model.Structures;

namespace Labyrinths.Model
{
    public class DijkstrasAlgorithm
    {
        protected readonly Vertice[] Vertices;
        protected readonly int[][] DistanceMatrix;

        public DijkstrasAlgorithm(Vertice[] vertices, int[][] distanceMatrix)
        {
            Vertices = vertices;
            DistanceMatrix = distanceMatrix;
        }
        public bool FindRoute(int startPointIndex, int endPointIndex)
        {
            PriorityQueue<int> queue = new();
            Vertices[startPointIndex].MinDistance = 0;
            queue.Push(startPointIndex, 0);
            
            while (queue.Count > 0)
            {
                int currentVertice = queue.Pop();
                if (currentVertice == endPointIndex) return true;
                for (int i = 0; i<Vertices.Length; i++)
                {
                    if (DistanceMatrix[currentVertice][i]==Int32.MaxValue/2) continue;
                    if (Vertices[i].MinDistance >
                        Vertices[currentVertice].MinDistance + DistanceMatrix[currentVertice][i])
                    {
                        Vertices[i].MinDistance =
                            Vertices[currentVertice].MinDistance + DistanceMatrix[currentVertice][i];
                        Vertices[i].Previous = currentVertice;
                    }

                    if (!Vertices[i].Passed)
                    {
                        queue.Push(i, GetCriteria(Vertices[i], Vertices[endPointIndex]));
                    }
                }

                Vertices[currentVertice].Passed = true;
            }

            return false;
        }

        protected virtual double GetCriteria(Vertice current, Vertice finish)
        {
            return current.MinDistance;
        }

        public Stack<int> TraceRoute(int finishIndex)
        {
            Structures.Stack<int> route = new();
            int current = finishIndex;
            while (current > -1)
            {
                route.Push(current);
                current = Vertices[current].Previous;
            }

            return route;
        }
    }
}