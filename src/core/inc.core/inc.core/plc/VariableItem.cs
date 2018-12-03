namespace inc.core.plc
{
    /// <summary>
    /// The class represent a variable in plc.
    /// </summary>
    public class VariableItem
    {
        /// <summary>
        /// Get or set the name of variable
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set special thread token.
        /// </summary>
        public string SpecialThreadToken { get; set; }

        /// <summary>
        /// Get or set the tag of varialbe
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 获取或设置显示
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 获取对应的PLC的名称，为空表示默认的PLC
        /// </summary>
        public string PLCName { get; set; }

        /// <summary>
        /// 获取或设置是否可读
        /// </summary>
        public bool Readable { get; set; } = true;

        /// <summary>
        /// 获取或设置是否可写
        /// </summary>
        public bool Writable { get; set; } = true;

        /// <summary>
        /// 获取或设置数据读取地址
        /// </summary>
        public string ReadAddress { get; set; }

        /// <summary>
        /// 获取或设置数据写入地址
        /// </summary>
        public string WriteAddress { get; set; }

        /// <summary>
        /// 读写数据地址是否分离
        /// </summary>
        public bool SplitReadAndWriteAddress { get; set; }

        /// <summary>
        /// 获取或设置存储的数值单位
        /// </summary>
        public string StorageUnitSymbol { get; set; }

        /// <summary>
        /// 获取或设置显示的数值单位
        /// </summary>
        public string DisplayUnitSymbol { get; set; }

        /// <summary>
        /// 获取或设置数据类型
        /// </summary>
        public DataType DataType { get; set; } = DataType.Int32;

        /// <summary>
        /// 获取或设置数组长度,如果不是数组则值为空
        /// </summary>
        public int? ArrayLength { get; set; }

        /// <summary>
        /// 获取或设置记录时间间隔,未设置以PLC设置为准,以秒为单位
        /// </summary>
        public double? ReadSpanInSeconds { get; set; }

        /// <summary>
        /// 获取或设置规格上限
        /// </summary>
        public double? SpecHigh { get; set; }

        /// <summary>
        /// 获取或设置规格下限
        /// </summary>
        public double? SpecLow { get; set; }

        public override string ToString()
        {
            return $"{Name} Tag={Tag} R.A={ReadAddress} W.A={WriteAddress} Len={ArrayLength ?? 1}";
        }
    }
}
