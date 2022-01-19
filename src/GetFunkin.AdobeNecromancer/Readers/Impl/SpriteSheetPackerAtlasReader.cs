using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GetFunkin.AdobeNecromancer.SubTextures;
using GetFunkin.AdobeNecromancer.SubTextures.Impl;

namespace GetFunkin.AdobeNecromancer.Readers.Impl
{
    public class SpriteSheetPackerAtlasReader : IAtlasReader
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

        public SpriteSheetPackerAtlasReader(SubTextureBuilder subBuilder)
        {
            SubBuilder = subBuilder;
        }

        public SpriteSheetPackerAtlasReader() : this(ImmutableSubTexture.BuildTexture)
        {
        }

        public List<ISubTexture> Read(string file, ref string relativeImage)
        {
            using Stream stream = File.Open(file, FileMode.Open);
            return Read(stream, ref relativeImage);
        }

        public List<ISubTexture> Read(Stream stream, ref string relativeImage)
        {
            using StreamReader reader = new(stream, Encoding.UTF8);

            List<string> lines = new();
            string tLine;

            while ((tLine = reader.ReadLine()) is not null)
                lines.Add(tLine);

            List<ISubTexture> textures = new();

            textures.AddRange(from line in lines
                select line.Split('=')
                into lineData
                let name = lineData[0]
                let textureData = lineData[1].TrimStart().Split(' ').Select(int.Parse).ToArray()
                select SubBuilder(
                    name,
                    textureData[0],
                    textureData[1],
                    textureData[2],
                    textureData[3],
                    0,
                    0,
                    0,
                    0
                ));

            return textures;
        }
    }
}