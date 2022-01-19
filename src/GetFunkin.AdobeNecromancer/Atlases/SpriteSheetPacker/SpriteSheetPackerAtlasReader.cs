using System.Collections.Generic;
using System.IO;

namespace GetFunkin.AdobeNecromancer.Atlases.SpriteSheetPacker
{
    public class SpriteSheetPackerAtlasReader : IAtlasReader<ISpriteSheetPackerAtlasFrame>
    {
        public NameModifierType NameModifierType => NameModifierType.Extension;

        public List<ISpriteSheetPackerAtlasFrame> ReadStream(Stream stream, ref string imageName)
        {
            imageName = Path.ChangeExtension(imageName, ".png");
            
            using StreamReader streamReader = new(stream);

            List<ISpriteSheetPackerAtlasFrame> frames = new();

            string ssp = streamReader.ReadToEnd().Trim();
            string[] lines = ssp.Split('\n');

            // The LINQ equivalent is so disgusting that I don't want everyone to ever need to lay their eyes on it.
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (string line in lines)
            {
                string[] data = line.Split('=');
                string name = data[0].Trim();
                string[] region = data[1].Trim().Split(' ');

                frames.Add(new SpriteSheetPackerAtlasFrame
                {
                    Name = name,
                    Data = data,
                    Region = region
                });
            }

            return frames;
        }
    }
}