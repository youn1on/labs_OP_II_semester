using System;
using System.IO;

namespace TextFilesProcessing
{
    public class FilepathOperations
    {
        public static string GetPath()
        {
            while (true)
            {
                Console.WriteLine("Enter your path:");
                string path = Console.ReadLine();
                string defaultPath = "C:\\Users\\Uni\\Desktop\\labs 2 semestr\\OP\\lab 2";
                if (path == "" && Directory.Exists(defaultPath))
                {
                    return defaultPath;
                }
                if (Directory.Exists(path)) return path;
                Console.WriteLine("Incorrect path: " + path + " not found");
            }
        }
            
    }
}