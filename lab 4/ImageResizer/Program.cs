using System;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var VARIABLE in args)
            {
                Console.WriteLine(VARIABLE);
            }
            if (args.Length != 3 || !FilePathValidator.Validate(args[0]) ||
                !FilePathValidator.FileDirectoryExists(args[1]) ||
                ! double.TryParse(args[2], out double multiplier)) return;
            Console.WriteLine("Resizing...");
            byte[] content = FileInput.GetContent(args[0]);
            Picture pic = new Picture(content);
            pic.Resize(multiplier);
            FileOutput.ImageToFile(pic, args[1]);
        }
    }
}