using System;
using System.Drawing;
using GetFunkin.AdobeNecromancer.SubTextures;

namespace GetFunkin.AdobeNecromancer.Extensions
{
    public static class SubTextureExtensions
    {
        // TODO: Implement frames. :(
        public static Bitmap GetCroppedBitmap(this ISubTexture texture, Image image)
        {
            texture.GetData(
                out bool _,
                out IScissoredTexture _,
                out IScissoredTexture _,
                out IScissoredTexture crop
            );
            
            using Bitmap bitmap = new(image);

            try
            {
                return bitmap.Clone(crop.ToRectangle(), bitmap.PixelFormat);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception thrown while cropping \"{texture.Name}\": {e}");
            }

            return bitmap;
        }
    }
}