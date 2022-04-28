using System;
using System.Collections.Generic;
using System.IO;

namespace TextFilesProcessing
{
    public class MatrixCreator
    {
        public static KeyValuePair<string[], int[,]> CreateMatrix(string path = "C:\\Users\\Uni\\Desktop\\labs 2 semestr\\OP\\lab 2")
        {
            
            string[] files = Directory.GetFiles(path, "*.csv");
            if (files.Length == 0)
            {
                return new (null,null);
            }
            StreamReader sr = new StreamReader(files[0]);
            sr.ReadLine();
            string nextLine = sr.ReadLine();
            sr.Close();
            if (nextLine == null)
            {
                return new (null,null);
            }
            
            int participantCountriesNumber = nextLine.Split(",").Length-1;
            int i = 0;
            string[] countries = new string[participantCountriesNumber];
            int[,] matrix = new int[participantCountriesNumber, participantCountriesNumber];
            string[] row;
            foreach (string file in files)
            {
                sr = new StreamReader(file);
                sr.ReadLine();
                while (!sr.EndOfStream && i < participantCountriesNumber)
                {
                    row = sr.ReadLine()?.Split(",");
                    if (row is null) continue;
                    countries[i] = row[0];
                    for (int j = 1; j < row.Length; j++)
                    {
                        if (!Int32.TryParse(row[j], out matrix[i,j-1]))
                        {
                            return new (null,null);
                        }
                    }

                    i++;
                }
                
                sr.Close();
            }

            return new(countries, matrix);
        }
    }
    
    /* List<StreamReader> readers = new List<StreamReader>();
             int countryCounter = 0;
             foreach (string file in files)
             {
                 StreamReader sr = new StreamReader(file);
                 if (Int32.TryParse(sr.ReadLine(), out int firstLine))
                 {
                     countryCounter += firstLine;
                     readers.Add(sr);
                 }
                 else
                 {
                     sr.Close();
                 }
             } */
}