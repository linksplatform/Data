using Xunit;

namespace Platform.Data.Tests
{
    /// <summary>
    /// <para>
    /// Represents the hybrid tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class HybridTests
    {
        /// <summary>
        /// <para>
        /// Tests that object constructor test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void ObjectConstructorTest()
        {
            Assert.Equal(0, new Hybrid<byte>(unchecked((byte)128)).AbsoluteValue);
            Assert.Equal(0, new Hybrid<byte>((object)128).AbsoluteValue);
            Assert.Equal(1, new Hybrid<byte>(unchecked((byte)-1)).AbsoluteValue);
            Assert.Equal(1, new Hybrid<byte>((object)-1).AbsoluteValue);
            Assert.Equal(0, new Hybrid<byte>(unchecked((byte)0)).AbsoluteValue);
            Assert.Equal(0, new Hybrid<byte>((object)0).AbsoluteValue);
            Assert.Equal(1, new Hybrid<byte>(unchecked((byte)1)).AbsoluteValue);
            Assert.Equal(1, new Hybrid<byte>((object)1).AbsoluteValue);
        }
    }
}
