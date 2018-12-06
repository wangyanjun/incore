using inc.core;

namespace inc.protocols.finsnet
{
    public class FinsCommand
    {
        public unsafe bool? GetBoolean(byte[] content)
        {
            bool? value = default(bool?);
            if ((content != null) && content.Length >= 1)
            {
                value = content[0] != 0;
            }

            return value;
        }

        protected unsafe void Swap(byte* p, int index, int count)
        {
            byte mid = 0;
            for (int i = 0; i < count; i += 2)
            {
                mid = p[i];
                p[i] = p[i + 1];
                p[i + 1] = mid;
            }
        }

        public unsafe bool[] GetBooleanArray(byte[] content)
        {
            var result = new bool[0];
            if ((content != null) && content.Length >= 8)
            {
                result = new bool[content.Length];
                for (int i = 0; i < content.Length; i++)
                {
                    result[i] = content[i] != 0;
                }
            }

            return result;
        }

        public unsafe int[] GetInt32Array(byte[] content)
        {
            return content.GetArray(4, x => *((int*)x.ToPointer()));
        }

        public unsafe float[] GetSingleArray(byte[] content)
        {
            return content.GetArray(4, x => *((float*)x.ToPointer()));
        }

        public unsafe double[] GetDoubleArray(byte[] content)
        {
            return content.GetArray(8, x => *((double*)x.ToPointer()));
        }

        public unsafe long[] GetInt64Array(byte[] content)
        {
            return content.GetArray(8, x => *((long*)x.ToPointer()));
        }

        public unsafe ulong[] GetUInt64Array(byte[] content)
        {
            return content.GetArray(8, x => *((ulong*)x.ToPointer()));
        }

        public unsafe ushort[] GetUInt16Array(byte[] content)
        {
            return content.GetArray(2, x => *((ushort*)x.ToPointer()));
        }

        public unsafe short[] GetInt16Array(byte[] content)
        {
            return content.GetArray(2, x => *((short*)x.ToPointer()));
        }

        public unsafe uint[] GetUInt32Array(byte[] content)
        {
            return content.GetArray(4, x => *((uint*)x.ToPointer()));
        }
    }
}
