﻿using inc.core;
using System;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represent read command response for modbus.
    /// </summary>
    public class ReadCommandResponse : IDecoder
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
        /// Get wehter has exception
        /// </summary>
        public bool HasException => ExceptionCode.HasValue;

        /// <summary>
        /// Get or set bytes count
        /// </summary>
        public byte BytesCount { get; set; }

        /// <summary>
        /// Get content
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Decode data
        /// </summary>
        /// <param name="data">Data to be decoded</param>
        /// <returns>Decode result</returns>
        public bool Decode(byte[] data)
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
                    BytesCount = data[1];
                    Content = new byte[BytesCount];
                    if (Content.Length > 0)
                    {
                        Array.Copy(data, 2, Content, 0, Content.Length);
                    }
                }
            }

            return result;
        }
    }
}
