using inc.core;
using System;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Represent frame of modbus tcp
    /// </summary>
    public class ModbusTCPFrame : IEncoder
    {
        /// <summary>
        /// Get or set the verify message set by client
        /// </summary>
        public ushort Verify { get; set; }

        /// <summary>
        /// Get or set protocol type. 0x00 represent TCP/IP protocol
        /// </summary>
        public ushort Type { get; set; }

        /// <summary>
        /// Get or set content
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Encode this frame
        /// </summary>
        /// <returns>Encoded data</returns>
        public unsafe byte[] Encode()
        {
            const int headerLength = 6;
            var dataLength = Content?.Length ?? 0;
            byte[] result = new byte[headerLength + dataLength];
            fixed (byte* p0 = result)
            {
                ushort* up = (ushort*)p0;
                *up++ = Verify;
                *up++ = Type;
                *up++ = (ushort)(dataLength);
                HexHelper.SwapEvenOdd(p0, 0, headerLength);
                if (dataLength > 0)
                {
                    Array.Copy(Content, 0, result, headerLength, Content.Length);
                }
            }

            return result;
        }

        /// <summary>
        /// Construct a modbus tcp frame
        /// </summary>
        /// <param name="data">data</param>
        public ModbusTCPFrame(IEncoder data)
        {
            Content = data.Encode();
        }
    }
}
