using inc.core;
using System;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represents a modbus read and wirte command.
    /// </summary>
    public class ReadWriteCommand : IEncoder
    {
        /// <summary>
        /// Get or set function code
        /// </summary>
        public ModbusFunction FunctionCode { get; } = ModbusFunction.ReadWriteMultipleRegister;

        /// <summary>
        /// Get or set the read starting address
        /// </summary>
        public ushort ReadStartingAddress { get; set; }

        /// <summary>
        /// Get or set the read quantity
        /// </summary>
        public ushort ReadCount{ get; set; }

        /// <summary>
        /// Content to be written
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Get or set the write starting address
        /// </summary>
        public ushort WriteStartingAddress { get; set; }
        
        /// <summary>
        /// Get or set write count
        /// </summary>
        public ushort WriteCount { get; set; }

        /// <summary>
        /// Encode command.
        /// </summary>
        /// <returns>Result package</returns>
        public unsafe byte[] Encode()
        {
            byte[] result = new byte[10 + Content.Length];
            fixed (byte* p0 = result)
            {
                byte* p = p0;
                *p++ = (byte)FunctionCode;
                ushort* up = (ushort*)p;
                *up++ = ReadStartingAddress;
                *up++ = ReadCount;
                *up++ = WriteStartingAddress;
                *up++ = WriteCount;
                HexHelper.SwapEvenOdd(p0, 1, 8);
                p = (byte*)up;
                *p++ = (byte)(Content.Length);
                Array.Copy(Content, 0, result, 10, Content.Length);
            }

            return result;
        }
    }
}
