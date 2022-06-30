using System.IO;
using System.Text.RegularExpressions;

namespace Labyrinths.Model
{
    public class FileOperations
    {
        public static string[]? GetFileContent(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            bool isValidated = ValidateFile(ref lines);
            if (isValidated) return lines;
            return null;
        }

        public static bool ValidateFile(ref string[] content)
        {
            int length = content[0].Trim().Length;
            Regex regex = new Regex(@"^[X ]+$");
            for (int i = 0; i < content.Length; i++)
            {
                content[i] = content[i].Trim();
                if (!regex.IsMatch(content[i]) || content[i].Length != length)
                {
                    return false;
                }
            }

            regex = new Regex(@"^(?:X )+X$");
            if (regex.IsMatch(content[0]) && regex.IsMatch(content[^1])) return true;
            return false;
        }
    }
}