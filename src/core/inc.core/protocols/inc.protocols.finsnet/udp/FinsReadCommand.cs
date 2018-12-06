using inc.core;
using System;

namespace inc.protocols.finsnet.udp
{
    public class FinsReadCommand : FinsCommand,
             IAddressableEncoder
    {
        public byte MemoryAreaCode { get; set; }

        public ushort Address { get; set; }

        public byte SubAddress { get; set; }

        public ushort Length { get; set; } = 1;

        public unsafe byte[] Encode()
        {
            var result = new byte[6];
            fixed (byte* p0 = result)
            {
                byte* p = p0;
                *p++ = MemoryAreaCode;
                SetValue(ref p, Address);
                *p++ = SubAddress;
                SetValue(ref p, Length);
            }

            return result;
        }

        private unsafe void SetValue(ref byte* p, ushort address)
        {
            *((ushort*)p) = address;
            byte mid = p[1];
            p[1] = *p;
            p[0] = mid;
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
    }
}
