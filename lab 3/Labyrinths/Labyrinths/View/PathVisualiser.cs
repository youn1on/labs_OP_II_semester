using System;
using System.Diagnostics.Tracing;
using Labyrinths.Model;
using Labyrinths.Model.Structures;

namespace Labyrinths.View
{

    public class PathVisualiser
    {
        public static char[][] Visualise(int[][] labyrinth, Stack<int> stack, Vertice[] vertices)
        {
            char[][] resultLabyrinth = new char[labyrinth.Length][];
            for (int i = 0; i < labyrinth.Length; i++)
            {
                resultLabyrinth[i] = new char[labyrinth[i].Length];
                for (int j = 0; j < labyrinth[i].Length; j++)
                {
                    resultLabyrinth[i][j] = labyrinth[i][j] == 1 ? 'X' : ' ';
                } 
            }
            int ctr = 1;
            Vertice first = vertices[stack.Pop()];
            Vertice next;
            resultLabyrinth[first.X][first.Y] = (char)(48);
            //Console.WriteLine(stack.Count); 
            while (stack.Count > 0)
            {
                next = vertices[stack.Pop()];
                if (first.X == next.X)
                {
                    if (first.Y > next.Y)
                    {
                        for (int i = first.Y-1; i >= next.Y; i--)
                        {
                            resultLabyrinth[first.X][i] = (char)(ctr+48);
                            ctr = ctr==9?0:ctr+1;
                        }
                    }
                    else
                    {
                        for (int i = first.Y+1; i <= next.Y; i++)
                        {
                            resultLabyrinth[first.X][i] = (char)(ctr+48);
                            ctr = ctr==9?0:ctr+1;
                        }
                    }
                }
                else
                {
                    if (first.X > next.X)
                    {
                        for (int i = first.X-1; i >= next.X; i--)
                        {
                            resultLabyrinth[i][first.Y] = (char)(ctr+48);
                            ctr = ctr==9?0:ctr+1;
                        }
                    }
                    else
                    {
                        for (int i = first.X+1; i <= next.X; i++)
                        {
                            resultLabyrinth[i][first.Y] = (char)(ctr+48);
                            ctr = ctr==9?0:ctr+1;
                        }
                    }
                }
                first = next;
            }

            return resultLabyrinth;
        }
    }
}