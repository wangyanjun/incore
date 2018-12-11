namespace inc.protocols.mc
{
    /// <summary>
    /// MC protocol control code
    /// </summary>
    public enum MCControlCode
    {
        /// <summary>
        /// Start of Text
        /// </summary>
        STX = 0x02,

        /// <summary>
        /// End of Text
        /// </summary>
        ETX = 0x03,

        /// <summary>
        /// End of Transmission
        /// </summary>
        EOT = 0x04,

        /// <summary>
        /// Enquiry
        /// </summary>
        ENQ = 0x05,

        /// <summary>
        /// Acknowledge
        /// </summary>
        ACK = 0x06,

        /// <summary>
        /// Line feed
        /// </summary>
        LF = 0x0A,

        /// <summary>
        /// Clear
        /// </summary>
        CL = 0x0C,

        /// <summary>
        /// Carriage Return
        /// </summary>
        CR = 0x0D,

        /// <summary>
        /// Negative Acknowledge
        /// </summary>
        NCK = 0x15
    }
}
