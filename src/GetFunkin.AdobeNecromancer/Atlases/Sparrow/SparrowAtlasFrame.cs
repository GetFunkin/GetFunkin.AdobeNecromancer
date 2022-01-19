using System.Drawing;

namespace GetFunkin.AdobeNecromancer.Atlases.Sparrow
{
    public class SparrowAtlasFrame : ISparrowAtlasFrame
    {
        public int X { get; init; }

        public int Y { get; init; }

        public int Width { get; init; }

        public int Height { get; init; }

        public string Name { get; init; }

        public bool Trimmed { get; init; }

        public bool Rotated { get; init; }

        public bool FlipX { get; init; }

        public bool FlipY { get; init; }

        public int FrameX { get; init; }

        public int FrameY { get; init; }

        public int FrameWidth { get; init; }

        public int FrameHeight { get; init; }

        public Rectangle GetRect() => new(X, Y, Width, Height);

        public Rectangle GetSize() => Trimmed
            ? new Rectangle(FrameX, FrameY, FrameWidth, FrameHeight)
            : new Rectangle(0, 0, Width, Height);

        public float GetAngle() => Rotated ? -90f : 0f;

        public Point GetOffset()
        {
            Rectangle size = GetSize();

            return new Point(-size.Left, -size.Top);
        }

        public Point SourceSize()
        {
            Rectangle size = GetSize();

            return new Point(size.Width, size.Height);
        }
    }
}