namespace GetFunkin.AdobeNecromancer.SubTextures
{
    /// <summary>
    ///     Interface representing a sub-texture spritesheet object.
    /// </summary>
    public interface ISubTexture
    {
        /// <summary>
        ///     The unique frame name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     The top-left X position.
        /// </summary>
        int X { get; }

        /// <summary>
        ///     The top-left Y position.
        /// </summary>
        int Y { get; }

        /// <summary>
        ///     The texture width.
        /// </summary>
        int Width { get; }

        /// <summary>
        ///     The texture height.
        /// </summary>
        int Height { get; }

        /// <summary>
        ///     Not always present - the trimmed X position.
        /// </summary>
        int FrameX { get; }

        /// <summary>
        ///     Not always present - the trimmed Y position.
        /// </summary>
        int FrameY { get; }

        /// <summary>
        ///     Not always present - the trimmed width.
        /// </summary>
        int FrameWidth { get; }

        /// <summary>
        ///     Not always present - the trimmed height.
        /// </summary>
        int FrameHeight { get; }

        /// <summary>
        ///     The sub-texture crop information.
        /// </summary>
        /// <returns></returns>
        IScissoredTexture Crop { get; }

        /// <summary>
        ///     Retrieves numerous useful pieces of information.
        /// </summary>
        /// <param name="trimmed">Whether this sub-texture was trimmed.</param>
        /// <param name="frame">The sub-texture's framing.</param>
        /// <param name="size">The sub-texture's size specifications.</param>
        void GetData(out bool trimmed, out IScissoredTexture frame, out IScissoredTexture size);

        /// <summary>
        ///     Retrieves numerous useful pieces of information.
        /// </summary>
        /// <param name="trimmed">Whether this sub-texture was trimmed.</param>
        /// <param name="frame">The sub-texture's framing.</param>
        /// <param name="size">The sub-texture's size specifications.</param>
        /// <param name="crop">A cropped sub-texture selection.</param>
        void GetData(
            out bool trimmed,
            out IScissoredTexture frame,
            out IScissoredTexture size,
            out IScissoredTexture crop
        );
    }
}