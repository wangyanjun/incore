using inc.core.communication;
using System;

namespace inc.core.plc
{
    /// <summary>
    /// PLC Communication interface
    /// </summary>
    public interface IPLCCommunicator : IDisposable
    {
        /// <summary>
        /// Get address mapping
        /// </summary>
        IAddress AddressMapping { get; }

        /// <summary>
        /// Get transport media type
        /// </summary>
        TransportMedia TransportMedia { get; }

        /// <summary>
        /// Get protocol family
        /// </summary>
        ProtocolFamily ProtocolFamily { get; }

        /// <summary>
        /// 获取副本
        /// </summary>
        IPLCCommunicator Copy { get; }

        /// <summary>
        /// Get whether connected
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Get communication status
        /// </summary>
        CommunicationStatus Status { get; }

        /// <summary>
        /// Get or set plc host
        /// </summary>
        string PLCHost { get; set; }

        /// <summary>
        /// Get or set plc port
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Connect to plc server
        /// </summary>
        /// <returns>Connect result</returns>
        OpResult ConnectServer();

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <param name="address">Starting address</param>
        /// <param name="length">Data length</param>
        /// <returns>Read result</returns>
        OpResult<byte[]> Read(string address, ushort length);

        /// <summary>
        /// Read Int16 data
        /// </summary>
        /// <param name="address">Starting address</param>
        /// <returns>Read result</returns>
        OpResult<short> ReadInt16(string address);

        /// <summary>
        /// Read int 16 array
        /// </summary>
        /// <param name="address">Starting address</param>
        /// <param name="count">Read count</param>
        /// <returns>Read result</returns>
        OpResult<short[]> ReadInt16(string address, int count);

        /// <summary>
        /// 读取Int16类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<ushort> ReadUInt16(string address);

        /// <summary>
        /// 读取Int32类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<int> ReadInt32(string address);

        /// <summary>
        /// 写Boolean数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, bool value);

        /// <summary>
        /// 写Int16数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, short value);

        /// <summary>
        /// 写Int16数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, byte value);

        /// <summary>
        /// 写UInt16数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, ushort value);

        /// <summary>
        /// 写UInt32数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, uint value);

        /// <summary>
        /// 写Int32数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, int value);

        /// <summary>
        /// 写Int64数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, long value);

        /// <summary>
        /// 写Single数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, float value);

        /// <summary>
        /// 写Double数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, double value);

        /// <summary>
        /// 写UInt64数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, ulong value);

        /// <summary>
        /// 读取UInt32类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<uint> ReadUInt32(string address);

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

        /// <summary>
        /// 读取Single类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<float> ReadSingle(string address);

        /// <summary>
        /// 读取Single类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<bool> ReadBoolean(string address);

        /// <summary>
        /// 读取UInt64类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<ulong> ReadUInt64(string address);

        /// <summary>
        /// 读取Double类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<double> ReadDouble(string address);

        OpResult<ushort[]> ReadUInt16(string address, int count);

        OpResult<int[]> ReadInt32(string address, int count);

        OpResult<float[]> ReadSingle(string address, int count);

        OpResult<double[]> ReadDouble(string address, int count);

        OpResult<bool[]> ReadBoolean(string address, int count);

        OpResult<ulong[]> ReadUInt64(string address, int count);

        OpResult<uint[]> ReadUInt32(string address, int count);

        /// <summary>
        /// 写Boolean数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, bool[] value);

        /// <summary>
        /// 写Int16数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, short[] value);

        /// <summary>
        /// 写Int16数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, byte[] value);

        /// <summary>
        /// 写UInt16数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, ushort[] value);

        /// <summary>
        /// 写UInt32数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, uint[] value);

        /// <summary>
        /// 写Int32数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, int[] value);

        /// <summary>
        /// 写Int64数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, long[] value);

        /// <summary>
        /// 写Single数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, float[] value);

        /// <summary>
        /// 写Double数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, double[] value);

        /// <summary>
        /// 写UInt64数据类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns>写入结果</returns>
        OpResult Write(string address, ulong[] value);

        OpResult<byte[]> ReadUInt16Bytes(string address, int count);

        OpResult<byte[]> ReadBooleanBytes(string address, int count);
    }
}
