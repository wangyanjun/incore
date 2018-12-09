namespace inc.core
{
    /// <summary>
    /// The decode interface
    /// </summary>
    public interface  IDecoder
    {
        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="content">content to be decoded</param>
        /// <returns>decode result</returns>
        bool Decode(byte[] content);
    }
}
