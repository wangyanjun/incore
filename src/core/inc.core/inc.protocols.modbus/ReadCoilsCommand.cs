using inc.core;
using System;

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
