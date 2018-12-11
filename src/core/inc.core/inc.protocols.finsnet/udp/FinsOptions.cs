using System;

namespace inc.protocols.finsnet
{
    /// <summary>
    /// Finsnet options
    /// </summary>
    public class FinsOptions
    {
        public byte ICF { get; set; } = 0x80;

        public byte RSV { get; set; } = 0x00;

        public byte GCT { get; set; } = 0x02;

        public byte DNA { get; set; }

        /// <summary>
        /// Get or set last byte of PLC host address
        /// </summary>
        public byte DA1 { get; set; }

        /// <summary>
        /// Get or set PLC unit no.
        /// </summary>
        public byte DA2 { get; set; }

        public byte SNA { get; set; }

        /// <summary>
        /// Get or set last byte of PC address
        /// </summary>
        public byte SA1 { get; set; }

        public byte SA2 { get; set; }

        /// <summary>
        /// Get or set service id
        /// </summary>
        public byte SID { get; set; }

        /// <summary>
        /// Copy info to destinate
        /// </summary>
        /// <param name="destinate">Destinate</param>
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
