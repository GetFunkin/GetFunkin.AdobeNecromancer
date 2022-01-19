using System.Drawing;
using GetFunkin.AdobeNecromancer.SubTextures;
using GetFunkin.AdobeNecromancer.SubTextures.Impl;

namespace GetFunkin.AdobeNecromancer.Extensions
{
    public static class ScissoredTextureExtensions
    {
        public static Rectangle ToRectangle(this IScissoredTexture scissoredTexture)
        {
            if (scissoredTexture is RectangleBackedScissoredTexture rect)
                return rect.Rectangle;

            return new Rectangle(
                scissoredTexture.X,
                scissoredTexture.Y,
                scissoredTexture.Width,
                scissoredTexture.Height
            );
        }
    }
}