using inc.core;
using inc.core.plc;

namespace inc.protocols.finsnet
{
    /// <summary>
    /// Finsnet memory area
    /// </summary>
    public enum FinsMemoryArea
    {
        NotComputed = 0x00,

        DM = 0x82,

        WR = 0X31
    }

    /// <summary>
    /// FinsMemoryArea helper class
    /// </summary>
    public static class FinsMemoryAreaHelper
    {
        public static int SizeIn(this DataType type, FinsMemoryArea memory)
        {
            var bytes = type.ByteSize();
            var x = 1;
            switch (memory)
            {
                case FinsMemoryArea.DM:
                    {
                        x = 2;
                        break;
                    }
            }

            var result = bytes / x;
            return result;
        }
    }
}
