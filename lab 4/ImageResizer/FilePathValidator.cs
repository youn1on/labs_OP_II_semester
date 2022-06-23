using System;
using System.Globalization;
using System.IO;

namespace ImageResizer
{
    public static class FilePathValidator
    {
        public static bool Validate(string path)
        {
            return (File.Exists(path) && !String.IsNullOrEmpty(path));
        }

        public static bool FileDirectoryExists(string path)
        {
            string dir = Path.GetDirectoryName(path);
            return Directory.Exists(dir);
        }
    }
}