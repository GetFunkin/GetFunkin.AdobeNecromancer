namespace GetFunkin.AdobeNecromancer.Atlases
{
    /// <summary>
    ///     Identifiers for ways in which atlases modify <c>ref</c>-ed image names.
    /// </summary>
    public enum NameModifierType
    {
        /// <summary>
        ///     Performs no modifications on image names.
        /// </summary>
        None,
        
        /// <summary>
        ///     Only ensures correctness by modifying the image extension (i.e. a <c>.txt</c> input gets changed to <c>.png</c>.
        /// </summary>
        Extension,
        
        /// <summary>
        ///     Completely changes the image name, often without care for the original value.
        /// </summary>
        Full
    }
}