using inc.core;
using inc.core.communication;
using inc.core.plc;

namespace inc.protocols.modbus
{
    public abstract class ModbusCommunicator: PLCCommunicator
    {
        public IBus Bus { get; set; }

        public override ProtocolFamily ProtocolFamily => ProtocolFamily.Modbus;

        public override IAddress AddressMapping => throw new System.NotImplementedException();

        public override OpResult<byte[]> Read(string address, ushort length)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<bool> ReadBoolean(string address)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<bool[]> ReadBoolean(string readAddress, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<byte[]> ReadBooleanBytes(string address, int count)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<double> ReadDouble(string address)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<double[]> ReadDouble(string readAddress, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<short> ReadInt16(string address)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<short[]> ReadInt16(string address, int count)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<int> ReadInt32(string address)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<int[]> ReadInt32(string readAddress, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<float> ReadSingle(string readAddress)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<float[]> ReadSingle(string readAddress, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<ushort> ReadUInt16(string address)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<ushort[]> ReadUInt16(string readAddress, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<byte[]> ReadUInt16Bytes(string address, int count)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<uint> ReadUInt32(string address)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<uint[]> ReadUInt32(string readAddress, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<ulong> ReadUInt64(string address)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult<ulong[]> ReadUInt64(string readAddress, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, bool value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, short value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, ushort value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, uint value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, int value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, long value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, ulong value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, float value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, double value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, byte value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, bool[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, short[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, byte[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, ushort[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, uint[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, int[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, long[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, float[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, double[] value)
        {
            throw new System.NotImplementedException();
        }

        public override OpResult Write(string address, ulong[] value)
        {
            throw new System.NotImplementedException();
        }

        protected override void CloseConnection()
        {
            throw new System.NotImplementedException();
        }

        protected override OpResult ConnectServerCore(string plcHost, int port)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ModbusTcpCommunicator : ModbusCommunicator
    {
        /// <summary>
        /// Get copy
        /// </summary>
        public override IPLCCommunicator Copy
        {
            get
            {
                var comm = new ModbusTcpCommunicator();
                return comm;
            }
        }

        public override TransportMedia TransportMedia => TransportMedia.Tcp;
    }
}
