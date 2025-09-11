using System.Numerics;
using Xunit;
using Platform.Reflection;
using Platform.Converters;
using Platform.Numbers;

namespace Platform.Data.Tests
{
    /// <summary>
    /// <para>
    /// Represents the links constants tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class LinksConstantsTests
    {
        /// <summary>
        /// <para>
        /// Tests that constructor test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void ConstructorTest()
        {
            var constants = new LinksConstants<ulong>(enableExternalReferencesSupport: true);
            Assert.Equal(Hybrid<ulong>.ExternalZero, constants.ExternalReferencesRange.Value.Minimum);
            Assert.Equal(ulong.MaxValue, constants.ExternalReferencesRange.Value.Maximum);
        }

        /// <summary>
        /// <para>
        /// Tests that external references test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void ExternalReferencesTest()
        {
            TestExternalReferences<ulong, long>();
            TestExternalReferences<uint, int>();
            TestExternalReferences<ushort, short>();
            TestExternalReferences<byte, sbyte>();
        }

        /// <summary>
        /// <para>
        /// Tests that null checking methods work correctly.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void NullCheckingTest()
        {
            var constants = new LinksConstants<ulong>();
            
            // Test IsNull method
            Assert.True(constants.IsNull(constants.Null));
            Assert.True(constants.IsNull(0UL)); // Null is default which is 0 for ulong
            Assert.False(constants.IsNull(1UL));
            Assert.False(constants.IsNull(constants.Any));
            Assert.False(constants.IsNull(constants.Continue));
            
            // Test IsNotNull method
            Assert.False(constants.IsNotNull(constants.Null));
            Assert.False(constants.IsNotNull(0UL)); // Null is default which is 0 for ulong
            Assert.True(constants.IsNotNull(1UL));
            Assert.True(constants.IsNotNull(constants.Any));
            Assert.True(constants.IsNotNull(constants.Continue));
        }
        private static void TestExternalReferences<TUnsigned, TSigned>() where TUnsigned : IUnsignedNumber<TUnsigned> where TSigned : ISignedNumber<TSigned>
        {
            var unsingedOne = TUnsigned.One;
            var half = TUnsigned.CreateTruncating((NumericType<TSigned>.MaxValue));
            LinksConstants<TUnsigned> constants = new LinksConstants<TUnsigned>((unsingedOne, half), (half+unsingedOne, NumericType<TUnsigned>.MaxValue));

            var minimum = new Hybrid<TUnsigned>(default, isExternal: true);
            var maximum = new Hybrid<TUnsigned>(half, isExternal: true);

            Assert.True(constants.IsExternalReference(minimum));
            Assert.True(minimum.IsExternal);
            Assert.False(minimum.IsInternal);
            Assert.True(constants.IsExternalReference(maximum));
            Assert.True(maximum.IsExternal);
            Assert.False(maximum.IsInternal);
        }
    }
}
