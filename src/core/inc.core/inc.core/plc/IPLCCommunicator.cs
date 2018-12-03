using System;

namespace inc.core.plc
{
    /// <summary>
    /// PLC通讯类
    /// </summary>
    public interface IPLCCommunicator : IDisposable
    {
        /// <summary>
        /// 获取副本
        /// </summary>
        IPLCCommunicator Copy { get; }

        /// <summary>
        /// 获取是否连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 获取通讯状态
        /// </summary>
        CommunicationStatus Status { get; }

        /// <summary>
        /// 获取或设置PLC主机地址
        /// </summary>
        string PLCHost { get; set; }

        /// <summary>
        /// 获取或设置端口
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <returns>连接结果</returns>
        OpResult ConnectServer();

        /// <summary>
        /// 读取字节数组类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">数据长度</param>
        /// <returns>读取结果</returns>
        OpResult<byte[]> Read(string address, ushort length);

        /// <summary>
        /// 读取Int16类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>读取结果</returns>
        OpResult<short> ReadInt16(string address);

        /// <summary>
        /// 读取Int16类型数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="count">读取的个数</param>
        /// <returns>读取结果</returns>
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
