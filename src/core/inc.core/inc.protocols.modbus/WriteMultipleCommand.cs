using inc.core;
using System;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represent write command for modbus
    /// </summary>
    public class WriteMultipleCommand : IEncoder
    {
        /// <summary>
        /// Get or set function code.
        /// </summary>
        public ModbusFunction Function { get; set; } = ModbusFunction.WriteSingleCoil;

        /// <summary>
        /// Get or set starting address
        /// </summary>
        public ushort StartingAddress { get; set; }

        /// <summary>
        /// Get or set quantity of coils (or registers)
        /// </summary>
        public ushort Quantity { get; set; }

        /// <summary>
        /// Get or set content to be sent
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Encode this command
        /// </summary>
        /// <returns>Encode result</returns>
        public unsafe byte[] Encode()
        {
            byte[] result = new byte[5 + Content.Length];
            fixed (byte* p0 = result)
            {
                byte* p = p0;
                *p++ = (byte)Function;
                ushort* up = (ushort*)p;
                *up++ = StartingAddress;
                *up++ = Quantity;
                HexHelper.SwapEvenOdd(p, 1, 4);
                p = (byte*)up;
                *p++ = (byte)(Content.Length);
                Array.Copy(Content, 0, result, 5, Content.Length);
            }

            return result;
        }
    }
}
