using System;

namespace inc.protocols.finsnet
{
    public class FinsFrame : FinsOptions
    {
        private const int FixedHeaderLength = 12;

        /// <summary>
        /// 获取或设置主请求号
        /// </summary>
        public byte MRC { get; set; }

        /// <summary>
        /// 获取或设置次请求号
        /// </summary>
        public byte SRC { get; set; }

        public byte[] Content { get; set; }

        public bool Decode(byte[] content)
        {
            var result = true;
            if (content == null || content.Length < FixedHeaderLength)
            {
                result = false;
            }
            else
            {
                ICF = content[0];
                RSV = content[1];
                GCT = content[2];
                DNA = content[3];
                DA1 = content[4];
                DA2 = content[5];
                SNA = content[6];
                SA1 = content[7];
                SA2 = content[8];
                SID = content[9];
                MRC = content[10];
                SRC = content[11];
                Content = new byte[content.Length - FixedHeaderLength];
                if (content.Length > 0)
                {
                    Array.Copy(content, FixedHeaderLength, Content, 0, Content.Length);
                }
            }

            return result;
        }

        public byte[] Encode()
        {
            var len = FixedHeaderLength;
            if (Content != null)
            {
                len += Content.Length;
            }

            var result = new byte[len];
            result[0] = ICF;
            result[1] = RSV;
            result[2] = GCT;
            result[3] = DNA;
            result[4] = DA1;
            result[5] = DA2;
            result[6] = SNA;
            result[7] = SA1;
            result[8] = SA2;
            result[9] = SID;
            result[10] = MRC;
            result[11] = SRC;
            if (Content != null)
            {
                Array.Copy(Content, 0, result, FixedHeaderLength, Content.Length);
            }

            return result;
        }
    }
}
