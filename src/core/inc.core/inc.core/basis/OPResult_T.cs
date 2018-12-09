namespace inc.core
{
    /// <summary>
    /// Operation result
    /// </summary>
    /// <typeparam name="T">Content data type</typeparam>
    public class OpResult<T> : OpResult
    {
        /// <summary>
        /// Get or set content
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// Construct OpResult
        /// </summary>
        public OpResult() { }

        /// <summary>
        /// Construct OpResult
        /// </summary>
        /// <param name="content">content data</param>
        public OpResult(T content)
        {
            Content = content;
        }

        /// <summary>
        /// Get content
        /// </summary>
        /// <returns>Additional content data</returns>
        public override object GetContent() => IsSuccess ? Content : default(object);
    }
}
