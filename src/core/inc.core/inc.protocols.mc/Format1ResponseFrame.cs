using inc.core;
using System;

namespace inc.protocols.mc
{
    public class Format1ResponseFrame : IDecoder
    {
        public byte ControlCode { get; set; }

        public byte FrameId { get; set; }

        public byte AccessRoute { get; set; }

        public byte[] ResponseData { get; set; }

        public byte ReturnControlCode { get; set; }

        public byte CheckSum { get; set; }

        public byte ErrorCode { get; set; }

        public bool Decode(byte[] content)
        {
            var result = content != null && content.Length >= 3;
            if (result)
            {
                ControlCode = content[0];
                FrameId = content[1];
                AccessRoute = content[2];
                MCControlCode code = (MCControlCode)ControlCode;
                switch (code)
                {
                    case MCControlCode.STX:
                        {
                            result = content.Length >= 5;
                            if (result)
                            {
                                ReturnControlCode = content[content.Length - 2];
                                CheckSum = content[content.Length - 1];
                                ResponseData = new byte[content.Length - 5];
                                Array.Copy(content, 3, ResponseData, 0, ResponseData.Length);
                            }

                            break;
                        }

                    case MCControlCode.ACK:
                        {
                            break;
                        }

                    case MCControlCode.NCK:
                        {
                            result = false;
                            if (content.Length >= 4)
                            {
                                ErrorCode = content[3];
                            }

                            break;
                        }

                    default:
                        {
                            result = false;
                            break;
                        }
                }
            }

            return result;
        }
    }
}
