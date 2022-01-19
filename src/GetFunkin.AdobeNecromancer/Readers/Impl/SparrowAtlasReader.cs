using System.Collections.Generic;
using System.IO;
using System.Xml;
using GetFunkin.AdobeNecromancer.SubTextures;
using GetFunkin.AdobeNecromancer.SubTextures.Impl;

namespace GetFunkin.AdobeNecromancer.Readers.Impl
{
    public class SparrowAtlasReader : IAtlasReader
    {
        public delegate ISubTexture SubTextureBuilder(
            string name,
            int x,
            int y,
            int width,
            int height,
            int frameX,
            int frameY,
            int frameWidth,
            int frameHeight
        );

        public SubTextureBuilder SubBuilder { get; }

        public SparrowAtlasReader(SubTextureBuilder subBuilder)
        {
            SubBuilder = subBuilder;
        }

        public SparrowAtlasReader() : this(ImmutableSubTexture.BuildTexture)
        {
        }

        public List<ISubTexture> Read(string file, ref string relativeImage)
        {
            using Stream stream = File.Open(file, FileMode.Open);
            return Read(stream, ref relativeImage);
        }

        public List<ISubTexture> Read(Stream stream, ref string relativeImage)
        {
            relativeImage = Path.ChangeExtension(relativeImage, ".png");
            
            using XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings {CheckCharacters = false});

            List<ISubTexture> textures = new();

            bool startingElement = true;

            while (reader.Read())
            {
                if (reader.NodeType is XmlNodeType.XmlDeclaration
                    or XmlNodeType.Comment
                    or XmlNodeType.Whitespace
                    or XmlNodeType.EndElement)
                    continue;

                if (reader.NodeType == XmlNodeType.EndElement)
                    break;

                if (reader.NodeType == XmlNodeType.Element && startingElement)
                {
                    startingElement = false;
                    relativeImage = reader.GetAttribute("imagePath");
                    continue;
                }

                string name = reader.GetAttribute("name");
                int x = int.Parse(reader.GetAttribute("x")!); // expected value
                int y = int.Parse(reader.GetAttribute("y")!); // expected value
                int width = int.Parse(reader.GetAttribute("width")!); // expected value
                int height = int.Parse(reader.GetAttribute("height")!); // expected value
                int frameX = int.Parse(reader.GetAttribute("frameX") ?? "0"); // optional value
                int frameY = int.Parse(reader.GetAttribute("frameY") ?? "0"); // optional value
                int frameWidth = int.Parse(reader.GetAttribute("frameWidth") ?? "0"); // optional value
                int frameHeight = int.Parse(reader.GetAttribute("frameHeight") ?? "0"); // optional value
                textures.Add(SubBuilder(name, x, y, width, height, frameX, frameY, frameWidth, frameHeight));
            }

            return textures;
        }
    }
}