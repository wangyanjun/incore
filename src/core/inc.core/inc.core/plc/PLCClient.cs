using System;
using System.Collections.Generic;
using System.Threading;

namespace inc.core.plc
{
    /// <summary>
    /// PLC设备
    /// </summary>
    public class PLCClient : IDisposable
    {
        private readonly SortedDictionary<string, ReadLoop> _specialCommunicators
            = new SortedDictionary<string, ReadLoop>(StringComparer.OrdinalIgnoreCase);

        private readonly SortedDictionary<string, Variable> _tempVariables 
            = new SortedDictionary<string, Variable>(StringComparer.OrdinalIgnoreCase);

        public const string DefaultName = "DefultPLC";

        private const string DefaultThread = "DefaultThread";

        public const double DefaultRefreshSpanInSeconds = 0.05;

        private readonly object SendLock = new object();

        /// <summary>
        /// 获取是否退出
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 获取读取间隔，单位秒
        /// </summary>
        public double ReadSpanInSeconds { get; set; } = 1;

        /// <summary>
        /// 获取通讯器
        /// </summary>
        public IPLCCommunicator Communicator { get; set; }

        /// <summary>
        /// 获取设备变量
        /// </summary>
        public VariableCollection Variables { get; } = new VariableCollection();

        /// <summary>
        /// 获取或设置是否是默认的设备
        /// </summary>
        public bool IsDefault { get; set; } = true;

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; } = DefaultName;

        /// <summary>
        /// 开启
        /// </summary>
        public void Start()
        {
            if (Communicator == null)
            {
                throw new InvalidOperationException($"{nameof(Communicator)} not set");
            }

            foreach (var va in Variables)
            {
                var thread = va.Value.Item.SpecialThreadToken;
                if (string.IsNullOrWhiteSpace(thread))
                {
                    thread = DefaultThread;
                }

                if (!_specialCommunicators.ContainsKey(thread))
                {
                    var comm = Communicator.Copy;
                    var loop = new ReadLoop(this, comm, thread);
                    _specialCommunicators[thread] = loop;
                }

                _specialCommunicators[thread].Add(va.Value);
            }

            foreach (var kv in _specialCommunicators)
            {
                kv.Value.Start();
            }
        }

        /// <summary>
        /// 获取或设置是否暂停
        /// </summary>
        public bool Pause { get; set; }

