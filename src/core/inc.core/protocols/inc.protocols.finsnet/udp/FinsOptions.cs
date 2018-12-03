using System;

namespace inc.protocols.finsnet
{
    public class FinsOptions
    {
        public byte ICF { get; set; } = 0x80;

        public byte RSV { get; set; } = 0x00;

        public byte GCT { get; set; } = 0x02;

        public byte DNA { get; set; }

        /// <summary>
        /// 获取或设置PLC地址最后一位
        /// </summary>
        public byte DA1 { get; set; }

        /// <summary>
        /// 获取或设置PLC单元号，一般为0
        /// </summary>
        public byte DA2 { get; set; }

        public byte SNA { get; set; }

        /// <summary>
        /// 获取或设置PC地址最后一位
        /// </summary>
        public byte SA1 { get; set; }

        public byte SA2 { get; set; }

        public byte SID { get; set; }

        public virtual void CopyTo(FinsOptions destinate)
        {
            if (destinate == null)
            {
                throw new ArgumentNullException(nameof(destinate));
            }

            destinate.DA1 = DA1;
            destinate.DA2 = DA2;
            destinate.DNA = DNA;
            destinate.GCT = GCT;
            destinate.ICF = ICF;
            destinate.RSV = RSV;
            destinate.SA1 = SA1;
            destinate.SA2 = SA2;
            destinate.SID = SID;
            destinate.SNA = SNA;
        }
    }
}
