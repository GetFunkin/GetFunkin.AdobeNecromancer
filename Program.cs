using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace GetFunkin.AdobeNecromancer
{
    // TODO: implement rotated, flipX, and flipY
    public static class Program
    {
        public static void Main(string[] args)
        {
            AssertArguments(ref args);

            List<SubTexture> textures = new();
            string directory = Path.GetDirectoryName(args[0]);

            Console.WriteLine("Reading file...");

            Necromancer.ReadAsXml(args[0], textures, out string imageName, out bool xmlFormat);

            if (!xmlFormat)
            {
                try
                {
                    Necromancer.ReadAsSpriteSheetPacker(args[0], textures, out imageName);
                }
                catch
                {
                    Console.WriteLine("Failed to parse Sparrow Atlas or SpriteSheetPacker formats. Exiting.");
                    throw;
                }
            }
            else
                Console.WriteLine("Finished reading file.");

            Console.WriteLine("Found texture data:");

            foreach (SubTexture texture in textures)
                Console.WriteLine($"Texture: {texture.Name}, x: {texture.X}, y: {texture.Y}, " +
                                  $"width: {texture.Width}, height: {texture.Height}, " +
                                  $"frameX: {texture.FrameX}, frameY: {texture.FrameY}, " +
                                  $"frameWidth: {texture.FrameWidth}, frameHeight: {texture.FrameHeight}");

            Console.WriteLine($"Opening associated image file ({imageName})...");

            using Image png = Necromancer.FromFile(directory, imageName);
            Console.WriteLine("Generating output directory...");
            DirectoryInfo outputDir = Directory.CreateDirectory(Path.Combine(directory ?? "", "Output"));
            Console.WriteLine($"Output directory generated at: {outputDir.FullName}");

            Console.WriteLine("Cutting up textures based on texture data...");

            /* partial implementation of FlxAtlasFrames in c#
             * https://github.com/HaxeFlixel/flixel/blob/dev/flixel/graphics/frames/FlxAtlasFrames.hx#L252
             */
            foreach (SubTexture texture in textures)
            {
                try
                {
                    Necromancer.GetCroppedBitmap(png, texture).Save(Path.Combine(outputDir.FullName, $"{texture.Name}.png"));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception thrown while saving \"{texture.Name}\": {e}");
                }
            }
        }

        public static void AssertArguments(ref string[] args)
        {
            if (args is not {Length: 1} && !Debugger.IsAttached)
            {
                Console.WriteLine("Expected file.");
                return;
            }

            Console.WriteLine("AdobeNecromancer: Re-animating the dead!");

            if (Debugger.IsAttached && args is not {Length: 1})
            {
                Console.WriteLine("Debugger attached, using pre-defined path to XML file.");
                args = new[] {Path.Combine("DebugFiles", "logoBumpin.xml")};
            }
        }
    }
}