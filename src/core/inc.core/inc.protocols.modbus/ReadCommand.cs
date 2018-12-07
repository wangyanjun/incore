namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represents a modbus read command.
    /// </summary>
    public class ReadCommand : IEncoder
    {
        /// <summary>
        /// Get or set function code
        /// </summary>
        public ModbusFunctionCode Code { get; set; } = ModbusFunctionCode.ReadCoils;

        /// <summary>
        /// Get or set the starting address
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// Get or set the quantity
        /// </summary>
        public ushort Count { get; set; }

        /// <summary>
        /// Encode command.
        /// </summary>
        /// <returns>Result package</returns>
        public unsafe byte[] Encode()
        {
            byte[] result = new byte[5];
            fixed (byte* p0 = result)
            {
                byte* p = p0;
                *p++ = (byte)Code;
                ushort* up = (ushort*)p;
                *up++ = Address;
                *up++ = Count;
            }

            return result;
        }
    }  
}
