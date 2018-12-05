using inc.core;

namespace inc.protocols.finsnet
{
    public class FinsWriteResponseCommand : FinsCommand, IDecoder
    {
        public ushort EndCode { get; set; }

        public unsafe bool Decode(byte[] content)
        {
            var result = content.Length >= 2;
            if (result)
            {
                fixed (byte* p0 = content)
                {
                    EndCode = *((ushort*)p0);
                }
            }

            return result;
        }
    }
}
