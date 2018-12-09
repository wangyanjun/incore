using System;

namespace inc.core.plc
{
    /// <summary>
    /// Address interface
    /// </summary>
    public interface IAddress : IComparable<IAddress>
    {
        /// <summary>
        /// Get or set main address
        /// </summary>
        int MainAddress { get; set; }

        /// <summary>
        /// Get or set sub address
        /// </summary>
        int? SubAddress { get; set; }

        /// <summary>
        /// Get or set literal address
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Get copy of this address
        /// </summary>
        IAddress Copy { get; }

        /// <summary>
        /// Copy address to destinate address
        /// </summary>
        /// <param name="address">Destinate address</param>
        void CopyTo(IAddress address);

        /// <summary>
        /// Parse address
        /// </summary>
        /// <param name="address">the address to be parsed</param>
        /// <returns>Parse result</returns>
        bool Parse(string address);

        /// <summary>
        /// Compute literal address
        /// </summary>
        /// <returns>Compute result</returns>
        string ComputeAddress();

        /// <summary>
        /// Determine can be merge with specified address
        /// </summary>
        /// <param name="address">Specified address</param>
        /// <param name="maxRange">Address range</param>
        /// <returns>Determine result</returns>
        bool CanMerge(IAddress address, int maxRange);

        /// <summary>
        /// Merge with specified address
        /// </summary>
        /// <param name="to">Specified to be merged with address</param>
        /// <param name="item">Variable item</param>
        /// <param name="lastArrayLength">Array length</param>
        void Merge(IAddress to, VariableItem item, int? lastArrayLength);
    }

    /// <summary>
    /// The address base class
    /// </summary>
    public abstract class AddressBase : IAddress
    {
        /// <summary>
        /// Get or set main address
        /// </summary>
        public int MainAddress { get; set; }

        /// <summary>
        /// Get or set sub address
        /// </summary>
        public int? SubAddress { get; set; }
        
        /// <summary>
        /// Get or set literal address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Get copy of this address
        /// </summary>
        public abstract IAddress Copy { get; }

        /// <summary>
        /// Determine can be merge with specified address
        /// </summary>
        /// <param name="address">Specified address</param>
        /// <param name="maxRange">Address range</param>
        /// <returns>Determine result</returns>
        public abstract bool CanMerge(IAddress address, int maxRange);

        /// <summary>
        /// Compare with other address
        /// </summary>
        /// <param name="other">Other address</param>
        /// <returns>Compare result</returns>
        public virtual int CompareTo(IAddress other)
        {
            if (other == null) return 1;
            var result = MainAddress.CompareTo(other.MainAddress);
            if (result == 0)
            {
                if (!(!SubAddress.HasValue && !other.SubAddress.HasValue))
                {
                    if (SubAddress.HasValue) result = 1;
                    if (other.SubAddress.HasValue) result = -1;
                    result = SubAddress.Value.CompareTo(other.SubAddress.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// Compute literal address
        /// </summary>
        /// <returns>Compute result</returns>
        public abstract string ComputeAddress();

        /// <summary>
        /// Copy address to destinate address
        /// </summary>
        /// <param name="address">Destinate address</param>
        public virtual void CopyTo(IAddress address)
        {
            MainAddress = address.MainAddress;
            SubAddress = address.SubAddress;
            Address = address.Address;
        }
        
        /// <summary>
        /// Merge with specified address
        /// </summary>
        /// <param name="to">Specified to be merged with address</param>
        /// <param name="item">Variable item</param>
        /// <param name="lastArrayLength">Array length</param>
        public abstract void Merge(IAddress to, VariableItem item, int? lastArrayLength);

        /// <summary>
        /// Parse address
        /// </summary>
        /// <param name="address">the address to be parsed</param>
        /// <returns>Parse result</returns>
        public abstract bool Parse(string address);
    }
}
