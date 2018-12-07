using inc.core;
using System;

namespace inc.protocols.modbus
{
    public class ModbusRTUFrame : IEncoder
    {
        private static readonly CRC16 s_crc = new CRC16();

        /// <summary>
        /// Get maxium data length
        /// </summary>
        public const int MaxDataLength = 253;

        /// <summary>
        /// Get or set additional address
        /// </summary>
        public byte Address { get; set; }

        /// <summary>
        /// Get or set CRC
        /// </summary>
        public ushort CRC { get; set; }

        /// <summary>
        /// Get or set content
        /// </summary>
        public byte[] Content { get; set; }

        public ModbusRTUFrame() { }

        public ModbusRTUFrame(IEncoder data)
        {
            Content = data.Encode();
        }

        /// <summary>
        /// Encode this frame
        /// </summary>
        /// <returns>Encoded data</returns>
        public unsafe byte[] Encode()
        {
            var dataLength = Content?.Length ?? 0;
            byte[] result = new byte[3 + dataLength];
            fixed (byte* p0 = result)
            {
                byte* p = p0;
                *p++ = Address;
                if (dataLength > 0)
                {
                    Array.Copy(Content, 0, result, 1, Content.Length);
                    CRC = s_crc.Compute(result, 1, dataLength);
                }

                p += dataLength;
                *((ushort*)p) = CRC;
            }

            return result;
        }
    }
}
