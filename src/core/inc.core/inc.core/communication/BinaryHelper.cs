using System;

namespace inc.core
{
    public static class BinaryHelper
    {
        public static unsafe void SwapHighLow(byte* p, int index, int count)
        {
            byte mid = 0;
            for (int i = 0; i < count - 1; i += 2)
            {
                mid = p[i];
                p[i] = p[i + 1];
                p[i + 1] = mid;
            }
        }

        public static unsafe void SwapHighLow(this byte[] content, int index, int count)
        {
            fixed (byte* p = content)
            {
                SwapHighLow(p, index, count);
            }
        }

        public static unsafe void SwapHighLow(this byte[] content) => SwapHighLow(content, 0, content.Length);

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
    }
}
