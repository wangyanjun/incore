using inc.core.plc;
using System;

namespace inc.protocols.finsnet
{
    /// <summary>
    /// Finsnet address
    /// </summary>
    public class FinsnetAddress : AddressBase
    {
        /// <summary>
        /// Get or set memory area
        /// </summary>
        public FinsMemoryArea MemoryArea { get; set; }

        /// <summary>
        /// Get copy of this object
        /// </summary>
        public override IAddress Copy
        {
            get
            {
                var result = new FinsnetAddress();
                CopyTo(result);
                return result;
            }
        }

        public override bool CanMerge(IAddress address, int maxRange)
        {
            var result = false;
            if (address is FinsnetAddress x)
            {
                result = MemoryArea == x.MemoryArea;
                if (result)
                {
                    result = MainAddress - x.MainAddress < maxRange;
                }
            }

            return result;
        }

        /// <summary>
        /// Compute address
        /// </summary>
        /// <returns>Address string</returns>
        public override string ComputeAddress()
        {
            var result = string.Empty;
            switch (MemoryArea)
            {
                case FinsMemoryArea.DM:
                    {
                        result += "D" + MainAddress;
                        break;
                    }

                case FinsMemoryArea.WR:
                    {
                        result += "W" + MainAddress;
                        if (SubAddress.HasValue)
                        {
                            result += "." + SubAddress;
                        }

                        break;
                    }
            }

            return result;
        }

        /// <summary>
        /// Copy info to destinate address
        /// </summary>
        /// <param name="address">destinate address</param>
        public override void CopyTo(IAddress address)
        {
            base.CopyTo(address);
            if (address is FinsnetAddress fdest)
            {
                fdest.MemoryArea = MemoryArea;
            }
        }

        /// <summary>
        /// Compare address
        /// </summary>
        /// <param name="other">other address to be compared</param>
        /// <returns>compare result</returns>
        public override int CompareTo(IAddress other)
        {
            if (!(other is FinsnetAddress x))
            {
                return 1;
            }

            var result = MemoryArea.CompareTo(x.MemoryArea);
            if (result == 0)
            {
                result = base.CompareTo(other);
            }

            return result;
        }

        /// <summary>
        /// Parse address
        /// </summary>
        /// <param name="address">address string</param>
        /// <returns>Is parse success</returns>
        public override bool Parse(string address)
        {
            Address = address;
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

            int? subAddr = default(int?);
            result = int.TryParse(add, out int addValue);
            if (!string.IsNullOrEmpty(sub))
            {
                int subValue = 0;
                result = result && int.TryParse(sub, out subValue);
                if (result) subAddr = subValue;
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
                MemoryArea = memoryType;
                MainAddress = addValue;
                SubAddress = subAddr;
            }
            else
            {
                MemoryArea = FinsMemoryArea.NotComputed;
                MainAddress = 0;
                SubAddress = null;
            }

            return result;
        }

        /// <summary>
        /// Merge address
        /// </summary>
        /// <param name="to">end address</param>
        /// <param name="Item">variable item</param>
        /// <param name="lastArrayLength">array length</param>
        public override void Merge(IAddress to, VariableItem Item, int? lastArrayLength)
        {
            Item.DataType = DataType.UInt16Bytes;
            switch (MemoryArea)
            {
                case FinsMemoryArea.WR:
                    {
                        Item.DataType = DataType.BooleanBytes;                       
                        break;
                    }
            }

            Item.ReadAddress = ComputeAddress();
            Item.WriteAddress = Item.ReadAddress;
            Item.ArrayLength = to.MainAddress - MainAddress + Item.DataType.SizeIn(MemoryArea);
            if (lastArrayLength.HasValue && lastArrayLength.Value > 1)
            {
                Item.ArrayLength += (lastArrayLength.Value - 1) * Item.DataType.SizeIn(MemoryArea);
            }

            switch (MemoryArea)
            {
                case FinsMemoryArea.WR:
                    {
                        Item.ReadAddress += ".0";
                        Item.ArrayLength = Item.ArrayLength.Value * 16;
                        break;
                    }
            }
        }
    }
}
