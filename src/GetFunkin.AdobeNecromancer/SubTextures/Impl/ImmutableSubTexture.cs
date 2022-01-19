namespace GetFunkin.AdobeNecromancer.SubTextures.Impl
{
    /// <summary>
    ///     A read-only <see cref="ISubTexture"/> implementation.
    /// </summary>
    public class ImmutableSubTexture : ISubTexture
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

        public ImmutableSubTexture(
            string name,
            int x,
            int y,
            int width,
            int height,
            int frameX = 0,
            int frameY = 0,
            int frameWidth = 0,
            int frameHeight = 0
        )
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

        public IScissoredTexture Crop
        {
            get
            {
                RectangleBackedScissoredTexture scissor = new(X, Y, Width, Height);

                if (scissor.X < 0)
                    scissor.X = 0;

                if (scissor.Y < 0)
                    scissor.Y = 0;

                return scissor;
            }
        }

        public void GetData(out bool trimmed, out IScissoredTexture frame, out IScissoredTexture size)
        {
            trimmed = FrameX != 0;
            frame = new RectangleBackedScissoredTexture(X, Y, Width, Height);

            size = trimmed
                ? new RectangleBackedScissoredTexture(FrameX, FrameY, FrameWidth, FrameHeight)
                : new RectangleBackedScissoredTexture(0, 0, Width, Height);
        }

        public void GetData(
            out bool trimmed,
            out IScissoredTexture frame,
            out IScissoredTexture size,
            out IScissoredTexture crop
        )
        {
            GetData(out trimmed, out frame, out size);
            crop = Crop;
        }

        public static ImmutableSubTexture BuildTexture(
            string name,
            int x,
            int y,
            int width,
            int height,
            int frameX = 0,
            int frameY = 0,
            int frameWidth = 0,
            int frameHeight = 0
        ) => new(name, x, y, width, height, frameX, frameY, frameWidth, frameHeight);
    }
}