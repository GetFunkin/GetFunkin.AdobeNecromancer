﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

namespace GetFunkin.AdobeNecromancer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args is not {Length: 1} && !Debugger.IsAttached)
            {
                Console.WriteLine("Expected file.");
                return;
            }

            Console.WriteLine("AdobeNecromancer: Re-animating the dead!");

            if (Debugger.IsAttached && args is not { Length: 1 })
            {
                Console.WriteLine("Debugger attached, using pre-defined path to XML file.");
                args = new[] {Path.Combine("DebugFiles", "logoBumpin.xml")};
            }

            List<SubTexture> textures = new();
            string directory = Path.GetDirectoryName(args[0]);
            string imageName = "";

            Console.WriteLine("Reading file...");

            bool xml = true;

            using (Stream stream = File.Open(args[0], FileMode.Open))
            {
                try
                {
                    XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings {CheckCharacters = false});
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
                }
                catch
                {
                    Console.WriteLine("Failed to parse file as XML, assuming SpriteSheetPacker format.");
                    xml = false;
                    textures.Clear();
                }
            }

            if (!xml)
            {
                try
                {
                    string[] lines = File.ReadAllLines(args[0]);
                    imageName = Path.ChangeExtension(args[0], ".png");

                    textures.AddRange(from line in lines
                        select line.Split('=')
                        into lineData
                        let name = lineData[0]
                        let textureData = lineData[1].TrimStart().Split(' ').Select(int.Parse).ToArray()
                        select new SubTexture(name, textureData[0], textureData[1], textureData[2], textureData[3], 0,
                            0, 0, 0));
                }
                catch
                {
                    Console.WriteLine("Failed to parse as both XML (Sparrow Atlas) or SpriteSheetPacker formats. Exiting.");
                    throw;
                }
            }
            else 
                Console.WriteLine("Finished reading XML file.");

            Console.WriteLine("Found texture data:");

            foreach (SubTexture texture in textures)
                Console.WriteLine($"Texture: {texture.Name}, x: {texture.X}, y: {texture.Y}, " +
                                  $"width: {texture.Width}, height: {texture.Height}, " +
                                  $"frameX: {texture.FrameX}, frameY: {texture.FrameY}, " +
                                  $"frameWidth: {texture.FrameWidth}, frameHeight: {texture.FrameHeight}");

            Console.WriteLine($"Opening associated image file ({imageName})...");

            using Image png = Image.FromFile(Path.Combine(directory ?? "", imageName ?? ""));
            Console.WriteLine("Generating output directory...");
            DirectoryInfo outputDir = Directory.CreateDirectory(Path.Combine(directory ?? "", "Output"));
            Console.WriteLine($"Output directory generated at: {outputDir.FullName}");

            Console.WriteLine("Cutting up textures based on texture data...");

            // partial implementation of
            // https://github.com/HaxeFlixel/flixel/blob/dev/flixel/graphics/frames/FlxAtlasFrames.hx#L252
            // in c#
            foreach (SubTexture texture in textures)
            {
                // TODO: implement rotated, flipX, and flipY
                bool trimmed = texture.FrameX != 0;
                Rectangle frame = new(texture.X, texture.Y, texture.Width, texture.Height);
                Rectangle size = trimmed
                    ? new Rectangle(texture.FrameX, texture.FrameY, texture.FrameWidth, texture.FrameHeight)
                    : new Rectangle(0, 0, frame.Width, frame.Height);
                Point offset = new(-size.Left, -size.Top);

                using Bitmap bitmap = new(png);
                Rectangle crop = new(frame.X, frame.Y, frame.Width, frame.Height);

                if (crop.X < 0)
                    crop.X = 0;

                if (crop.Y < 0)
                    crop.Y = 0;

                Console.WriteLine($"Cropping frame: {texture.Name}, cropX: {crop.X}, cropY: {crop.Y}, " +
                                  $"width: {crop.Width}, height: {crop.Height}, " +
                                  $"originalX: {frame.X}, originalY: {frame.Y}");

                try
                {
                    bitmap.Clone(crop, bitmap.PixelFormat).Save(Path.Combine(outputDir.FullName, $"{texture.Name}.png"));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception thrown while cropping and saving \"{texture.Name}\": {e}");
                }
            }
        }
    }
}