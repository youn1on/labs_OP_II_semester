using System.IO;

namespace ImageResizer
{
    public class FileInput
    {
        public static byte[] GetContent(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}