using inc.core;
using inc.core.communication;
using inc.core.plc;
using System;

namespace inc.protocols.finsnet.udp
{
    /// <summary>
    /// 模拟的FinsNet通讯对象
    /// </summary>
    public class MockFinsNetCommunicator : PLCCommunicator
    {
        private int intValue = 0;

        private byte[] _memory = new byte[4096];

        public override IPLCCommunicator Copy => new MockFinsNetCommunicator();

        public override TransportMedia TransportMedia => TransportMedia.Memory;

        public override ProtocolFamily ProtocolFamily => ProtocolFamily.FinsNet;

        public override IAddress AddressMapping => throw new NotImplementedException();

        /// <summary>
        /// 读取字节数组类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">数据长度</param>
        /// <returns>读取结果</returns>
        public override OpResult<byte[]> Read(string address, ushort length)
        {
            var addr = GetAddress(address, length);
            byte[] content = new byte[length];
            Array.Copy(_memory, content, length);
            var result = new OpResult<byte[]>()
            {
                IsSuccess = true,
                Content = content
            };

            return result;
        }

        public unsafe override OpResult<bool> ReadBoolean(string address)
        {
            var len = 1;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<bool>();
            fixed (byte* p0 = content)
            {
                result.Content = (*((byte*)p0)) > 0;
            }

            return result;
        }

        public override OpResult<bool[]> ReadBoolean(string readAddress, int value)
        {
            throw new NotImplementedException();
        }

        public unsafe override OpResult<double> ReadDouble(string address)
        {
            var len = 8;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<double>();
            fixed (byte* p0 = content)
            {
                result.Content = *((double*)p0) + intValue;
            }

            intValue++;
            return result;
        }

        public override OpResult<double[]> ReadDouble(string readAddress, int value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 读取Int16类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public unsafe override OpResult<short> ReadInt16(string address)
        {
            var len = 2;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<short>() { IsSuccess = true };
            fixed (byte* p0 = content)
            {
                result.Content = *((short*)p0);
            }

            return result;
        }

        public override OpResult<short[]> ReadInt16(string address, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 读取Int32类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public unsafe override OpResult<int> ReadInt32(string address)
        {
            var len = 4;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<int>() { IsSuccess = true };
            fixed (byte* p0 = content)
            {
                result.Content = *((int*)p0) + intValue;
            }

            intValue++;
            return result;
        }

        public override OpResult<int[]> ReadInt32(string readAddress, int value)
        {
            throw new NotImplementedException();
        }

        public unsafe override OpResult<float> ReadSingle(string address)
        {
            var len = 4;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<float>();
            fixed (byte* p0 = content)
            {
                result.Content = *((int*)p0) + intValue;
            }

            intValue++;
            return result;
        }

        public override OpResult<float[]> ReadSingle(string readAddress, int value)
        {
            throw new NotImplementedException();
        }

        public unsafe override OpResult<ushort> ReadUInt16(string address)
        {
            var len = 2;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<ushort>();
            fixed (byte* p0 = content)
            {
                result.Content = *((ushort*)p0);
            }

            return result;
        }

        public override OpResult<ushort[]> ReadUInt16(string readAddress, int value)
        {
            throw new NotImplementedException();
        }

        public unsafe override OpResult<uint> ReadUInt32(string address)
        {
            var len = 4;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<uint>();
            fixed (byte* p0 = content)
            {
                result.Content = *((uint*)p0) + (uint)intValue;
            }

            intValue++;
            return result;
        }

        public override OpResult<uint[]> ReadUInt32(string readAddress, int value)
        {
            throw new NotImplementedException();
        }

        public unsafe override OpResult<ulong> ReadUInt64(string address)
        {
            var len = 8;
            var addr = GetAddress(address, len);
            byte[] content = new byte[len];
            Array.Copy(_memory, content, len);
            var result = new OpResult<ulong>();
            fixed (byte* p0 = content)
            {
                result.Content = *((ulong*)p0) + (ulong)intValue;
            }

            intValue++;
            return result;
        }

        public override OpResult<ulong[]> ReadUInt64(string readAddress, int value)
        {
            throw new NotImplementedException();
        }

        public override OpResult<byte[]> ReadUInt16Bytes(string address, int count)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, bool value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, short value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, ushort value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, uint value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, int value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, long value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, ulong value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, float value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, double value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, byte value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, bool[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, short[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, byte[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, ushort[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, uint[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, int[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, long[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, float[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, double[] value)
        {
            throw new NotImplementedException();
        }

        public override OpResult Write(string address, ulong[] value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 关闭连接具体实现
        /// </summary>
        protected override void CloseConnection()
        {
        }

        /// <summary>
        /// 连接服务器具体实现
        /// </summary>
        /// <param name="plcHost">PLC主机地址</param>
        /// <param name="port">端口号</param>
        /// <returns>连接结果</returns>
        protected override OpResult ConnectServerCore(string plcHost, int port)
        {
            return new OpResult();
        }

        private int GetAddress(string address, int length)
        {
            var result = 0;
            if (result >= _memory.Length)
            {
                Array.Resize(ref _memory, result + length);
            }

            return result;
        }

        public override OpResult<byte[]> ReadBooleanBytes(string address, int count)
        {
            throw new NotImplementedException();
        }
    }
}
