using System;
using Labyrinths.Model.Structures;

namespace Labyrinths.Model
{
    public class LabyrinthProcessor
    {
        public static Vertice[] GetVerticeList(int[][] labyrinth, int[][] dots)
        {
            Queue<Vertice> verticeList = new Queue<Vertice>();
            for (int i = 1; i < labyrinth.Length - 1; i++)
            {
                for (int j = 1; j < labyrinth[i].Length - 1; j++)
                {
                    if (labyrinth[i][j] == 0 && (!IsTunnel(labyrinth, i, j) ||
                                                 i == dots[0][0] && j == dots[0][1] ||
                                                 i == dots[1][0] && j == dots[1][1]))
                    {
                        verticeList.Push(new Vertice(i, j));
                        labyrinth[i][j] = 2;
                    }
                }
            }

            Vertice[] vertices = new Vertice[verticeList.Count];
            for (int i = 0; i < vertices.Length; i++) vertices[i] = verticeList.Pop();
            return vertices;
        }

        private static bool IsTunnel(int[][] labyrinth, int i, int j)
        {
            return labyrinth[i][j - 1] == 1 && labyrinth[i][j + 1] == 1 && labyrinth[i - 1][j] != 1 &&
                labyrinth[i + 1][j] != 1 || labyrinth[i][j - 1] != 1 && labyrinth[i][j + 1] != 1 &&
                labyrinth[i - 1][j] == 1 && labyrinth[i + 1][j] == 1;
        }

        public static int[][] GetDistances(Vertice[] vertices, int[][] labyrinth)
        {
            int[][] distances = new int[vertices.Length][];
            for (int i = 0; i < vertices.Length; i++)
                distances[i] = new int[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                for (int j = i; j < vertices.Length; j++)
                {
                    distances[i][j] = Int32.MaxValue/2;
                    distances[j][i] = Int32.MaxValue/2;
                }
            }
            
            for (int i = 0; i < vertices.Length; i++)
            {
                
                for (int j = i + 1; j < vertices.Length; j++)
                {
                    if (IsAdjacent(vertices[i], vertices[j], labyrinth))
                    {
                        distances[i][j] = distances[j][i] = GetDistance(vertices[i], vertices[j]);
                    }
                }
            }

            return distances;
        }

        private static bool IsAdjacent(Vertice vertice1, Vertice vertice2, int[][] labyrinth)
        {
            if ((vertice1.X == vertice2.X) == (vertice1.Y == vertice2.Y))
            {
                return false;
            }

            for (int j = Math.Min(vertice1.Y, vertice2.Y) + 1; j < Math.Max(vertice1.Y, vertice2.Y); j++)
            {
                if (labyrinth[vertice1.X][j] > 0) return false;
            }

            for (int i = Math.Min(vertice1.X, vertice2.X) + 1; i < Math.Max(vertice1.X, vertice2.X); i++)
            {
                if (labyrinth[i][vertice1.Y] > 0) return false;
            }

            return true;
        }

        
        private static int GetDistance(Vertice vertice1, Vertice vertice2)
        {
            return (int) Math.Sqrt(Math.Pow(vertice1.X - vertice2.X, 2) + Math.Pow(vertice1.Y - vertice2.Y, 2));
        }

        public static int[] GetEntryPointIndexes(Vertice[] vertices, int[][] coordinates)
        {
            int found = 0;
            int[] indexes = new int[2];
            for (int i = 0; i < vertices.Length && found < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (vertices[i].X == coordinates[j][0] && vertices[i].Y == coordinates[j][1])
                    {
                        indexes[j] = i;
                        found++;
                    }
                }
            }

            return indexes;
        }
    }
}