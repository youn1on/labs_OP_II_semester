using System;
using System.IO;

namespace TextFilesProcessing.Controller
{
    public class FilepathOperations
    {
        public static string GetPath()
        {
            while (true)
            {
                Console.WriteLine("Enter your path or 'exit':");
                string path = Console.ReadLine();
                if (path == "exit") Environment.Exit(0);
                string validatePath = ValidatePath(path);
                if(validatePath != null) return validatePath;
                Console.WriteLine("Incorrect path: " + path + " not found");
            }
        }

        public static string ValidatePath(string path)
        {
            string defaultPath = "C:/Users/Uni/Desktop/labs 2 semestr/OP/lab 2";
            if (path == "" && Directory.Exists(defaultPath))
            {
                return defaultPath;
            }
            if (Directory.Exists(path)) return path;
            return null;
        }
    }
}