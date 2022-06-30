using System;

namespace Labyrinths.Controller{

    public class DotsInput
    {

        public static int[][] GetDots(int[][] labyrinth)
        {
            Console.WriteLine("Enter startpoint");
            int[] startpointCoordinates = GetValidCoordinates(labyrinth);
            Console.WriteLine("Enter endpoint");
            int[] endpointCoordinates = GetValidCoordinates(labyrinth);
            return new int[][] {startpointCoordinates, endpointCoordinates};

        }

        public static int[] GetValidCoordinates(int[][] labyrinth)
        {
            int[] coordinates = new int[2];
            while (true)
            {
                Console.WriteLine("Enter your coordinates separated by ',':");
                string? readInput = Console.ReadLine();
                if (readInput is null) continue;
                string[] input = readInput.Split(",");
                if (int.TryParse(input[0], out coordinates[0]) && int.TryParse(input[1], out coordinates[1]) &&
                    coordinates[0] > 0 && coordinates[1] > 0 && coordinates[0] < labyrinth.Length-1 &&
                    coordinates[1] < labyrinth[0].Length-1 && labyrinth[coordinates[0]][coordinates[1]] == 0)
                    return coordinates;
                Console.WriteLine("Incorrect input");
            }
        }
    }
}