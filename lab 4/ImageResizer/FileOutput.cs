using System.IO;

namespace ImageResizer
{
    public class FileOutput
    {
        public static void ImageToFile(Picture picture, string filePath)
        {
            var file = File.Create(filePath);
            foreach (byte _byte in picture.ToByteArray())
            {
                file.WriteByte(_byte);
            }
            file.Close();
        }
    }
}