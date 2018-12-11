using System;

namespace inc.protocols.mc
{
    public class Format1RequestFrame : IEncoder
    {
        public byte ControlCode { get; set; }

        public byte FrameId { get; set; }

        public byte AccessRoute { get; set; }

        public byte[] RequestData { get; set; }

        public byte CheckSum { get; set; }

        public byte[] Encode()
        {
            int dataLength = RequestData?.Length ?? 0;
            byte[] result = new byte[4 + dataLength];
            result[0] = ControlCode;
            result[1] = FrameId;
            result[2] = AccessRoute;
            result[result.Length - 1] = CheckSum;
            if (dataLength > 0)
            {
                Array.Copy(RequestData, 0, result, 3, dataLength);
            }

            return result;
        }
    }
}
