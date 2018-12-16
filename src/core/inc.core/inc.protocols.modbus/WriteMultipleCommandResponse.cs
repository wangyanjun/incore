using inc.core;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represent write multiple command response for modbus.
    /// </summary>
    public class WriteMultipleCommandResponse : IDecoder
    {
        /// <summary>
        /// Get or set function code
        /// </summary>
        public ModbusFunction FunctionCode { get; set; } = ModbusFunction.ReadCoils;

        /// <summary>
        /// Get or set exception code.
        /// </summary>
        public byte? ExceptionCode { get; set; }

        /// <summary>
        /// Get or set output address
        /// </summary>
        public ushort StartingAddress { get; set; }

        /// <summary>
        /// Get or set output value
        /// </summary>
        public ushort Quantity { get; set; }

        /// <summary>
        /// Get wehter has exception
        /// </summary>
        public bool HasException => ExceptionCode.HasValue;

        /// <summary>
        /// Decode data
        /// </summary>
        /// <param name="data">Data to be decoded</param>
        /// <returns>Decode result</returns>
        public unsafe bool Decode(byte[] data)
        {
            var result = false;
            if (data != null && data.Length >= 2)
            {
                FunctionCode = (ModbusFunction)data[0];
                if (data.Length == 2)
                {
                    ExceptionCode = data[0];
                }
                else
                {
                    fixed (byte* p0 = data)
                    {
                        ushort* up = (ushort*)(p0 + 1);
                        StartingAddress = *up++;
                        Quantity = *up++;
                    }
                }
            }

            return result;
        }
    }
}
