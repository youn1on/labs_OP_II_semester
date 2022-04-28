using System;

namespace TextFilesProcessing
{
    public class MatrixAnalyser
    {
        public static int[,] GetRatingMatrix(int[,] matrix)
        {
            int numberOfColumns = matrix.GetLength(0) > 10 ? 10 : matrix.GetLength(0);
            int[,] ratingMatrix = new int[matrix.GetLength(1), numberOfColumns];
            
            int[] initialIndexes = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++) initialIndexes[i] = i;
            
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Array.Sort(initialIndexes, (i1, i2) => matrix[i2, j].CompareTo(matrix[i1, j]));
                for (int i = 0; i < ratingMatrix.GetLength(1) && i < 10; i++)
                {
                    ratingMatrix[j, i] = initialIndexes[i];
                }
            }
            return ratingMatrix;
        }

        public static int[] GetArrayOfCountriesPoints(int[,] ratingMatrix)
        {
            int[] arrayOfCountriesPoints = new int[ratingMatrix.GetLength(0)];
            int[] points = {12, 10, 8, 7, 6, 5, 4, 3, 2, 1};
            for (int i = 0; i < ratingMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < ratingMatrix.GetLength(1); j++)
                {
                    arrayOfCountriesPoints[ratingMatrix[i, j]] += points[j];
                }
                
            }
            return arrayOfCountriesPoints;
        }
    }
}