using System;

namespace Labyrinths.Model
{
    public class AStarEuclidean : DijkstrasAlgorithm
    {
        public AStarEuclidean(Vertice[] vertices, int[][] distanceMatrix) : base(vertices, distanceMatrix) { }
        
        protected override double GetCriteria(Vertice current, Vertice finish)
        {
            return current.MinDistance + Math.Sqrt(Math.Pow(current.X - finish.X, 2) + Math.Pow(current.Y - finish.Y, 2));
        }
    }
}