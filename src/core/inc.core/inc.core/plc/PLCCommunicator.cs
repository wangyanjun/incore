using System;
using inc.core.communication;

namespace inc.core.plc
{
    /// <summary>
    /// PLC通讯基类
    /// </summary>
    public abstract class PLCCommunicator : IPLCCommunicator
    {
        /// <summary>
        /// 获取是否连接
        /// </summary>
        public bool IsConnected => Status == CommunicationStatus.Connected;

        /// <summary>
        /// 获取通讯状态
        /// </summary>
        public CommunicationStatus Status { get; protected set; } = CommunicationStatus.NotConnected;

        /// <summary>
        /// 获取或设置PLC主机地址
        /// </summary>
        public string PLCHost { get; set; }

        /// <summary>
        /// 获取副本
        /// </summary>
        public abstract IPLCCommunicator Copy { get; }

        /// <summary>
        /// 获取或设置端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Get transport media type
        /// </summary>
        public abstract TransportMedia TransportMedia { get; }

        /// <summary>
        /// Get protocol family
        /// </summary>
        public abstract ProtocolFamily ProtocolFamily { get; }

        /// <summary>
        /// 析构本对象
        /// </summary>
        ~PLCCommunicator()
        {
            Dispose(false);
        }

        /// <summary>
        /// 注销本对象
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 注销本对象
        /// </summary>
        /// <param name="disposing">注销中</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Status != CommunicationStatus.NotConnected)
            {
                Status = CommunicationStatus.Disconnecting;
                CloseConnection();
            }

            Status = CommunicationStatus.Disconnected;
        }

        /// <summary>
        /// 关闭连接具体实现
        /// </summary>
        protected abstract void CloseConnection();

        /// <summary>
        /// 关闭连接
        /// </summary>
        public virtual void Close()
        {
            Dispose();
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <returns>连接结果</returns>
        public OpResult ConnectServer()
        {
            Status = CommunicationStatus.Connecting;
            var result = ConnectServerCore(PLCHost, Port);
            if (!result.IsSuccess)
            {
                Status = CommunicationStatus.NotConnected;
                Console.WriteLine("connect failed:" + result.Message);
            }
            else
            {
                Status = CommunicationStatus.Connected;
            }

            return result;
        }

        /// <summary>
        /// 连接服务器具体实现
        /// </summary>
        /// <param name="plcHost">PLC主机地址</param>
        /// <param name="port">端口号</param>
        /// <returns>连接结果</returns>
        protected abstract OpResult ConnectServerCore(string plcHost, int port);

        /// <summary>
        /// 读取字节数组类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">数据长度</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<byte[]> Read(string address, ushort length);

        /// <summary>
        /// 读取Int16类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<short> ReadInt16(string address);

        /// <summary>
        /// 读取Int32类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<int> ReadInt32(string address);

        /// <summary>
        /// 读取Single类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<float> ReadSingle(string readAddress);

        /// <summary>
        /// 读取Boolean类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<bool> ReadBoolean(string address);

        /// <summary>
        /// 读取UInt64类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<ulong> ReadUInt64(string address);

        /// <summary>
        /// 读取UInt32类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<uint> ReadUInt32(string address);

        /// <summary>
        /// 读取UInt16类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        public abstract OpResult<ushort> ReadUInt16(string address);

        public abstract OpResult<double> ReadDouble(string address);

        public abstract OpResult Write(string address, bool value);

        public abstract OpResult Write(string address, short value);

        public abstract OpResult Write(string address, ushort value);

        public abstract OpResult Write(string address, uint value);

        public abstract OpResult Write(string address, int value);

        public abstract OpResult Write(string address, long value);

        public abstract OpResult Write(string address, ulong value);

        public abstract OpResult Write(string address, float value);

        public abstract OpResult Write(string address, double value);

        public abstract OpResult Write(string address, byte value);

        public abstract OpResult<short[]> ReadInt16(string address, int count);

        public abstract OpResult<ushort[]> ReadUInt16(string readAddress, int value);

        public abstract OpResult<int[]> ReadInt32(string readAddress, int value);

        public abstract OpResult<float[]> ReadSingle(string readAddress, int value);

        public abstract OpResult<double[]> ReadDouble(string readAddress, int value);

        public abstract OpResult<bool[]> ReadBoolean(string readAddress, int value);

        public abstract OpResult<ulong[]> ReadUInt64(string readAddress, int value);

        public abstract OpResult<uint[]> ReadUInt32(string readAddress, int value);

        public abstract OpResult Write(string address, bool[] value);

        public abstract OpResult Write(string address, short[] value);

        public abstract OpResult Write(string address, byte[] value);

        public abstract OpResult Write(string address, ushort[] value);

        public abstract OpResult Write(string address, uint[] value);

        public abstract OpResult Write(string address, int[] value);

        public abstract OpResult Write(string address, long[] value);

        public abstract OpResult Write(string address, float[] value);

        public abstract OpResult Write(string address, double[] value);

        public abstract OpResult Write(string address, ulong[] value);

        public abstract OpResult<byte[]> ReadUInt16Bytes(string address, int count);

        public abstract OpResult<byte[]> ReadBooleanBytes(string address, int count);
    }
}
