using System;

namespace inc.core.plc
{
    /// <summary>
    /// 变量
    /// </summary>
    public class Variable
    {
        public IPLCCommunicator Communicator { get; set; }

        public int MainAddress { get; set; }

        public int? SubAddress { get; set; }

        public FinsMemoryArea MemoryArea { get; set; } = FinsMemoryArea.NotComputed;

        /// <summary>
        /// 获取值是否超过规格
        /// </summary>
        public bool OverSpec => OverSpecHigh || OverSpecLow;

        /// <summary>
        /// 获取是否超过规格上限
        /// </summary>
        public bool OverSpecHigh { get; private set; }

        /// <summary>
        /// 获取是否超过规格下限
        /// </summary>
        public bool OverSpecLow { get; private set; }

        /// <summary>
        /// 获取上一次读取的时间
        /// </summary>
        public DateTime LastRead { get; set; }

        /// <summary>
        /// 值被设置事件
        /// </summary>
        public event EventHandler<VariableEventArgs> ValueGot;

        /// <summary>
        /// 获取是否有错误
        /// </summary>
        public bool HasError { get; private set; }

        /// <summary>
        /// 获取或设置原始接收值
        /// </summary>
        public object RawValue { get; private set; }

        public string LastError { get; private set; }

        public bool? ValueAsBoolean
        {
            get
            {
                var result = default(bool?);
                if (RawValue is bool bValue)
                {
                    result = bValue;
                }

                return result;
            }
        }

        /// <summary>
        /// 读取结果设置
        /// </summary>
        /// <param name="value">读取结果</param>
        public virtual void ReadSet(OpResult value)
        {
            OverSpecHigh = false;
            OverSpecLow = false;
            if (value == null)
            {
                RawValue = null;
                HasError = true;
                LastError = null;
            }
            else
            {
                ReadSet(value.GetContent(), value.IsSuccess, value.Message);
            }

            OnValueGot();
        }

        /// <summary>
        /// 读取结果设置
        /// </summary>
        /// <param name="value">读取结果</param>
        public virtual void ReadSet(object value, bool success, string message)
        {
            OverSpecHigh = false;
            OverSpecLow = false;
            LastError = message;
            if (value == null)
            {
                RawValue = null;
                HasError = true;
                LastError = null;
            }
            else
            {
                LastRead = DateTime.Now;
                if (success)
                {
                    LastError = null;
                    RawValue = value;
                    if (Item.SpecHigh.HasValue || Item.SpecLow.HasValue)
                    {
                        if (RawValue is IConvertible convertable)
                        {
                            OverSpecHigh = Item.SpecHigh.HasValue && (convertable.ToDouble(null) > Item.SpecHigh);
                            OverSpecLow = Item.SpecLow.HasValue && (convertable.ToDouble(null) < Item.SpecLow);
                        }
                    }

                    HasError = false;
                }
                else
                {
                    LastError = message;
                    HasError = true;
                }
            }

            OnValueGot();
        }

        /// <summary>
        /// 判断是否需要记录
        /// </summary>
        /// <param name="readSpan">默认的读取间隔</param>
        /// <returns>是否需要读取</returns>
        public bool NeedReadNow(double readSpan)
        {
            var span = Item.ReadSpanInSeconds ?? readSpan;
            var duration = DateTime.Now - LastRead;
            return Item.Readable && Math.Abs(duration.TotalSeconds) >= span;
        }

        /// <summary>
        /// 值被设置处理函数
        /// </summary>
        protected void OnValueGot()
        {
            OnValueGot(new VariableEventArgs(this));
        }

        /// <summary>
        /// 值被设置处理函数
        /// </summary>
        /// <param name="e">事件参数</param>
        protected virtual void OnValueGot(VariableEventArgs e)
        {
            ValueGot?.Invoke(this, e);
        }

        /// <summary>
        /// 获取PLC设备
        /// </summary>
        public PLCClient PLC { get; private set; }

        /// <summary>
        /// 获取变量项目
        /// </summary>
        public VariableItem Item { get; private set; }

        /// <summary>
        /// 初始化Variable
        /// </summary>
        /// <param name="item">变量项目</param>
        /// <param name="plc">PLC设备</param>
        public Variable(VariableItem item, PLCClient plc)
        {
            Item = item;
            PLC = plc;
        }

        /// <summary>
        /// 写入变量值
        /// </summary>
        /// <param name="value">写入值</param>
        /// <returns>写入结果</returns>
        public OpResult WriteValue(object value)
        {
            return PLC.ForceWrite(this, value);
        }

        /// <summary>
        /// 读取变量值
        /// </summary>
        /// <returns>读取结果</returns>
        public OpResult ReadValue()
        {
            return PLC.ForceRead(this);
        }

        public override string ToString()
        {
            return $"{Item.Name}[{Item.ReadAddress}]: {RawValue}";
        }
    }
}
