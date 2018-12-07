using inc.core;
using Xunit;

namespace inc.protocols.finsnet.tests
{
    public class FinsnetAddressTests
    {
        [Fact]
        public void Parse()
        {
            var address = new FinsnetAddress();
            var suc = address.Parse("W10.6");
            Assert.True(suc);
            Assert.Equal("W10.6", address.Address);
            Assert.Equal(FinsMemoryArea.WR, address.MemoryArea);
            Assert.Equal(10, address.MainAddress);
            Assert.Equal(6, address.SubAddress);

            suc = address.Parse("D106");
            Assert.True(suc);
            Assert.Equal(FinsMemoryArea.DM, address.MemoryArea);
            Assert.Equal(106, address.MainAddress);
            Assert.False(address.SubAddress.HasValue);

            suc = address.Parse("XXX");
            Assert.Equal("XXX", address.Address);
            Assert.False(suc);
        }

        [Fact]
        public void Copy()
        {
            var addr = new FinsnetAddress()
            {
                Address = "W120.3",
                MainAddress = 120,
                SubAddress = 3,
                MemoryArea = FinsMemoryArea.WR
            };

            var copy = addr.Copy as FinsnetAddress;
            Assert.NotNull(copy);
            Assert.Equal(addr.Address, copy.Address);
            Assert.Equal(addr.MainAddress, copy.MainAddress);
            Assert.Equal(addr.SubAddress, copy.SubAddress);
            Assert.Equal(addr.MemoryArea, copy.MemoryArea);
        }

        [Fact]
        public void ComputeAddress()
        {
            var addr = new FinsnetAddress()
            {
                Address = "W120.3",
                MainAddress = 120,
                SubAddress = 3,
                MemoryArea = FinsMemoryArea.WR
            };

            Assert.Equal("W120.3", addr.ComputeAddress());

            addr.Address = "D1245";
            addr.MainAddress = 1245;
            addr.SubAddress = null;
            addr.MemoryArea = FinsMemoryArea.DM;
            Assert.Equal("D1245", addr.ComputeAddress());
        }
    }
}
