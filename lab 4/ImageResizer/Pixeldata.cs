namespace ImageResizer
{
    public struct Pixeldata
    {
        byte redComponent;
        byte greenComponent;
        byte blueComponent;

        public Pixeldata(byte[] data)
        {
            redComponent = data[0];
            greenComponent = data[1];
            blueComponent = data[2];
        }

        public byte[] ToByteArray()
        {
            return new[] {redComponent, greenComponent, blueComponent};
        }
        public Pixeldata(int r, int g, int b)
        {
            redComponent = r < 256 ? (byte)r : (byte)255;
            greenComponent = g < 256 ? (byte)g : (byte)255;
            blueComponent = b < 256 ? (byte)b : (byte)255;
        }

        public static Pixeldata operator *(Pixeldata pixel, double coef)
        {
            return new Pixeldata((int)(pixel.redComponent*coef), (int)(pixel.greenComponent*coef), (int)(pixel.blueComponent*coef));
        }

        public static Pixeldata operator +(Pixeldata pix1, Pixeldata pix2)
        {
            return new Pixeldata(pix1.redComponent + pix2.redComponent, pix1.greenComponent + pix2.greenComponent,
                pix1.blueComponent + pix2.blueComponent);
        }
    }
}