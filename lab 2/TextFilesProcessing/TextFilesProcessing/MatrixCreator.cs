using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TextFilesProcessing
{
    public class MatrixCreator
    {
        public static KeyValuePair<string[], int[,]> GetCountriesMatrix(string path)
        {
            
            string[] files = Directory.GetFiles(path, "*.csv");
            if (files.Length == 0)
            {
                throw new NoFilesException("No .csv files found");
            }
            Regex regex = new Regex(@"^[\w ]+,(?:\d+,)+\d+$");
            StreamReader sr;
            string nextLine = "";
            foreach (string file in files)
            {
                sr = new StreamReader(file);
                sr.ReadLine();
                nextLine = sr.ReadLine();
                sr.Close();
                if (nextLine is not null && regex.IsMatch(nextLine))
                {
                    break;
                }

                nextLine = "";
            }

            if (nextLine == "") throw new AllInvalidFilesException("All found files are invalid to build a matrix.");
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
                    string line = sr.ReadLine();
                    if (line is null || !regex.IsMatch(line)) continue;
                    row = line.Split(",");
                    countries[i] = row[0];
                    for (int j = 1; j < row.Length; j++)
                    {
                        matrix[i, j - 1] = Int32.Parse(row[j]);
                    }

                    i++;
                }
                
                sr.Close();
            }

            return new(countries, matrix);
        }
    }
}