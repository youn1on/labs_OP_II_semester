using System;
using Labyrinths.Controller;
using Labyrinths.Model.Structures;
using Labyrinths.View;

namespace Labyrinths.Model
{
    public static class Program
    {
        public static void Main(string[] args)
        {
           string path = FilenameOperations.GetPath();
           string[]? content = FileOperations.GetFileContent(path);
           if (content is null)
           {
               Console.WriteLine("Incorrect file");
               return;
           }

           int[][] labyrinth = FileContentProcessor.GetLabyrinthMatrix(content);
           int[][] entryPointsCoordinates = DotsInput.GetDots(labyrinth);
           Vertice[] vertices = LabyrinthProcessor.GetVerticeList(labyrinth, entryPointsCoordinates);
           int[][] distances = LabyrinthProcessor.GetDistances(vertices, labyrinth);
           int[] entryPointIndexes = LabyrinthProcessor.GetEntryPointIndexes(vertices, entryPointsCoordinates);
           DijkstrasAlgorithm algo = new DijkstrasAlgorithm(vertices, distances);
           algo.FindRoute(entryPointIndexes[0], entryPointIndexes[1]);
           Stack<int> route = algo.TraceRoute(entryPointIndexes[1]);
           char[][] resultLabyrinth = PathVisualiser.Visualise(labyrinth, route, vertices);
           OutputOperations.PrintLabyrinth(resultLabyrinth);
        }
    }
}