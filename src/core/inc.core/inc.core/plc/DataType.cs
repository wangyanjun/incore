namespace inc.core.plc
{
    /// <summary>
    /// Data type
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Bit data
        /// </summary>
        Bit,

        /// <summary>
        /// Byte data
        /// </summary>
        Byte,

        /// <summary>
        /// Unsigned 16 bits represented by bytes
        /// </summary>
        UInt16Bytes,

        /// <summary>
        /// Booleans represented by bytes.
        /// </summary>
        BooleanBytes,

        /// <summary>
        /// Signed 16 bits integer
        /// </summary>
        Int16,

        /// <summary>
        /// Signed 32 bits integer
        /// </summary>
        Int32,

        /// <summary>
        /// Signed 64 bits integer
        /// </summary>
        Int64,

        /// <summary>
        /// Unsigned 16 bits integer
        /// </summary>
        UInt16,

        /// <summary>
        /// Unsigned 32 bits integer
        /// </summary>
        UInt32,

        /// <summary>
        /// unsigned 64 bits integer
        /// </summary>
        UInt64,

        /// <summary>
        /// float
        /// </summary>
        Single,

        /// <summary>
        /// double
        /// </summary>
        Double
    }
}
