using inc.core.plc;
using System;

namespace inc.core
{
    /// <summary>
    /// The helper class for hex data.
    /// </summary>
    public static class HexHelper
    {
        /// <summary>
        /// Swap even odd bytes.
        /// </summary>
        /// <param name="p">The pointer of byte array</param>
        /// <param name="index">start position</param>
        /// <param name="count">swap count</param>
        public static unsafe void SwapEvenOdd(byte* p, int index, int count)
        {
            byte mid = 0;
            int end = index + count - 1;
            for (int i = index; i < end; i += 2)
            {
                mid = p[i];
                p[i] = p[i + 1];
                p[i + 1] = mid;
            }
        }

        /// <summary>
        /// Swap even odd bytes.
        /// </summary>
        /// <param name="p">The pointer of byte array</param>
        /// <param name="count">swap count</param>
        public static unsafe void SwapEvenOdd(byte* p, int count) => SwapEvenOdd(p, 0, count);

        public static unsafe void SwapEvenOdd(this byte[] content, int index, int count)
        {
            fixed (byte* p = content)
            {
                SwapEvenOdd(p, index, count);
            }
        }

        public static unsafe void SwapEvenOdd(this byte[] content) => SwapEvenOdd(content, 0, content.Length);

        public static unsafe T[] GetArray<T>(this byte[] content, int size, Func<IntPtr, T> converter)
         where T : struct
        {
            int count = content == null ? 0 : (content.Length / size);
            return GetArray(content, size, 0, count, converter);
        }

        public static unsafe T GetItem<T>(this byte[] content, int size, int offset, Func<IntPtr, T> converter)
            where T : struct
        {
            T result = default(T);
            if ((content != null) && content.Length >= (offset + size))
            {
                fixed (byte* p0 = content)
                {
                    result = converter(new IntPtr(p0 + offset));
                }
            }

            return result;
        }

        public static unsafe T? GetNullableItem<T>(this byte[] content, int size, int offset, Func<IntPtr, T> converter)
            where T : struct
        {
            T? result = default(T?);
            if ((content != null) && content.Length >= (offset + size))
            {
                fixed (byte* p0 = content)
                {
                    result = converter(new IntPtr(p0 + offset));
                }
            }

            return result;
        }

        public static unsafe T[] GetArray<T>(this byte[] content, int size, int offset, int count, Func<IntPtr, T> converter)
           where T : struct
        {
            T[] result;
            if ((content != null) && content.Length >= (offset + size * count))
            {
                result = new T[count];
                fixed (byte* p0 = content)
                {
                    for (int i = 0; i < count; i++)
                    {
                        result[i] = converter(new IntPtr(p0 + offset + i * size));
                    }
                }
            }
            else
            {
                result = new T[0];
            }

            return result;
        }

        public static int ByteSize(this DataType type)
        {
            switch (type)
            {
                case DataType.Bit:
                case DataType.Byte:
                case DataType.BooleanBytes:
                    {
                        return 1;
                    }

                case DataType.Int16:
                case DataType.UInt16:
                case DataType.UInt16Bytes:
                    {
                        return 2;
                    }

                case DataType.Int32:
                case DataType.UInt32:
                case DataType.Single:
                    {
                        return 4;
                    }

                case DataType.Int64:
                case DataType.UInt64:
                case DataType.Double:
                    {
                        return 8;
                    }

                default:
                    {
                        throw new NotSupportedException("");
                    }
            }
        }       
    }
}
