using System.Drawing;

namespace GetFunkin.AdobeNecromancer.Atlases.SpriteSheetPacker
{
    public interface ISpriteSheetPackerAtlasFrame : IAtlasFrame
    {
        // TODO: Document? Some properties directly use this information in the reader.
        /// <summary>
        ///     Assorted array of data.
        /// </summary>
        string[] Data { get; }

        /// <summary>
        ///     Assorted array of image region data.
        /// </summary>
        string[] Region { get; }

        /// <summary>
        ///     Calculate the rectangular boundaries and relative position from the provided <see cref="Data"/>.
        /// </summary>
        /// <returns>A constructed <see cref="Rectangle"/> with the aforementioned information.</returns>
        Rectangle GetRect();

        /// <summary>
        ///     Retrieves the source size of <see cref="GetRect"/>.
        /// </summary>
        /// <returns>The source size, based on <see cref="Rectangle.Height"/> and <see cref="Rectangle.Width"/>.</returns>
        Point GetSourceSize();

        /// <summary>
        ///     Unused.
        /// </summary>
        /// <returns>Always returns <c>(0, 0)</c>.</returns>
        Point GetOffset();
    }
}