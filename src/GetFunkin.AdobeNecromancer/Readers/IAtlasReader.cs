using System.Collections.Generic;
using System.IO;
using GetFunkin.AdobeNecromancer.SubTextures;

namespace GetFunkin.AdobeNecromancer.Readers
{
    public interface IAtlasReader
    {
        List<ISubTexture> Read(string file, ref string relativeImage);

        List<ISubTexture> Read(Stream stream, ref string relativeImage);
    }
}