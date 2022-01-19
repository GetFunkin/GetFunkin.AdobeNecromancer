namespace GetFunkin.AdobeNecromancer.SubTextures
{
    /// <summary>
    ///     An interface for implementing a scissored texture area.
    /// </summary>
    public interface IScissoredTexture
    {
        /// <summary>
        ///     The top-left X position.
        /// </summary>
        int X { get; }

        /// <summary>
        ///     The top-left Y position.
        /// </summary>
        int Y { get; }

        /// <summary>
        ///     The section width.
        /// </summary>
        int Width { get; }

        /// <summary>
        ///     The section height.
        /// </summary>
        int Height { get; }
    }
}