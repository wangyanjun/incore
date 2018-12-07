using System.Collections.Generic;

namespace inc.core.plc
{
    public class MergedVariable : Variable
    {
        public const int MaxAddressRange = 1000;

        public const int MaxArrayLength = 300;

        private readonly List<Variable> _variables = new List<Variable>();

        public Variable First { get; private set; }

        public Variable Last { get; private set; }

        public double? MinRefreshSpan { get; private set; }

        public bool IsEmpty => _variables.Count == 0;

        public MergedVariable(PLCClient plc, string name) : base(new VariableItem(), plc)
        {
            Item.Name = name;
        }

        public MergedVariable(VariableItem item, PLCClient plc) : base(item, plc)
        {
        }

        public bool Merge(Variable variable)
        {
            var result = false;
            if (NeedMerge(variable) && CanMerge(variable))
            {
                if (IsEmpty)
                {
                    Last = variable;
                    First = variable;
                    Address = variable.Address.Copy;
                }
                else
                {
                    if (Compare(variable, Last) > 0) Last = variable;
                    if (Compare(variable, First) < 0) First = variable;
                }

                if (variable.Item.ReadSpanInSeconds.HasValue)
                {
                    if ((!MinRefreshSpan.HasValue) || (MinRefreshSpan.Value > variable.Item.ReadSpanInSeconds.Value))
                    {
                        MinRefreshSpan = variable.Item.ReadSpanInSeconds;
                    }
                }

                _variables.Add(variable);
                result = true;
            }

            return result;
        }

        public void MergeAddress()
        {
            if (!IsEmpty)
            {
                Item.ReadSpanInSeconds = MinRefreshSpan;
                First.Address.Merge(Last.Address, Item, Last.Item.ArrayLength);               
            }
        }

        public bool CanMerge(Variable variable)
        {
            return IsEmpty ||
                variable.Address.CanMerge(First.Address, MaxAddressRange);
        }

        public bool NeedMerge(Variable variable)
        {
            var result = (!variable.Item.ArrayLength.HasValue) ||
                (variable.Item.ArrayLength.HasValue && variable.Item.ArrayLength <= MaxArrayLength);

            return result;
        }

        public unsafe override void ReadSet(OpResult value)
        {
            base.ReadSet(value);
            if (value.IsSuccess)
            {
                byte[] data = value.GetContent() as byte[];
                fixed (byte* p0 = data)
                {
                    foreach (var v in _variables)
                    {
                        object itemValue = default(object);
                        var offset = (v.Address.MainAddress - First.Address.MainAddress) * Item.DataType.ByteSize();
                        var p = p0 + offset;
                        switch (v.Item.DataType)
                        {
                            case DataType.Bit:
                                {
                                    offset = (v.Address.MainAddress - First.Address.MainAddress) * 16;
                                    offset += v.Address.SubAddress ?? 0;
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        var array = new bool[v.Item.ArrayLength.Value];
                                        for (int i = offset; i < offset + v.Item.ArrayLength.Value; i++)
                                        {
                                            array[i - offset] = data[i] > 0 ? true : false;
                                        }
                                    }
                                    else
                                    {
                                        itemValue = data[offset] > 0 ? true : false;
                                    }

                                    break;
                                }

                            case DataType.Int16:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(2, offset, v.Item.ArrayLength.Value, x => *((short*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(2, offset, x => *((short*)x.ToPointer()));
                                    }

                                    break;
                                }

                            case DataType.UInt16:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(2, offset, v.Item.ArrayLength.Value, x => *((ushort*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(2, offset, x => *((ushort*)x.ToPointer()));
                                    }

                                    break;
                                }

                            case DataType.Int32:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(4, offset, v.Item.ArrayLength.Value, x => *((int*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(4, offset, x => *((int*)x.ToPointer()));
                                    }

                                    break;
                                }

                            case DataType.UInt32:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(4, offset, v.Item.ArrayLength.Value, x => *((uint*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(4, offset, x => *((uint*)x.ToPointer()));
                                    }

                                    break;
                                }

                            case DataType.Int64:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(8, offset, v.Item.ArrayLength.Value, x => *((long*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(8, offset, x => *((long*)x.ToPointer()));
                                    }

                                    break;
                                }

                            case DataType.UInt64:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(8, offset, v.Item.ArrayLength.Value, x => *((ulong*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(8, offset, x => *((ulong*)x.ToPointer()));
                                    }

                                    break;
                                }

                            case DataType.Single:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(8, offset, v.Item.ArrayLength.Value, x => *((float*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(8, offset, x => *((float*)x.ToPointer()));
                                    }

                                    break;
                                }

                            case DataType.Double:
                                {
                                    if (v.Item.ArrayLength.HasValue)
                                    {
                                        itemValue = data.GetArray(8, offset, v.Item.ArrayLength.Value, x => *((double*)x.ToPointer()));
                                    }
                                    else
                                    {
                                        itemValue = data.GetItem(8, offset, x => *((double*)x.ToPointer()));
                                    }

                                    break;
                                }
                        }

                        v.ReadSet(itemValue, value.IsSuccess, value.Message);
                    }
                }
            }
            else
            {
                foreach (var v in _variables)
                {
                    v.ReadSet(null, false, value.Message);
                }
            }
        }

        public static int Compare(Variable a, Variable b)
        {
            return a.Address.CompareTo(b.Address);
        }
    }
}