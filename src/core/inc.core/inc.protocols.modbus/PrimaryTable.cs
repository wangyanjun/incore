namespace inc.protocols.modbus
{
    public enum PrimaryTable
    {
        /// <summary>
        /// Read only single bit. 
        /// </summary>
        DiscreteInput,

        /// <summary>
        /// Read/write single bit.
        /// </summary>
        Coils,

        /// <summary>
        /// Read only 16 bits data
        /// </summary>
        InputRegister,

        /// <summary>
        /// Read/write 16 bits data
        /// </summary>
        HoldingRegister
    }
}
