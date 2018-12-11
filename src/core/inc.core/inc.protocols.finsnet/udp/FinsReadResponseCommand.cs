using inc.core;
using System;

namespace inc.protocols.finsnet
{
    /// <summary>
    /// Class represent finsnet read repsonse
    /// </summary>
    public class FinsReadResponseCommand : FinsCommand, IDecoder
    {
        /// <summary>
        /// Get or set end code
        /// </summary>
        public ushort EndCode { get; set; }

        /// <summary>
        /// Get or set response data.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Data as int16 array
        /// </summary>
        public short[] DataAsInt16Array => GetInt16Array(Data);

        /// <summary>
        /// Data as uint16 array
        /// </summary>
        public ushort[] DataAsUInt16Array => GetUInt16Array(Data);

        /// <summary>
        /// Data as int32 array
        /// </summary>
        public int[] DataAsInt32Array => GetInt32Array(Data);

        /// <summary>
        /// Data as uint32 array
        /// </summary>
        public uint[] DataAsUInt32Array => GetUInt32Array(Data);

        /// <summary>
        /// Data as int64 array
        /// </summary>
        public long[] DataAsInt64Array => GetInt64Array(Data);

        /// <summary>
        /// Data as uint64 array
        /// </summary>
        public ulong[] DataAsUInt64Array => GetUInt64Array(Data);

        /// <summary>
        /// Data as float array
        /// </summary>
        public float[] DataAsSingleArray => GetSingleArray(Data);

        /// <summary>
        /// Data as double array
        /// </summary>
        public double[] DataAsDoubleArray => GetDoubleArray(Data);

        /// <summary>
        /// Data as boolean array
        /// </summary>
        public bool[] DataAsBooleanArray => GetBooleanArray(Data);

        /// <summary>
        /// Data as double
        /// </summary>
        public unsafe double? DataAsDouble => Data.GetNullableItem(8, 0, x => *((double*)x.ToPointer()));

        /// <summary>
        /// Data as boolean
        /// </summary>
        public bool? DataAsBoolean => GetBoolean(Data);

        /// <summary>
        /// Data as int32
        /// </summary>
        public unsafe int? DataAsInt32 => Data.GetNullableItem(4, 0, x => *((int*)x.ToPointer()));

        /// <summary>
        /// Data as int16
        /// </summary>
        public unsafe short? DataAsInt16 => Data.GetNullableItem(2, 0, x => *((short*)x.ToPointer()));

        /// <summary>
        /// Data as uint32
        /// </summary>
        public unsafe uint? DataAsUInt32 => Data.GetNullableItem(4, 0, x => *((uint*)x.ToPointer()));

        /// <summary>
        /// Data as uint64
        /// </summary>
        public unsafe ulong? DataAsUInt64 => Data.GetNullableItem(8, 0, x => *((ulong*)x.ToPointer()));

        /// <summary>
        /// Data as uint16
        /// </summary>
        public unsafe ushort? DataAsUInt16 => Data.GetNullableItem(2, 0, x => *((ushort*)x.ToPointer()));

        /// <summary>
        /// Data as float
        /// </summary>
        public unsafe float? DataAsSingle => Data.GetNullableItem(4, 0, x => *((float*)x.ToPointer()));

        /// <summary>
        /// Decode data
        /// </summary>
        /// <param name="content">data content</param>
        /// <returns>Decode result</returns>
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
