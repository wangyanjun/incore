namespace inc.core
{
    /// <summary>
    /// 通讯状态
    /// </summary>
    public enum CommunicationStatus
    {
        /// <summary>
        /// 连接已经建立
        /// </summary>
        Connected,

        /// <summary>
        /// 连接建立中
        /// </summary>
        Connecting,

        /// <summary>
        /// 未连接
        /// </summary>
        NotConnected,

        /// <summary>
        /// 失败
        /// </summary>
        Failed,

        /// <summary>
        /// 断开连接中
        /// </summary>
        Disconnecting,

        /// <summary>
        /// 连接已经断开
        /// </summary>
        Disconnected
    }
}
