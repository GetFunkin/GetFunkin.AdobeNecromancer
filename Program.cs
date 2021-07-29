using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

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

            ReadAsXml(args[0], textures, out string imageName, out bool xmlFormat);

            if (!xmlFormat)
            {
                try
                {
                    ReadAsSpriteSheetPacker(args[0], textures, out imageName);
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

            using Image png = FromFile(directory, imageName);
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
                    GetCroppedBitmap(png, texture).Save(Path.Combine(outputDir.FullName, $"{texture.Name}.png"));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception thrown while saving \"{texture.Name}\": {e}");
                }
            }
        }

        private static Image FromFile(string directory, string imageName) => Image.FromFile(Path.Combine(directory ?? "", imageName ?? ""));

        private static void AssertArguments(ref string[] args)
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

        public static void ReadAsXml(string file, List<SubTexture> textures, out string imageName, out bool success)
        {
            using Stream stream = File.Open(file, FileMode.Open);
            using XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings {CheckCharacters = false});
            ReadAsXml(reader, textures, out imageName, out success);
        }

        public static void ReadAsXml(XmlReader reader, List<SubTexture> textures, out string imageName,
            out bool success)
        {
            imageName = "";

            try
            {
                bool startingElement = true;

                while (reader.Read())
                {
                    if (reader.NodeType is XmlNodeType.XmlDeclaration or XmlNodeType.Comment or XmlNodeType.Whitespace
                        or XmlNodeType.EndElement)
                        continue;

                    if (reader.NodeType == XmlNodeType.EndElement)
                        break;

                    if (reader.NodeType == XmlNodeType.Element && startingElement)
                    {
                        startingElement = false;
                        imageName = reader.GetAttribute("imagePath");
                        Console.WriteLine($"TextureAtlas image path: {imageName}");
                        continue;
                    }

                    Console.WriteLine($"Reading element with data: {reader.NodeType}, " +
                                      $"Attribute count: {reader.AttributeCount}");

                    string name = reader.GetAttribute("name");
                    int x = int.Parse(reader.GetAttribute("x")!); // expected value
                    int y = int.Parse(reader.GetAttribute("y")!); // expected value
                    int width = int.Parse(reader.GetAttribute("width")!); // expected value
                    int height = int.Parse(reader.GetAttribute("height")!); // expected value
                    int frameX = int.Parse(reader.GetAttribute("frameX") ?? "0"); // optional value
                    int frameY = int.Parse(reader.GetAttribute("frameY") ?? "0"); // optional value
                    int frameWidth = int.Parse(reader.GetAttribute("frameWidth") ?? "0"); // optional value
                    int frameHeight = int.Parse(reader.GetAttribute("frameHeight") ?? "0"); // optional value
                    textures.Add(new SubTexture(name, x, y, width, height, frameX, frameY, frameWidth, frameHeight));
                }

                success = true;
            }
            catch
            {
                Console.WriteLine("Failed to parse file as XML, assuming SpriteSheetPacker format.");
                textures.Clear();
                success = false;
            }
        }

        public static void ReadAsSpriteSheetPacker(string file, List<SubTexture> textures, out string imageName)
        {
            string[] lines = File.ReadAllLines(file);
            imageName = Path.ChangeExtension(file, ".png");

            textures.AddRange(from line in lines
                select line.Split('=')
                into lineData
                let name = lineData[0]
                let textureData = lineData[1].TrimStart().Split(' ').Select(int.Parse).ToArray()
                select new SubTexture(name, textureData[0], textureData[1], textureData[2], textureData[3], 0,
                    0, 0, 0));
        }

        public static Bitmap GetCroppedBitmap(Image image, SubTexture texture)
        {
            texture.GetData(out bool _, out Rectangle frame, out Rectangle _, out Rectangle crop);
            using Bitmap bitmap = new(image);

            Console.WriteLine($"Cropping frame: {texture.Name}, cropX: {crop.X}, cropY: {crop.Y}, " +
                              $"width: {crop.Width}, height: {crop.Height}, " +
                              $"originalX: {frame.X}, originalY: {frame.Y}");

            try
            {
                return bitmap.Clone(crop, bitmap.PixelFormat);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception thrown while cropping \"{texture.Name}\": {e}");
            }

            return bitmap;
        }
    }
}