using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace inc.core.plc
{
    /// <summary>
    /// PLC设备管理器
    /// </summary>
    public class PLCClientManager : IEnumerable<PLCClient>,
        IDisposable
    {
        private readonly List<PLCClient> _devices = new List<PLCClient>();

        private readonly SortedDictionary<string, Variable> _variables = new SortedDictionary<string, Variable>();

        private readonly object _syncObj = new object();

        /// <summary>
        /// 获取默认的PLC设备
        /// </summary>
        public PLCClient DefaultPLC { get; private set; }

        /// <summary>
        /// 构造PLCDeviceManager
        /// </summary>
        public PLCClientManager()
        {
        }

        /// <summary>
        /// 获取PLC的数量
        /// </summary>
        public int Count => _devices.Count;

        /// <summary>
        /// 根据名称获取PLC，如果名称为空返回默认PLC
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>名称对应的PLC</returns>
        public PLCClient this[string name]
        {
            get
            {
                return _devices.FirstOrDefault(x => x.Match(name));
            }
        }

        /// <summary>
        /// 通过变量名获取变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <returns>变量名称对应的变量</returns>
        public Variable GetVariable(string name)
        {
            var result = default(Variable);
            lock (_syncObj)
            {
                if (_variables.ContainsKey(name))
                {
                    result = _variables[name];
                }
            }

            if (result == null)
            {
                lock (_syncObj)
                {
                    foreach (var plc in _devices)
                    {
                        result = plc.Variables[name];
                        if (result != null)
                        {
                            _variables[name] = result;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 添加PLC
        /// </summary>
        /// <param name="device">要添加的PLC设备</param>
        public void Add(PLCClient device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (!_devices.Contains(device))
            {
                _devices.Add(device);
            }

            DefaultPLC = this[string.Empty];
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>PLC设备枚举器</returns>
        public IEnumerator<PLCClient> GetEnumerator() => _devices.GetEnumerator();

        /// <summary>
        /// 加载变量
        /// </summary>
        /// <param name="variables">变量集合</param>
        public void LoadVariables(IEnumerable<VariableItem> variables)
        {
            if (variables == null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            var plc = default(PLCClient);
            foreach (var v in variables)
            {
                if ((plc == null) || (plc.Match(v.PLCName)))
                {
                    plc = this[v.PLCName];
                }

                if (plc != null)
                {
                    plc.Variables.Add(v, plc);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
            if (_devices != null)
            {
                var array = _devices.ToArray();
                foreach (var dev in array)
                {
                    dev.Dispose();
                }

                _devices.Clear();
            }

            _variables.Clear();
        }
    }
}
