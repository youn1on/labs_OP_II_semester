using System;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3 || !FilePathValidator.Validate(args[0]) ||
                !FilePathValidator.FileDirectoryExists(args[1]) ||
                ! double.TryParse(args[2], out double multiplier)) return;
            Console.WriteLine($"Enlarging image {multiplier} times... Done.");
            byte[] content = FileInput.GetContent(args[0]);
            if (FileValidator.ValidateFile(content))
            {
                Picture picture = new Picture(content);
                picture.Resize(multiplier);
                FileOutput.ImageToFile(picture, args[1]);
            }
            else Console.WriteLine("Incorrect file structure");
        }
    }
}