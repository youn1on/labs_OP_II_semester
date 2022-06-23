using System;

namespace ImageResizer
{
    public class FileValidator
    {
        public static bool ValidateFile(byte[] data)
        {
            if (data.Length <= 54 || BitConverter.ToInt32(data[2..6]) != data.Length ||
                BitConverter.ToInt16(data, 0) != 19778 || BitConverter.ToInt32(data, 10) != 54 ||
                BitConverter.ToInt32(data, 14) != 40 || BitConverter.ToInt16(data, 26) != 1 ||
                BitConverter.ToInt16(data, 28) != 24) return false;

            int width = BitConverter.ToInt32(data, 18);
            int depth = BitConverter.ToInt32(data, 22);
            int gap = 4 - (width * 3 % 4);
            if (gap == 4) gap = 0;
            return (depth * (width * 3 + gap) + 54) <= data.Length;
        }
    }
}