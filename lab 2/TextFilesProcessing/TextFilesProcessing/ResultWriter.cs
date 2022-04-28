using System.IO;

namespace TextFilesProcessing
{
    public class ResultWriter
    {
        public static void WriteToFile(string[] winners, string path)
        {
            string filename = path + "\\results.csv";
            if (!File.Exists(filename)) using (File.Create(filename)) {}

            StreamWriter writer = new StreamWriter(filename, false);
            foreach (string winner in winners)
            {
                writer.WriteLine(winner);
            }
            
            writer.Close();
        }
    }
}