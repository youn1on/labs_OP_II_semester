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
    }
}