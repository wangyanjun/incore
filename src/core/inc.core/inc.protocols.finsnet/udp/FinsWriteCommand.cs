using inc.core;
using System;

namespace inc.protocols.finsnet.udp
{
    /// <summary>
    /// Class represent finsnet write command
    /// </summary>
    public class FinsWriteCommand : FinsCommand,
         IAddressableEncoder
    {
        /// <summary>
        /// Get or set memory area code
        /// </summary>
        public byte MemoryAreaCode { get; set; }

        /// <summary>
        /// Get or set starting address
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// Get or set sub address
        /// </summary>
        public byte SubAddress { get; set; }

        /// <summary>
        /// Get or set write count
        /// </summary>
        public ushort Length { get; set; } = 1;

        /// <summary>
        /// Get or set write data
        /// </summary>
        public byte[] Data { get; set; } = new byte[0];

        /// <summary>
        /// Set boolean data
        /// </summary>
        /// <param name="value">data</param>
        public void SetData(bool value)
        {
            Data = new byte[1];
            Data[0] = value ? (byte)0x01 : (byte)0x00;
        }

        /// <summary>
        /// Encode this command
        /// </summary>
        /// <returns>Encode result</returns>
        public unsafe byte[] Encode()
        {
            var result = new byte[6 + Data.Length];
            fixed (byte* p0 = result)
            {
                byte* p = p0;
                *p++ = MemoryAreaCode;
                SetValue(ref p, Address);
                *p++ = SubAddress;
                SetValue(ref p, Length);
                if (Data != null && Data.Length > 0)
                {
                    Array.Copy(Data, 0, result, 6, Data.Length);
                }
            }

            return result;
        }

        private unsafe void SetValue(ref byte* p, ushort address)
        {
            *((ushort*)p) = address;
            Swap(p, 0, 2);
            p += 2;
        }

        public bool FillAddress(string address)
        {
            var result = true;
            var prefix = string.Empty;
            var add = string.Empty;
            var sub = string.Empty;
            var memoryType = FinsMemoryArea.DM;
            int part = 0;
            for (int i = 0; i < address.Length; i++)
            {
                var ch = address[i];
                switch (part)
                {
                    case 0:
                        {
                            if (Char.IsDigit(ch))
                            {
                                add += ch;
                                part++;
                            }
                            else
                            {
                                prefix += ch;
                            }

                            break;
                        }

                    case 1:
                        {
                            if (ch == '.')
                            {
                                part++;
                            }
                            else
                            {
                                add += ch;
                            }

                            break;
                        }

                    default:
                        {
                            sub += ch;
                            break;
                        }
                }
            }


            result = int.TryParse(add, out int addValue);
            int subValue = 0;
            if (!string.IsNullOrEmpty(sub))
            {
                result = result && int.TryParse(sub, out subValue);
            }

            if (result)
            {
                switch (prefix.ToLower())
                {
                    case "%d":
                    case "d":
                        {
                            memoryType = FinsMemoryArea.DM;
                            break;
                        }

                    case "%w":
                    case "w":
                        {
                            memoryType = FinsMemoryArea.WR;
                            break;
                        }

                    default:
                        {
                            result = false;
                            break;
                        }
                }
            }

            if (result)
            {
                MemoryAreaCode = (byte)memoryType;
                Address = (ushort)addValue;
                SubAddress = (byte)subValue;
            }

            return result;
        }

        public unsafe void SetData(byte value)
        {
            Data = new[] { value };
        }

        public unsafe void SetData(short value)
        {
            Data = new byte[2];
            fixed (byte* p0 = Data)
            {
                *((short*)p0) = value;
                Swap(p0, 0, 2);
            }
        }

        public unsafe void SetData(ushort value)
        {
            Data = new byte[2];
            fixed (byte* p0 = Data)
            {
                *((ushort*)p0) = value;
                Swap(p0, 0, 2);
            }
        }

        public unsafe void SetData(int value)
        {
            Data = new byte[4];
            fixed (byte* p0 = Data)
            {
                *((int*)p0) = value;
                Swap(p0, 0, 4);
            }
        }

        public unsafe void SetData(uint value)
        {
            Data = new byte[4];
            fixed (byte* p0 = Data)
            {
                *((uint*)p0) = value;
                Swap(p0, 0, 4);
            }
        }

        public unsafe void SetData(long value)
        {
            Data = new byte[8];
            fixed (byte* p0 = Data)
            {
                *((long*)p0) = value;
                Swap(p0, 0, 8);
            }
        }

        public unsafe void SetData(ulong value)
        {
            Data = new byte[8];
            fixed (byte* p0 = Data)
            {
                *((ulong*)p0) = value;
                Swap(p0, 0, 8);
            }
        }

        public unsafe void SetData(float value)
        {
            Data = new byte[4];
            fixed (byte* p0 = Data)
            {
                *((float*)p0) = value;
                Swap(p0, 0, 4);
            }
        }

        public unsafe void SetData(double value)
        {
            Data = new byte[8];
            fixed (byte* p0 = Data)
            {
                *((double*)p0) = value;
                Swap(p0, 0, 8);
            }
        }

        public unsafe void SetData(bool[] value)
        {
            Data = new byte[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                Data[i] = value[i] ? (byte)0x01 : (byte)0x00;
            }
        }

        public unsafe void SetData(short[] value)
        {
            const int len = 2;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((short*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(byte[] value)
        {
            const int len = 1;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *p = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(ushort[] value)
        {
            const int len = 2;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((ushort*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(uint[] value)
        {
            const int len = 4;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((uint*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(int[] value)
        {
            const int len = 4;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((int*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(long[] value)
        {
            const int len = 8;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((long*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(float[] value)
        {
            const int len = 4;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((float*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(double[] value)
        {
            const int len = 8;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((double*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }

        public unsafe void SetData(ulong[] value)
        {
            const int len = 4;
            Data = new byte[value.Length * len];
            fixed (byte* p0 = Data)
            {
                byte* p = p0;
                foreach (var v in value)
                {
                    *((ulong*)p) = v;
                    p += len;
                }

                Swap(p0, 0, Data.Length);
            }
        }
    }
}
