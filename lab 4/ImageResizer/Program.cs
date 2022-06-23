using System;
using System.IO;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3 || !FilePathValidator.Validate(args[0]) ||
                !FilePathValidator.FileDirectoryExists(args[1]) ||
                !double.TryParse(args[2].Replace(',', '.'), out double multiplier))
            {
                Console.WriteLine("Invalid input!"); 
                return;
            }
                
            byte[] content = FileInput.GetContent(args[0]);
            bool isValidFile = FileValidator.ValidateFile(content);
            bool isCorrectMultiplier = double.Parse(args[2]) > 0;
            if (!isValidFile) Console.WriteLine("Invalid file structure");
            else if (!isCorrectMultiplier) Console.WriteLine("Multiplier is < 0");
            else 
            {
                Console.Write($"Enlarging image {multiplier} times... ");
                Picture picture = new Picture(content);
                picture.Resize(multiplier);
                FileOutput.ImageToFile(picture, args[1]);
                Console.WriteLine("Done.");
            }
        }
    }
}