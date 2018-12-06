using System;
using System.Collections;
using System.Collections.Generic;

namespace inc.core.plc
{
    /// <summary>
    /// 变量集合
    /// </summary>
    public class VariableCollection : IEnumerable<KeyValuePair<string, Variable>>
    {
        private readonly SortedDictionary<string, Variable> _variables = new SortedDictionary<string, Variable>();

        /// <summary>
        /// 获取数目
        /// </summary>
        public int Count => _variables.Count;

        /// <summary>
        /// 根据名称获取变量
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>名称对应的变量</returns>
        public Variable this[string name]
        {
            get
            {
                var result = default(Variable);
                if ((!string.IsNullOrEmpty(name)) && _variables.ContainsKey(name))
                {
                    result = _variables[name];
                }

                return result;
            }

            set
            {
                _variables[name] = value;
            }
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<KeyValuePair<string, Variable>> GetEnumerator() => _variables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 添加变量
        /// </summary>
        /// <param name="item"></param>
        /// <param name="plc"></param>
        public void Add(VariableItem item, PLCClient plc)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (plc == null) throw new ArgumentNullException(nameof(plc));
            var v = new Variable(item, plc);
            _variables[v.Item.Name] = v;
        }

        public void Add(Variable variable)
        {
            _variables[variable.Item.Name] = variable;
        }

        public void Clear()
        {
            _variables.Clear();
        }
    }
}
