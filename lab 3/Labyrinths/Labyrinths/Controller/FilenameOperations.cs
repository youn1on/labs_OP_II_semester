using System;
using System.IO;

namespace Labyrinths.Controller
{
    public class FilenameOperations
    {
        public static string GetPath()
        {
            while (true)
            {
                Console.WriteLine("Enter your filepath or 'exit':");
                string? path = Console.ReadLine();
                if (path == "exit") Environment.Exit(0);
                string? validatePath = ValidatePath(path);
                if (validatePath != null) return validatePath;
                Console.WriteLine("Incorrect path: " + path + " not found");
            }
        }

        public static string? ValidatePath(string? path)
        {
            string defaultPath = @"C:\Users\Uni\Desktop\labs 2 semestr\OP\lab 3\Labrinths\Inputs\Labyrinth_huge.txt";
            if (path == "" && File.Exists(defaultPath))
            {
                return defaultPath;
            }

            if (File.Exists(path)) return path;
            return null;
        }
    }
}