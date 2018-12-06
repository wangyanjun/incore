namespace inc.protocols.modbus
{
    public enum ModbusSubFunction
    {
        ReturnQueryData = 0x00,

        RestartCommicationOptions = 0x01,

        ReturnDiagonosticRegister = 0x02,

        ChangeASCIIInputDelimiter = 0x03,

        ForceListenOnlyMode = 0x04,

        ClearCountersAndDiagnosticRegister = 0x0A,

        ReturnBusMessageCount = 0x0B,

        ReturnBusCommunicationErrorCount = 0x0C,

        ReturnBusExceptionErrorCount = 0x0D,

        ReturnBusServerMessageCount = 0x0E,

        ReturnBusServerNoResponseCount = 0x0F,

        ReturnServerNAKCount = 0x10,

        ReturnServerBusyCount = 0x11,

        ReturnBusCharacterOverrunCount = 0x12,

        ClearOverrunCounterAndFlag = 0x14
    }
}
