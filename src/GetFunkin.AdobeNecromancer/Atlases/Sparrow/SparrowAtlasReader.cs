using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace GetFunkin.AdobeNecromancer.Atlases.Sparrow
{
    public class SparrowAtlasReader : IAtlasReader<ISparrowAtlasFrame>
    {
        /// <inheritdoc cref="IAtlasReader{TFrame}.ReadStream"/>
        public List<ISparrowAtlasFrame> ReadStream(Stream stream, ref string imageName)
        {
            List<ISparrowAtlasFrame> frames = new();
            
            using XmlReader xmlReader = XmlReader.Create(stream);

            bool start = true;

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType is XmlNodeType.XmlDeclaration
                    or XmlNodeType.Comment
                    or XmlNodeType.Whitespace
                    or XmlNodeType.EndElement)
                    continue;

                if (xmlReader.NodeType is XmlNodeType.EndElement)
                    break;

                if (xmlReader.NodeType == XmlNodeType.Element && start)
                {
                    start = false;
                    imageName = xmlReader.GetAttribute("imagePath");
                    continue;
                }

                string name = xmlReader.GetAttribute("name");
                
                // expected values
                int x = int.Parse(xmlReader.GetAttribute("x")!);
                int y = int.Parse(xmlReader.GetAttribute("y")!);
                int width = int.Parse(xmlReader.GetAttribute("width")!);
                int height = int.Parse(xmlReader.GetAttribute("height")!);
                
                // optional values
                int frameX = int.Parse(xmlReader.GetAttribute("frameX") ?? "-1");
                int frameY = int.Parse(xmlReader.GetAttribute("frameY") ?? "-1");
                int frameWidth = int.Parse(xmlReader.GetAttribute("frameWidth") ?? "-1");
                int frameHeight = int.Parse(xmlReader.GetAttribute("frameHeight") ?? "-1");
                bool rotated = bool.Parse(xmlReader.GetAttribute("rotated") ?? "false");
                bool flipX = bool.Parse(xmlReader.GetAttribute("flipX") ?? "false");
                bool flipY = bool.Parse(xmlReader.GetAttribute("flipY") ?? "false");

                frames.Add(new SparrowAtlasFrame
                {
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height,
                    Name = name,
                    Trimmed = frameX > -1,
                    Rotated = rotated,
                    FlipX = flipX,
                    FlipY = flipY,
                    FrameX = frameX,
                    FrameY = frameY,
                    FrameWidth = frameWidth,
                    FrameHeight = frameHeight
                });
            }

            return frames;
        }
    }
}