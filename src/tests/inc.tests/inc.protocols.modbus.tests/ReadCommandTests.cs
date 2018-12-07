using Xunit;

namespace inc.protocols.modbus.tests
{
    public class ReadCommandTests
    {
        [Fact]
        public void Encode()
        {
            var command = new ReadCommand()
            {
                Address = 0x12,
                FunctionCode = ModbusFunctionCode.ReadCoils,
                Count = 2
            };

            var content = command.Encode();
            Assert.Equal(5, content.Length);
            Assert.Equal(command.Address, content[2]);
            Assert.Equal(command.Count, content[4]);
        }
    }
}
