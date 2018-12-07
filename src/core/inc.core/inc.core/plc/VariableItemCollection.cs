using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace inc.core.plc
{
    /// <summary>
    /// 变量集合
    /// </summary>
    public class VariableItemCollection : IEnumerable<VariableItem>
    {
        private readonly SortedDictionary<string, VariableItem> _variables
            = new SortedDictionary<string, VariableItem>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 根据名称获取变量
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>名称对应的变量</returns>
        public VariableItem this[string name]
        {
            get
            {
                return _variables[name];
            }

            set
            {
                _variables[name] = value;
            }
        }

        public int Count => _variables.Count;

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<VariableItem> GetEnumerator() => _variables.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool ReadFromCSVFile(string path)
        {
            var result = false;
            var lines = File.ReadAllLines(path, Encoding.UTF8);
            foreach (var lin in lines)
            {
                var parts = lin.Split(',');
                if (parts.Length >= 5)
                {
                    var item = new VariableItem()
                    {
                        Tag = parts[0]?.Trim(),
                        Name = parts[0]?.Trim()
                    };

                    DecorateName(item);
                    SetDataTypeAndArrayLength(item, parts[1]?.Trim());
                    item.ReadAddress = parts[3]?.Trim();
                    item.WriteAddress = item.ReadAddress;
                    if (string.IsNullOrEmpty(item.ReadAddress) || (item.ReadAddress.Length > 20))
                    {
                        continue;
                    }

                    _variables[item.Name] = item;
                }
            }

            return result;
        }

        private void DecorateName(VariableItem item)
        {
            int index = item.Name.LastIndexOf('/');
            if (index > 0)
            {
                item.Name = item.Name.Substring(index + 1);
            }
        }

        private void SetDataTypeAndArrayLength(VariableItem item, string type)
        {
            var index0 = type.LastIndexOf("[");
            var index1 = type.LastIndexOf("]");
            var typeWithoutLength = type;
            var hasRange = false;
            if (index0 > 0 && index1 > 0 && index1 > index0)
            {
                typeWithoutLength = type.Substring(0, index0);
                var len = type.Substring(index0 + 1, index1 - index0 - 1);
                index0 = len.IndexOf("..");
                if (index0 > 0)
                {
                    len = len.Substring(index0 + 2);
                    index0 = type.IndexOf("OF", StringComparison.OrdinalIgnoreCase);
                    if (index0 > 0)
                    {
                        hasRange = true;
                        typeWithoutLength = type.Substring(index0 + 3);
                    }
                }

                if (int.TryParse(len, out int arrayLen))
                {
                    if (hasRange) arrayLen += 1;
                    item.ArrayLength = arrayLen;
                }
            }

            item.DataType = ParseType(typeWithoutLength);
        }

        private DataType ParseType(string dataType)
        {
            switch (dataType?.ToLower())
            {
                case "bool":
                case "boolean":
                case "bit":
                    {
                        return DataType.Bit;
                    }

                case "word":
                    {
                        return DataType.UInt16;
                    }

                case "dword":
                    {
                        return DataType.UInt32;
                    }

                case "uint":
                    {
                        return DataType.UInt16;
                    }

                case "sint":
                case "int":
                    {
                        return DataType.Int16;
                    }

                case "sdint":
                case "dint":
                    {
                        return DataType.Int32;
                    }

                case "udint":
                    {
                        return DataType.UInt32;
                    }

                case "real":
                    {
                        return DataType.Single;
                    }

                case "lreal":
                    {
                        return DataType.Double;
                    }

                default:
                    {
                        throw new NotSupportedException($"Type [{dataType} not supported");
                    }
            }
        }
    }
}
