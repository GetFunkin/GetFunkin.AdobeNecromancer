using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using GetFunkin.AdobeNecromancer.Extensions;
using GetFunkin.AdobeNecromancer.Readers.Impl;
using GetFunkin.AdobeNecromancer.SubTextures;

namespace GetFunkin.AdobeNecromancer
{
    // TODO: implement rotated, flipX, and flipY
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            string directory = Path.GetDirectoryName(args[1]);
            string imageName = directory;
            
            DirectoryInfo outputDir = Directory.CreateDirectory(Path.Combine(directory ?? "", "Output"));

            List<ISubTexture> textures = args[0] switch
            {
                "sparrow" => new SparrowAtlasReader().Read(directory, ref imageName),
                "ssp" => new SpriteSheetPackerAtlasReader().Read(directory, ref imageName),
                _ => null
            };

            if (textures is null)
                throw new Exception("Could not produce valid texture results.");

            using Image png = Image.FromFile(Path.Combine(directory ?? "", imageName ?? ""));

            /* partial implementation of FlxAtlasFrames in c#
             * https://github.com/HaxeFlixel/flixel/blob/dev/flixel/graphics/frames/FlxAtlasFrames.hx#L252
             */
            foreach (ISubTexture texture in textures)
            {
                try
                {
                    texture.GetCroppedBitmap(png).Save(Path.Combine(outputDir.FullName, $"{texture.Name}.png"));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception thrown while saving \"{texture.Name}\": {e}");
                }
            }
        }
    }
}