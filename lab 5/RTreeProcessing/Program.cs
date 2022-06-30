using System;

namespace RTreeProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                ErrorOutput.PrintIncorrectArgumentsNumber(args.Length);
                return;
            }
            if (!double.TryParse(args[1], out double targetLatitude) ||
                     !double.TryParse(args[2], out double targetLongitude) || 
                     !int.TryParse(args[3], out int radius))
            {
                ErrorOutput.PrintIncorrectArgument(args[1], args[2], args[3]);
                return;
            }

            RTree tree = RTreeFactory.FillFromFile(args[0]);
            if (tree is null)
            {
                ErrorOutput.FileDoesntExist(args[0]);
                return;
            }
            Console.WriteLine("Tree built successfully! Searching for target points...");

            GeographicalPoint[] nearest = tree.FindNearest(targetLatitude, targetLongitude, radius);
            
            ResultOutputter.PrintResult(nearest, targetLatitude, targetLongitude, radius);
        }        
    }
}