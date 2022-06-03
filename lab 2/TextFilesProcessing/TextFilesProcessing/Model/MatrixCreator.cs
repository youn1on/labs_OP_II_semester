using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TextFilesProcessing.Model
{
    public class MatrixCreator
    {
        public static int[][] GetCountriesMatrix(string path, out string[] countries)
        {

            string[] files = Directory.GetFiles(path, "*.csv");
            Regex regex = new Regex(@"^[\w ]+,(?:\d+,)+\d+$");
            int participantCountriesNumber = GetParticipantNumber(regex, files);
            int i = 0;
            countries = new string[participantCountriesNumber];
            int[][] matrix = new int[participantCountriesNumber][];
            for (int k = 0; k < participantCountriesNumber; k++)
            {
                matrix[k] = new int[participantCountriesNumber];
            }
            
            StreamReader sr;
            string[] row;
            foreach (string file in files)
            {
                sr = new StreamReader(file);
                sr.ReadLine();
                while (!sr.EndOfStream && i < participantCountriesNumber)
                {
                    string line = sr.ReadLine();
                    if (line is null || !regex.IsMatch(line)) throw new InvalidFileException("Invalid .csv file found");
                    row = line.Split(",");
                    countries[i] = row[0];
                    for (int j = 1; j < row.Length; j++)
                    {
                        matrix[i][j - 1] = Int32.Parse(row[j]);
                    }
                    i++;
                }
                sr.Close();
            }
            return matrix;
        }

        private static int GetParticipantNumber(Regex regex, string[] files)
        {
            if (files.Length == 0)
            {
                throw new NoFilesException("No .csv files found");
            }

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
            return nextLine.Split(",").Length - 1;
        }
    }
}