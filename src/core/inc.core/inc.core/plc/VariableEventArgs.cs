using System;

namespace inc.core.plc
{
    /// <summary>
    /// 变量事件参数
    /// </summary>
    public class VariableEventArgs : EventArgs
    {
        public object RawValue { get; private set; }

        /// <summary>
        /// 构造VariableEventArgs
        /// </summary>
        /// <param name="variable">变量</param>
        public VariableEventArgs(Variable variable)
        {
            Variable = variable;
            RawValue = variable.RawValue;
        }

        /// <summary>
        /// 获取相关变量
        /// </summary>
        public Variable Variable { get; }
    }
}
