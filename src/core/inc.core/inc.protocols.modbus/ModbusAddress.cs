using inc.core.plc;

namespace inc.protocols.modbus
{
    /// <summary>
    /// Class represent for modbus address.
    /// </summary>
    public class ModbusAddress : AddressBase
    {
        /// <summary>
        /// Get copy of this address
        /// </summary>
        public override IAddress Copy
        {
            get
            {
                var result= new ModbusAddress();
                CopyTo(result);
                return result;
            }
        }

        public override bool CanMerge(IAddress address, int maxRange)
        {
            throw new System.NotImplementedException();
        }

        public override string ComputeAddress()
        {
            throw new System.NotImplementedException();
        }

        public override void Merge(IAddress to, VariableItem item, int? lastArrayLength)
        {
            throw new System.NotImplementedException();
        }

        public override bool Parse(string address)
        {
            throw new System.NotImplementedException();
        }
    }
}
