using System.Collections.Generic;
using System.IO;

namespace GetFunkin.AdobeNecromancer.Atlases
{
    /// <summary>
    ///     Base interface for atlas readers.
    /// </summary>
    public interface IAtlasReader<TFrame> where TFrame : IAtlasFrame
    {
        /// <summary>
        ///     The type of image name modification this atlas reader performs.
        /// </summary>
        NameModifierType NameModifierType { get; }

        /// <summary>
        ///     Reads frame data from a stream.
        /// </summary>
        /// <param name="stream">The stream of bytes to read data from.</param>
        /// <param name="imageName">The image name - depending on the atlas, it may be automatically set by the reader or may need to be manually specified.</param>
        /// <returns>A list of <see cref="IAtlasFrame"/>s containing appropriate frame data.</returns>
        List<TFrame> ReadStream(Stream stream, ref string imageName);
    }
}