using System;

namespace TextFilesProcessing.Model
{
    public class MatrixAnalyser
    {
        public static int[][] GetRatingMatrix(int[][] matrix)
        {
            int numberOfColumns = matrix.Length > 10 ? 10 : matrix.Length;
            int[][] ratingMatrix = new int[matrix[0].Length][];
            for (int i = 0; i < ratingMatrix.Length; i++)
            {
                ratingMatrix[i] = new int[numberOfColumns];
            }
            
            int[] initialIndexes = new int[matrix.Length];
            for (int i = 0; i < matrix.Length; i++) initialIndexes[i] = i;
            
            for (int j = 0; j < matrix[0].Length; j++)
            {
                Array.Sort(initialIndexes, (i1, i2) => matrix[i2][j].CompareTo(matrix[i1][j]));
                for (int i = 0; i < ratingMatrix[0].Length && i < 10; i++)
                {
                    ratingMatrix[j][i] = initialIndexes[i];
                }
            }
            return ratingMatrix;
        }

        public static int[] GetArrayOfCountriesPoints(int[][] ratingMatrix)
        {
            int[] arrayOfCountriesPoints = new int[ratingMatrix.Length];
            int[] points = {12, 10, 8, 7, 6, 5, 4, 3, 2, 1};
            for (int i = 0; i < ratingMatrix.Length; i++)
            {
                for (int j = 0; j < ratingMatrix[0].Length; j++)
                {
                    arrayOfCountriesPoints[ratingMatrix[i][j]] += points[j];
                }
                
            }
            return arrayOfCountriesPoints;
        }
    }
}