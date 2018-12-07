using inc.core;
using System;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represent for modbus rtu frame
    /// </summary>
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

        /// <summary>
        /// Construct modbus rtu frame
        /// </summary>
        public ModbusRTUFrame() { }

        /// <summary>
        /// Construct modbus rtu frame
        /// </summary>
        /// <param name="data">data</param>
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
            const int headerLength = 3;
            var dataLength = Content?.Length ?? 0;
            byte[] result = new byte[headerLength + dataLength];
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
                HexHelper.SwapEvenOdd(p, 2);
            }

            return result;
        }
    }
}
