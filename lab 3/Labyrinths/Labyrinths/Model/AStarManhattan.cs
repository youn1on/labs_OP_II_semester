using System;

namespace Labyrinths.Model
{
    public class AStarManhattan : DijkstrasAlgorithm
    {
        public AStarManhattan(Vertice[] vertices, int[][] distanceMatrix) : base(vertices, distanceMatrix) { }
        
        protected override double GetCriteria(Vertice current, Vertice finish)
        {
            return current.MinDistance + Math.Abs(current.X - finish.X) + Math.Abs(current.Y - finish.Y);
        }
    }
}