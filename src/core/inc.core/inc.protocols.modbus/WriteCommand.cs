using inc.core;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represent write command for modbus
    /// </summary>
    public class WriteCommand : IEncoder
    {
        /// <summary>
        /// Get or set function code.
        /// </summary>
        public ModbusFunction Function { get; set; } = ModbusFunction.WriteSingleCoil;

        /// <summary>
        /// Get or set starting address
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// Get or set output value
        /// </summary>
        public ushort OutputValue { get; set; }

        /// <summary>
        /// Encode this command
        /// </summary>
        /// <returns>Encode result</returns>
        public unsafe byte[] Encode()
        {
            byte[] result = new byte[5];
            fixed (byte* p0 = result)
            {
                byte* p = p0;
                *p++ = (byte)Function;
                ushort* up = (ushort*)p;
                *up++ = Address;
                *up++ = OutputValue;
                HexHelper.SwapEvenOdd(p, 0, 4);
            }

            return result;
        }
    }
}
