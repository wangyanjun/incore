using System;

namespace inc.core.plc
{
    public interface IAddress : IComparable<IAddress>
    {
        int MainAddress { get; set; }

        int? SubAddress { get; set; }

        string Address { get; set; }

        IAddress Copy { get; }

        void CopyTo(IAddress address);

        bool Parse(string address);

        string ComputeAddress();

        bool CanMerge(IAddress address, int maxRange);

        void Merge(IAddress to, VariableItem item, int? lastArrayLength);
    }

    public abstract class AddressBase : IAddress
    {
        public int MainAddress { get; set; }

        public int? SubAddress { get; set; }

        public string Address { get; set; }

        public abstract IAddress Copy { get; }

        public abstract bool CanMerge(IAddress address, int maxRange);

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

        public abstract string ComputeAddress();

        public virtual void CopyTo(IAddress address)
        {
            MainAddress = address.MainAddress;
            SubAddress = address.SubAddress;
            Address = address.Address;
        }

        public abstract void Merge(IAddress to, VariableItem item, int? lastArrayLength);

        public abstract bool Parse(string address);
    }
}
