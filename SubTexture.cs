using System.Drawing;

namespace GetFunkin.AdobeNecromancer
{
    public sealed class SubTexture
    {
        public string Name { get; }

        public int X { get; }

        public int Y { get; }

        public int Width { get; }

        public int Height { get; }

        public int FrameX { get; }

        public int FrameY { get; }

        public int FrameWidth { get; }

        public int FrameHeight { get; }

        public SubTexture(string name, int x, int y, int width, int height, int frameX, int frameY, int frameWidth, int frameHeight)
        {
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            FrameX = frameX;
            FrameY = frameY;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
        }

        public void GetData(out bool trimmed, out Rectangle frame, out Rectangle size)
        {
            trimmed = FrameX != 0;
            frame = new Rectangle(X, Y, Width, Height);

            size = trimmed
                ? new Rectangle(FrameX, FrameY, FrameWidth, FrameHeight)
                : new Rectangle(0, 0, Width, Height);
        }

        public void GetData(out bool trimmed, out Rectangle frame, out Rectangle size, out Rectangle crop)
        {
            GetData(out trimmed, out frame, out size);
            crop = GetCrop();
        }

        public Rectangle GetCrop()
        {
            Rectangle crop = new(X, Y, Width, Height);

            if (crop.X < 0)
                crop.X = 0;

            if (crop.Y < 0)
                crop.Y = 0;

            return crop;
        }
    }
}