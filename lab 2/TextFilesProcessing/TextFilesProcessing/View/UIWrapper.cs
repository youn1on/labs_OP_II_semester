using System;

namespace TextFilesProcessing.View
{
    public class UIWrapper
    {
        public static void PrintOutput(int[][] matrix, int[][] ratingMatrix, int[] arrayOfCountriesPoints, string[] winners, string filepath)
        {
            Console.WriteLine("Initial matrix:");
            ConsoleWriter.PrintMatrix(matrix);
            Console.WriteLine("Rating matrix:");
            ConsoleWriter.PrintMatrix(ratingMatrix);
            Console.WriteLine("Countries points:");
            ConsoleWriter.PrintArray(arrayOfCountriesPoints);
            Console.WriteLine("Winners:");
            ConsoleWriter.PrintArray(winners);
            ResultWriter.WriteToFile(winners, filepath);
        }
    }
}