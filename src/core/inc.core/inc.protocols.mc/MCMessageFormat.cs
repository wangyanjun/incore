namespace inc.protocols.mc
{
    /// <summary>
    /// MC protocol message format
    /// </summary>
    public enum MCMessageFormat
    {
        /// <summary>
        /// Pure ASCII code
        /// </summary>
        Format1 = 0,

        /// <summary>
        /// ASCII code, format with block number appended
        /// </summary>
        Format2 = 1,

        /// <summary>
        /// ASCII code, format enclosed with STX and ETX
        /// </summary>
        Format3 = 2,

        /// <summary>
        /// ASCII code, format with CR and LF appended at the end
        /// </summary>
        Format4 = 3,

        /// <summary>
        /// Binary format,can be used by 4C frame.
        /// </summary>
        Format5 = 4
    }
}
