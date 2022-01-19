namespace GetFunkin.AdobeNecromancer.Atlases
{
    /// <summary>
    ///     Simple interface outlining core atlas frame data shared between formats.
    /// </summary>
    public interface IAtlasFrame
    {
        /// <summary>
        ///     The X position of the frame.
        /// </summary>
        int X { get; }

        /// <summary>
        ///     The Y position of the frame.
        /// </summary>
        int Y { get; }
        
        /// <summary>
        ///     The frame snippet width.
        /// </summary>
        int Width { get; }

        /// <summary>
        ///     The frame snippet height.
        /// </summary>
        int Height { get; }
    }
}