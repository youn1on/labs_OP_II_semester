using System;

namespace TextFilesProcessing
{
    public class ConsoleWriter
    {
        public static void PrintArray(int[] array)
        {
            foreach (int number in array) Console.Write("{0,4:D0}", number);
            Console.WriteLine();
        }
        
        public static void PrintArray(string[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i]+", ");
            }

            Console.Write(array[^1]);
        }
        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0,8:D0}",matrix[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}