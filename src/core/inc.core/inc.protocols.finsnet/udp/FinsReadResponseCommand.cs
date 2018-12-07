using inc.core;
using System;

namespace inc.protocols.finsnet
{
    public class FinsReadResponseCommand : FinsCommand, IDecoder
    {
        public ushort EndCode { get; set; }

        public byte[] Data { get; set; }

        public short[] DataAsInt16Array => GetInt16Array(Data);

        public ushort[] DataAsUInt16Array => GetUInt16Array(Data);

        public int[] DataAsInt32Array => GetInt32Array(Data);

        public uint[] DataAsUInt32Array => GetUInt32Array(Data);

        public long[] DataAsInt64Array => GetInt64Array(Data);

        public ulong[] DataAsUInt64Array => GetUInt64Array(Data);

        public float[] DataAsSingleArray => GetSingleArray(Data);

        public double[] DataAsDoubleArray => GetDoubleArray(Data);

        public bool[] DataAsBooleanArray => GetBooleanArray(Data);

        public unsafe double? DataAsDouble => Data.GetNullableItem(8, 0, x => *((double*)x.ToPointer()));

        public bool? DataAsBoolean => GetBoolean(Data);

        public unsafe int? DataAsInt32 => Data.GetNullableItem(4, 0, x => *((int*)x.ToPointer()));

        public unsafe short? DataAsInt16 => Data.GetNullableItem(2, 0, x => *((short*)x.ToPointer()));

        public unsafe uint? DataAsUInt32 => Data.GetNullableItem(4, 0, x => *((uint*)x.ToPointer()));

        public unsafe ulong? DataAsUInt64 => Data.GetNullableItem(8, 0, x => *((ulong*)x.ToPointer()));

        public unsafe ushort? DataAsUInt16 => Data.GetNullableItem(2, 0, x => *((ushort*)x.ToPointer()));

        public unsafe float? DataAsSingle => Data.GetNullableItem(4, 0, x => *((float*)x.ToPointer()));

        public unsafe bool Decode(byte[] content)
        {
            var result = content.Length >= 2;
            if (result)
            {
                fixed (byte* p0 = content)
                {
                    EndCode = *((ushort*)p0);
                    Data = new byte[content.Length - 2];
                    Array.Copy(content, 2, Data, 0, Data.Length);
                    Data.SwapEvenOdd();
                }
            }

            return result;
        }
    }
}
