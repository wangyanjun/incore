namespace inc.protocols.modbus
{
    /// <summary>
    /// Command code for modbus.
    /// </summary>
    public enum ModbusFunctionCode
    {
        /// <summary>
        /// Read Discrete Inputs
        /// </summary>
        ReadDiscreteInputs = 0x02,

        /// <summary>
        /// Read Coils
        /// </summary>
        ReadCoils = 0x01,

        /// <summary>
        /// Write Single Coil 
        /// </summary>
        WriteSingleCoil = 0x05,

        /// <summary>
        /// Write Multiple Coils 
        /// </summary>
        WriteMultipleCoils = 0x0F,

        /// <summary>
        /// Read Input Register
        /// </summary>
        ReadInputRegistrer = 0x04,

        /// <summary>
        /// Read Holding Registers
        /// </summary>
        ReadHoldingRegisters = 0x03,

        /// <summary>
        /// Write Single Register
        /// </summary>
        WriteSingleRegister = 0x06,

        /// <summary>
        /// Write Multiple Registers
        /// </summary>
        WriteMultipleRegisters = 0x10,

        /// <summary>
        /// Read/Write Multiple Registers
        /// </summary>
        ReadWriteMultipleRegister = 0x17,

        /// <summary>
        /// Mask Write Register
        /// </summary>
        MaskWriteRegister = 0x16,

        /// <summary>
        /// Read FIFO queue
        /// </summary>
        ReadFIFOQueue = 0x18,

        /// <summary>
        /// Read File record 
        /// </summary>
        ReadFileRecord = 0x14,

        /// <summary>
        /// Write File record 
        /// </summary>
        WriteFileRecord = 0x15,

        /// <summary>
        /// Read Exception status 
        /// </summary>
        ReadExceptionStatus = 0x07,

        /// <summary>
        /// Diagnostic
        /// </summary>
        Diagonostic = 0x08,

        /// <summary>
        /// Get Com event counter
        /// </summary>
        GetComEventCounter = 0x0B,

        /// <summary>
        /// Get Com Event Log 
        /// </summary>
        GetComEventLog = 0x0C,

        /// <summary>
        /// Report Server ID 
        /// </summary>
        ReadServerId = 0x11,

        /// <summary>
        /// Read device Identification 
        /// </summary>
        ReadDeviceIdentification = 0x2B,

        /// <summary>
        /// Encapsulated Interface Transport
        /// </summary>
        EncapsultedInterfaceTransport = 0x2B,

        /// <summary>
        /// CANopen General Reference 
        /// </summary>
        CANOpenGeneralReference = 0x2B
    }
}
