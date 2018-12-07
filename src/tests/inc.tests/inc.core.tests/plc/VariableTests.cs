using inc.core.plc;
using Xunit;

namespace inc.tests.plc
{
    public class VariableTests
    {
        [Fact]
        public void VauseAsBoolean()
        {
            var v = new Variable(new VariableItem(), new PLCClient());
            Assert.False(v.ValueAsBoolean.HasValue);
        }
    }
}
