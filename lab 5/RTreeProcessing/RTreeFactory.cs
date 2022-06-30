using System.IO;

namespace RTreeProcessing
{
    public class RTreeFactory
    {
        public static RTree FillFromFile(string filename)
        {
            if (!File.Exists(filename)) return null;
            RTree tree = new RTree();
            StreamReader sr = new StreamReader(filename);
            string source;
            for (int i = 0; !sr.EndOfStream; i++)
            {
                source = sr.ReadLine();
                GeographicalPoint? point = GeographicalPointFactory.ParsePoint(source);
                if (point is null)
                {
                    ErrorOutput.PrintIncorrectLine(i, source);
                }
                else tree.Add(point.Value);
            }
            sr.Close();
            return tree;
        }
    }
}