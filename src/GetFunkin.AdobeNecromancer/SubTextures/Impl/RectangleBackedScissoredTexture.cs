using System.Drawing;

namespace GetFunkin.AdobeNecromancer.SubTextures.Impl
{
    /// <summary>
    ///     A <see cref="Rectangle"/>-backed <see cref="IScissoredTexture"/>.
    /// </summary>
    public class RectangleBackedScissoredTexture : IScissoredTexture
    {
        public int X
        {
            get => Rectangle.X;
            
            set
            {
                Rectangle rect = Rectangle;
                rect.X = value;
                Rectangle = rect;
            }
        }

        public int Y
        {
            get => Rectangle.Y;
            
            set
            {
                Rectangle rect = Rectangle;
                rect.Y = value;
                Rectangle = rect;
            }
        }

        public int Width
        {
            get => Rectangle.Width;
            
            set
            {
                Rectangle rect = Rectangle;
                rect.Width = value;
                Rectangle = rect;
            }
        }

        public int Height
        {
            get => Rectangle.Height;

            set
            {
                Rectangle rect = Rectangle;
                rect.Height = value;
                Rectangle = rect;
            }
        }

        public Rectangle Rectangle { get; private set; }

        public RectangleBackedScissoredTexture(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }

        public RectangleBackedScissoredTexture(int x, int y, int width, int height) : this(
            new Rectangle(x, y, width, height)
        )
        {
        }
    }
}