        /// <summary>
        /// 判断名称是否匹配
        /// </summary>
        /// <param name="name">设备名称</param>
        /// <returns>是否匹配</returns>
        public bool Match(string name)
        {
            if (string.Equals(name, Name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.IsNullOrEmpty(name) && IsDefault)
            {
                return true;
            }

            return false;
        }

        public OpResult Write(Variable variable, object value)
        {
            var result = new OpResult();
            if (result.CheckNotNull(variable, SR.ArgumentIsNullOrEmpty) && result.CheckNotNull(value, SR.ArgumentIsNullOrEmpty))
            {

                var communicator = variable.Communicator;
                var item = variable.Item;
                if (value is IConvertible conv)
                {
                    switch (item.DataType)
                    {
                        case DataType.Bit:
                            {
                                result = communicator.Write(item.WriteAddress, (bool)value);
                                break;
                            }

                        case DataType.Byte:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToByte(null));
                                break;
                            }

                        case DataType.Int16:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToInt16(null));
                                break;
                            }

                        case DataType.UInt16:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToUInt16(null));
                                break;
                            }

                        case DataType.Int32:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToInt32(null));
                                break;
                            }

                        case DataType.UInt32:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToUInt32(null));
                                break;
                            }

                        case DataType.Int64:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToInt64(null));
                                break;
                            }

                        case DataType.UInt64:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToUInt64(null));
                                break;
                            }

                        case DataType.Single:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToSingle(null));
                                break;
                            }

                        case DataType.Double:
                            {
                                result = communicator.Write(item.WriteAddress, conv.ToDouble(null));
                                break;
                            }
                    }
                }
                else
                {
                    switch (item.DataType)
                    {
                        case DataType.Bit:
                            {
                                var values = (value as bool[]) ?? value.ToArray<bool>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.Byte:
                            {
                                var values = (value as byte[]) ?? value.ToArray<byte>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.Double:
                            {
                                var values = (value as double[]) ?? value.ToArray<double>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }
                                break;
                            }

                        case DataType.Int16:
                            {
                                var values = (value as short[]) ?? value.ToArray<short>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.Int32:
                            {
                                var values = (value as int[]) ?? value.ToArray<int>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.Int64:
                            {
                                var values = (value as long[]) ?? value.ToArray<long>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.Single:
                            {
                                var values = (value as float[]) ?? value.ToArray<float>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.UInt16:
                            {
                                var values = (value as ushort[]) ?? value.ToArray<ushort>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.UInt32:
                            {
                                var values = (value as uint[]) ?? value.ToArray<uint>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }

                        case DataType.UInt64:
                            {
                                var values = (value as ulong[]) ?? value.ToArray<ulong>();
                                if (result.CheckNotNull(values, SR.InvalidParameter))
                                {
                                    result = communicator.Write(item.WriteAddress, values);
                                }

                                break;
                            }
                    }
                }
            }

            return result;
        }

        private ReadLoop GetReadLoop(Variable variable)
        {
            var key = string.IsNullOrWhiteSpace(variable.Item.SpecialThreadToken) ? DefaultThread : variable.Item.SpecialThreadToken;
            var result = default(ReadLoop);
            if (_specialCommunicators.ContainsKey(key))
            {
                result = _specialCommunicators[key];
            }

            return result;
        }

        /// <summary>
        /// 强制写入
        /// </summary>
        /// <param name="variable">要写入的参数</param>
        /// <param name="value">要写入的值</param>
        /// <returns>写入结果</returns>
        public OpResult ForceWrite(Variable variable, object value)
        {
            var op = new OpResult();
            lock (SendLock)
            {
                op = Write(variable, value);
            }

            return op;
        }

        /// <summary>
        /// 强制读取
        /// </summary>
        /// <param name="variable">要读取的参数</param>
        /// <returns>读取结果</returns>
        public OpResult ForceRead(Variable variable)
        {
            var op = new OpResult();
            lock (SendLock)
            {
                op = ReadVariable(variable);
            }

            return op;
        }

        /// <summary>
        /// 读取变量
        /// </summary>
        /// <param name="variable">要读取的变量</param>
        protected virtual OpResult ReadVariable(Variable variable)
        {
            var item = variable.Item;
            var communicator = variable.Communicator;
            var r = default(OpResult);
            switch (item.DataType)
            {
                case DataType.Int16:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadInt16(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadInt16(item.ReadAddress);
                        }

                        break;
                    }

                case DataType.UInt16Bytes:
                    {
                        r = communicator.ReadUInt16Bytes(item.ReadAddress, item.ArrayLength ?? 1);
                        break;
                    }

                case DataType.BooleanBytes:
                    {
                        r = communicator.ReadBooleanBytes(item.ReadAddress, item.ArrayLength ?? 1);
                        break;
                    }

                case DataType.UInt16:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadUInt16(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadUInt16(item.ReadAddress);
                        }

                        break;
                    }

                case DataType.Int32:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadInt32(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadInt32(item.ReadAddress);
                        }

                        break;
                    }

                case DataType.Single:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadSingle(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadSingle(item.ReadAddress);
                        }

                        break;
                    }

                case DataType.Double:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadDouble(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadDouble(item.ReadAddress);
                        }

                        break;
                    }

                case DataType.Bit:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadBoolean(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadBoolean(item.ReadAddress);
                        }

                        break;
                    }

                case DataType.UInt64:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadUInt64(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadUInt64(item.ReadAddress);
                        }

                        break;
                    }

                case DataType.UInt32:
                    {
                        if (item.ArrayLength.HasValue)
                        {
                            r = communicator.ReadUInt32(item.ReadAddress, item.ArrayLength.Value);
                        }
                        else
                        {
                            r = communicator.ReadUInt32(item.ReadAddress);
                        }

                        break;
                    }

                default:
                    {
                        throw new NotSupportedException($"data type [{item.DataType}] not supported");
                    }
            }

            variable.ReadSet(r);
            return r;
        }

        /// <summary>
        /// Read boolean
        /// </summary>
        /// <param name="address">address</param>
        /// <returns>Read result</returns>
        public OpResult<bool> ReadBoolean(string address)
        {
            return Communicator.ReadBoolean(address);
        }

        /// <summary>
        /// Read uint16 data
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public OpResult<ushort> ReadUInt16(string address)
        {
            return Communicator.ReadUInt16(address);
        }

        public OpResult<ushort[]> ReadUInt16Array(string address,int count)
        {
            return Communicator.ReadUInt16(address, count);
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
            if (!IsDisposed)
            {
                IsDisposed = true;
                foreach (var com in _specialCommunicators)
                {
                    com.Value.Dispose();
                }

                _specialCommunicators.Clear();
                if (Communicator != null)
                {
                    Communicator.Dispose();
                    Communicator = null;
                }
            }
        }

        public class ReadLoop : IDisposable
        {
            private Thread _loopThread;

            private ManualResetEvent _mre = new ManualResetEvent(true);

            private bool _isDisposed;

            public string ThreadName { get; private set; }

            public ReadLoop(PLCClient pLCDevice, IPLCCommunicator comm, string thread)
            {
                PLC = pLCDevice;
                Communicator = comm;
                ThreadName = thread;
            }

            public bool Reading { get; private set; }

            public bool Merge { get; set; } = true;

            public void Start()
            {
                if (Merge)
                {
                    MergeVariables();
                }

                _loopThread = new Thread(Loop);
                _loopThread.Start();
            }

            private void MergeVariables()
            {
                var list = new List<Variable>();
                foreach (var kv in Variables) list.Add(kv.Value);
                list.Sort(MergedVariable.Compare);
                Variables.Clear();
                var index = 0;
                var v = new MergedVariable(PLC, $"{ThreadName}--{index++}")
                {
                    Communicator = Communicator
                };

                foreach (var x in list)
                {
                    if (!v.Merge(x))
                    {
                        bool noMerge = !v.NeedMerge(x);
                        if (noMerge)
                        {
                            Variables.Add(x);
                        }

                        if (!v.IsEmpty)
                        {
                            v.MergeAddress();
                            Variables.Add(v);
                        }

                        v = new MergedVariable(PLC, $"{ThreadName}--{index++}")
                        {
                            Communicator = Communicator
                        };
                    }
                }

                if (!v.IsEmpty)
                {
                    v.MergeAddress();
                    Variables.Add(v);
                }
            }

            public PLCClient PLC { get; private set; }

            public VariableCollection Variables { get; private set; } = new VariableCollection();

            public IPLCCommunicator Communicator { get; private set; }

            private void MakesureConnection()
            {
                MakesureConnection(Communicator);
            }

            private void MakesureConnection(IPLCCommunicator communicator)
            {
                switch (communicator.Status)
                {
                    case CommunicationStatus.Connected:
                        {
                            break;
                        }

                    case CommunicationStatus.NotConnected:
                        {
                            communicator.ConnectServer();
                            break;
                        }

                    case CommunicationStatus.Failed:
                        {
                            break;
                        }

                    case CommunicationStatus.Connecting:
                    case CommunicationStatus.Disconnecting:
                        {
                            break;
                        }
                }
            }

            private void Loop()
            {
                while (!PLC.IsDisposed)
                {
                    if (Communicator == null)
                    {
                        _mre.SafeWait(100);
                        continue;
                    }

                    MakesureConnection();
                    if (Communicator.IsConnected && (!PLC.IsDisposed))
                    {
                        try
                        {
                            DateTime time = DateTime.Now;
                            ReadAll();
                            var ts = DateTime.Now - time;
                        }
                        catch (Exception ex)
                        {
                            _mre.SafeWait(15);
                            ex.TraceException(nameof(Loop));
                        }
                    }
                }
            }

            /// <summary>
            /// 读取所有
            /// </summary>
            public virtual int ReadAll()
            {
                int count = 0;
                foreach (var kv in Variables)
                {
                    if (PLC.IsDisposed) break;
                    var variable = kv.Value;
                    if (variable.NeedReadNow(variable.Item.ReadSpanInSeconds ?? DefaultRefreshSpanInSeconds))
                    {
                        count++;
                        if (PLC.Pause) _mre.SafeWait();
                        Reading = true;
                        lock (PLC.SendLock)
                        {
                            PLC.ReadVariable(kv.Value);
                        }

                        Reading = false;
                    }
                }

                if (!PLC.IsDisposed)
                {
                    _mre.SafeWait(count == 0 ? 100 : 0);
                }

                return count;
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_isDisposed)
                {
                    _isDisposed = true;
                    _mre?.SafeSet();
                    if (_loopThread != null)
                    {
                        try
                        {
                            _loopThread.Abort();
                        }
                        catch
                        {
                        }

                        _loopThread = null;
                    }

                    if (Communicator != null)
                    {
                        Communicator.Dispose();
                        Communicator = null;
                    }

                    foreach (var kv in Variables)
                    {
                        kv.Value.Communicator = null;
                    }

                    _mre.SafeDispose();
                    _mre = null;
                }
            }

            ~ReadLoop()
            {
                // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void Add(Variable value)
            {
                value.Communicator = Communicator;
                Variables.Add(value);
            }
        }
    }
}
