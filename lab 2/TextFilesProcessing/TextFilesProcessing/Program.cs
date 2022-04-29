using System;
using System.IO;

namespace TextFilesProcessing
{
    class Program
    {
        static void Main(string[] args)
        {   
            string filepath = FilepathOperations.GetPath();
            string[] countries;
            int[,] matrix;
            try
            {
                (countries, matrix) = MatrixCreator.GetCountriesMatrix(filepath);
            }
            catch (FileNotFoundException noFile)
            {
                Console.WriteLine(noFile.Message);
                return;
            }
            catch (AllInvalidFilesException noValidFiles)
            {
                Console.WriteLine(noValidFiles.Message);
                return;
            }
            Console.WriteLine("Initial matrix:");
            ConsoleWriter.PrintMatrix(matrix);
            int[,] ratingMatrix = MatrixAnalyser.GetRatingMatrix(matrix);
            Console.WriteLine("Rating matrix:");
            ConsoleWriter.PrintMatrix(ratingMatrix);
            int[] arrayOfCountriesPoints = MatrixAnalyser.GetArrayOfCountriesPoints(ratingMatrix);
            Console.WriteLine("Countries points:");
            ConsoleWriter.PrintArray(arrayOfCountriesPoints);
            string[] winners = FinalResultsAnalyser.GetWinners(countries, arrayOfCountriesPoints);
            Console.WriteLine("Winners:");
            ConsoleWriter.PrintArray(winners);
            ResultWriter.WriteToFile(winners, filepath);
        }

    }
}