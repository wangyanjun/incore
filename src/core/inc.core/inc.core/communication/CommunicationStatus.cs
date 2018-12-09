namespace inc.core
{
    /// <summary>
    /// Communication status
    /// </summary>
    public enum CommunicationStatus
    {
        /// <summary>
        /// Connection connected
        /// </summary>
        Connected,

        /// <summary>
        /// Connnection is connecting
        /// </summary>
        Connecting,

        /// <summary>
        /// Not connected
        /// </summary>
        NotConnected,

        /// <summary>
        /// Failed
        /// </summary>
        Failed,

        /// <summary>
        /// Connection is disconnecting
        /// </summary>
        Disconnecting,

        /// <summary>
        /// Connection is disconnected
        /// </summary>
        Disconnected
    }
}
