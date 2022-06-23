using System;
using System.Linq;

namespace ImageResizer
{
    public struct Picture
    {
        public Int16 Id { get; }            // Завжди дві літери 'B' і 'M'
        public Int32 Filesize { get; private set; }        // Розмір файла в байтах
        public Int32 Reserved { get; }        // 0, 0
        public Int32 Headersize { get; }      // 54L для 24-бітних зображень
        public Int32 InfoSize { get; }        // 40L для 24-бітних зображень
        public Int32 Width { get; private set; }           // ширина зображення в пікселях
        public Int32 Depth { get; private set; }           // висота зображення в пікселях
        public Int16 BiPlanes { get; }        // 1 (для 24-бітних зображень)
        public Int16 Bits { get; }            // 24 (для 24-бітних зображень)
        public Int32 BiCompression { get; }   // 0L
        public Int32 BiSizeImage { get; }     // Можна поставити в 0L для зображень без компрессії (наш варіант)
        public Int32 BiXPelsPerMeter { get; } // Рекомендована кількість пікселів на метр, можна 0L
        public Int32 BiYPelsPerMeter { get; } // Те саме, по висоті
        public Int32 BiClrUsed { get; }       // Для індексованих зображень, можна поставити 0L
        public Int32 BiClrImportant { get; }  // Те саме
        public Pixeldata[][] Bitmap { get; private set; }

        private int _gapSize;
        
        public Picture(byte[] data)
        {
            Id = BitConverter.ToInt16(data[..2]);
            Filesize = BitConverter.ToInt32(data[2..6]);
            Reserved = BitConverter.ToInt32(data[6..10]);
            Headersize = BitConverter.ToInt32(data[10..14]);
            InfoSize = BitConverter.ToInt32(data[14..18]);
            Width = BitConverter.ToInt32(data[18..22]);
            Depth = BitConverter.ToInt32(data[22..26]);
            BiPlanes = BitConverter.ToInt16(data[26..28]);
            Bits = BitConverter.ToInt16(data[28..30]);
            BiCompression = BitConverter.ToInt32(data[30..34]);
            BiSizeImage = BitConverter.ToInt32(data[34..38]);
            BiXPelsPerMeter = BitConverter.ToInt32(data[38..42]);
            BiYPelsPerMeter = BitConverter.ToInt32(data[42..46]);
            BiClrUsed = BitConverter.ToInt32(data[46..50]);
            BiClrImportant = BitConverter.ToInt32(data[50..54]);

            _gapSize = 4 - (Width * 3 % 4);
            if (_gapSize == 4) _gapSize = 0;

            int iterator = 54;

            Bitmap = new Pixeldata[Depth][];
            for (int i = 0; i < Depth; i++)
            {
                Bitmap[i] = new Pixeldata[Width];
                for (int j = 0; j < Width; j++)
                {
                    Bitmap[i][j] = new Pixeldata(data[iterator..(iterator + 3)]);
                    iterator += 3;
                }

                iterator += _gapSize;
            }
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[Filesize];
            int iterator = 0;

            Int32[] attributes = new Int32[]
            {
                Id, Filesize, Reserved, Headersize, InfoSize, Width, Depth, BiPlanes, Bits, BiCompression,
                BiSizeImage, BiXPelsPerMeter, BiYPelsPerMeter, BiClrUsed, BiClrImportant
            };

            bool[] IsInt16 = new[]
                {true, false, false, false, false, false, false, true, true, false, false, false, false, false, false};

            foreach ((int attribute, bool isInt16) in attributes.Zip(IsInt16))
            {
                byte[] bytes = BitConverter.GetBytes(attribute);
                if (isInt16) bytes = bytes[..2];
                foreach (byte _byte in bytes)
                {
                    result[iterator] = _byte;
                    iterator++;
                }
            }
            
            for (int i = 0; i < Depth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    foreach (byte _byte in Bitmap[i][j].ToByteArray())
                    {
                        result[iterator] = _byte;
                        iterator++;
                    }
                }
                iterator += _gapSize;
            }

            return result;
        }

        public void Resize(double multiplier)
        {
            if (multiplier - (int) multiplier == 0)
            {
                Depth *= (int) multiplier;
                Width *= (int) multiplier;
                _gapSize = 4 - (Width * 3 % 4);
                Filesize = Depth * (Width * 3 + _gapSize) + 54;

                Pixeldata[][] newBitmap = new Pixeldata[Depth][];
                for (int i = 0; i < Depth; i++)
                {
                    newBitmap[i] = new Pixeldata[Width];
                    for (int j = 0; j < Width; j++)
                    {
                        newBitmap[i][j] = Bitmap[i / (int) multiplier][j / (int) multiplier];
                    }
                }

                Bitmap = newBitmap;
            }
        }
    }
}