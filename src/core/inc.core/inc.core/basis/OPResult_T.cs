namespace inc.core
{
    /// <summary>
    /// 操作结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class OpResult<T> : OpResult
    {
        /// <summary>
        /// 获取或设置内容
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// 构造OpResult
        /// </summary>
        public OpResult() { }

        /// <summary>
        /// 构造OpResult
        /// </summary>
        /// <param name="content">数据</param>
        public OpResult(T content)
        {
            Content = content;
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <returns>附加的数据内容</returns>
        public override object GetContent() => IsSuccess ? Content : default(object);
    }
}
