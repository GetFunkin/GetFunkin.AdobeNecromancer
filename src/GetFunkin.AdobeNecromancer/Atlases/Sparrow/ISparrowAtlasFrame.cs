using System.Drawing;

namespace GetFunkin.AdobeNecromancer.Atlases.Sparrow
{
    public interface ISparrowAtlasFrame : IAtlasFrame
    {
        /// <summary>
        ///     The frame's identifiable name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Whether additional trimming should be supplied based off of <c>Frame&lt;N&gt;</c> properties.
        /// </summary>
        bool Trimmed { get; }

        /// <summary>
        ///     Whether a -90 degree angle rotation should be applied.
        /// </summary>
        bool Rotated { get; }

        /// <summary>
        ///     Whether the frame is flipped on the X-axis.
        /// </summary>
        bool FlipX { get; }

        /// <summary>
        ///     Whether the frame is flipped ont he Y-axis.
        /// </summary>
        bool FlipY { get; }

        /// <summary>
        ///     The cropped X position.
        /// </summary>
        int FrameX { get; }

        /// <summary>
        ///     The cropped Y position.
        /// </summary>
        int FrameY { get; }

        /// <summary>
        ///     The cropped frame width.
        /// </summary>
        int FrameWidth { get; }

        /// <summary>
        ///     The cropped height.
        /// </summary>
        int FrameHeight { get; }

        /// <summary>
        ///     Calculate the rectangular boundaries and location of the frame relative to the full atlas.
        /// </summary>
        /// <returns>The crop-ready rectangle.</returns>
        Rectangle GetRect();

        /// <summary>
        ///     Calculate the rectangular boundaries of the trimming parameters of this frame. Results differ based on <see cref="Trimmed"/>.
        /// </summary>
        /// <returns>The trimmed size.</returns>
        Rectangle GetSize();

        /// <summary>
        ///     Retrieves the angle of rotation, in degrees.
        /// </summary>
        /// <returns><c>-90</c> if <see cref="Rotated"/> is <see langkey="true"/>, otherwise <c>0</c>.</returns>
        float GetAngle();

        /// <summary>
        ///     Calculates the offset based on the trimmed frame size.
        /// </summary>
        /// <returns>A point containing negative values of the <see cref="Rectangle.Left"/> and <see cref="Rectangle.Top"/> properties.</returns>
        Point GetOffset();

        /// <summary>
        ///     Calculates the source size of the frame.
        /// </summary>
        /// <returns>A point containing the normal values of the <see cref="Rectangle.Width"/> and <see cref="Rectangle.Height"/> properties based off of the trimmed size.</returns>
        Point SourceSize();
    }
}