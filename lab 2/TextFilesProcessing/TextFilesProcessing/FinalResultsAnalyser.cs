using System;
using System.Collections.Generic;

namespace TextFilesProcessing
{
    public class FinalResultsAnalyser
    {
        public static string[] GetWinners(string[] countries, int[] arrayOfCountriesPoints)
        {
            KeyValuePair<string, int>[] countryPoints = new KeyValuePair<string, int>[countries.Length];
            for (int i = 0; i < countries.Length; i++)
            {
                countryPoints[i] = new(countries[i], arrayOfCountriesPoints[i]);
            }
            Array.Sort(countryPoints, (pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            int ratingSize = countries.Length > 10 ? 10 : countries.Length;
            string[] winners = new string[ratingSize];
            for (int i = 0; i < ratingSize; i++)
            {
                winners[i] = countryPoints[i].Key;
            }

            return winners;
        }
    }
}