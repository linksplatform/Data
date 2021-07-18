using Xunit;

namespace Platform.Data.Tests
{
    public static class HybridTests
    {
        [Fact]
        public static void ObjectConstructorTest()
        {
            //Assert.Equal(0, new Hybrid<byte>(unchecked((byte)128)).AbsoluteValue);
            //Assert.Equal(0, new Hybrid<byte>((object)128).AbsoluteValue);
            //Assert.Equal(1, new Hybrid<byte>(unchecked((byte)-1)).AbsoluteValue);
            Assert.Equal(255, new Hybrid<byte>(unchecked((byte)255)).AbsoluteValue);
            //Assert.Equal(1, new Hybrid<byte>((object)-1).AbsoluteValue);
            //Assert.Equal(0, new Hybrid<byte>(unchecked((byte)0)).AbsoluteValue);
            //Assert.Equal(0, new Hybrid<byte>((object)0).AbsoluteValue);
            //Assert.Equal(1, new Hybrid<byte>(unchecked((byte)1)).AbsoluteValue);
            //Assert.Equal(1, new Hybrid<byte>((object)1).AbsoluteValue);
        }
    }
}
