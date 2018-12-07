using System;

namespace inc.protocols.modbus
{
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

        public unsafe byte[] Encode()
        {
            var dataLength = Content?.Length ?? 0;
            byte[] result = new byte[3 + dataLength];
            fixed (byte* p0 = result)
            {
                ushort* up = (ushort*)p0;
                *up++ = Verify;
                *up++ = Type;
                *up++ = (ushort)(dataLength + 1);
                if (dataLength > 0)
                {
                    Array.Copy(Content, 0, result, 6, Content.Length);
                }
            }

            return result;
        }

        public ModbusTCPFrame(IEncoder data)
        {
            Content = data.Encode();
        }
    }
}
