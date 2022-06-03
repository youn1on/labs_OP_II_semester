using System;
using System.IO;
using TextFilesProcessing.Controller;
using TextFilesProcessing.View;

namespace TextFilesProcessing.Model
{
    class Program
    {
        static void Main(string[] args)
        {   
            string filepath = FilepathOperations.GetPath();
            string[] countries;
            int[][] matrix;
            try
            {
                matrix = MatrixCreator.GetCountriesMatrix(filepath, out countries);
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
            catch (InvalidFileException invalidFile)
            {
                Console.WriteLine(invalidFile.Message);
                return;
            }
            
            int[][] ratingMatrix = MatrixAnalyser.GetRatingMatrix(matrix);
            int[] arrayOfCountriesPoints = MatrixAnalyser.GetArrayOfCountriesPoints(ratingMatrix);
            string[] winners = FinalResultsAnalyser.GetWinners(countries, arrayOfCountriesPoints);

            UIWrapper.PrintOutput(matrix, ratingMatrix, arrayOfCountriesPoints, winners, filepath);
        }
    }
}