using Xunit;
using Platform.Data.Numbers.Raw;

namespace Platform.Data.Tests.Numbers.Raw
{
    /// <summary>
    /// <para>
    /// Represents the raw number to address converter tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class RawNumberToAddressConverterTests
    {
        /// <summary>
        /// <para>
        /// Tests the converter clears the most significant bit correctly.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void ConverterClearsMSBTest()
        {
            var converter = new RawNumberToAddressConverter<ulong>();
            
            // Test with value that has MSB set (all bits set)
            var inputWithMSB = 0xFFFFFFFFFFFFFFFFUL; // All bits set, MSB = 1
            var expectedOutput = 0x7FFFFFFFFFFFFFFFUL; // MSB cleared
            var result = converter.Convert(inputWithMSB);
            Assert.Equal(expectedOutput, result);
            
            // Test with value that doesn't have MSB set
            var inputWithoutMSB = 0x7FFFFFFFFFFFFFFFUL; // MSB already 0
            var result2 = converter.Convert(inputWithoutMSB);
            Assert.Equal(inputWithoutMSB, result2); // Should remain unchanged
            
            // Test with zero
            var result3 = converter.Convert(0UL);
            Assert.Equal(0UL, result3);
            
            // Test with maximum value that doesn't have MSB set
            var maxWithoutMSB = 0x7FFFFFFFFFFFFFFFUL;
            var result4 = converter.Convert(maxWithoutMSB);
            Assert.Equal(maxWithoutMSB, result4);
        }

        /// <summary>
        /// <para>
        /// Tests the converter with different unsigned integer types.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void ConverterWorksWithDifferentTypesTest()
        {
            // Test with uint
            var converterUint = new RawNumberToAddressConverter<uint>();
            var inputUint = 0xFFFFFFFFU; // All bits set
            var expectedUint = 0x7FFFFFFFU; // MSB cleared
            var resultUint = converterUint.Convert(inputUint);
            Assert.Equal(expectedUint, resultUint);
            
            // Test with ushort
            var converterUshort = new RawNumberToAddressConverter<ushort>();
            var inputUshort = (ushort)0xFFFF; // All bits set
            var expectedUshort = (ushort)0x7FFF; // MSB cleared
            var resultUshort = converterUshort.Convert(inputUshort);
            Assert.Equal(expectedUshort, resultUshort);
            
            // Test with byte
            var converterByte = new RawNumberToAddressConverter<byte>();
            var inputByte = (byte)0xFF; // All bits set
            var expectedByte = (byte)0x7F; // MSB cleared
            var resultByte = converterByte.Convert(inputByte);
            Assert.Equal(expectedByte, resultByte);
        }

        /// <summary>
        /// <para>
        /// Tests what the original Hybrid.AbsoluteValue was actually doing.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void DebugOriginalBehaviorTest()
        {
            // Test specific failing case
            ulong input1 = 0x8000000000000000UL; // MSB set, other bits 0
            var hybrid1 = new Hybrid<ulong>(input1);
            var originalResult1 = hybrid1.AbsoluteValue; // This returns 0 according to test failure
            
            // Test with ulong max value (all bits set)
            ulong input2 = 0xFFFFFFFFFFFFFFFFUL;
            var hybrid2 = new Hybrid<ulong>(input2);
            var originalResult2 = hybrid2.AbsoluteValue; // What does this return?
            
            // The test failures show that:
            // - For 0x8000000000000000UL, original returns 0, my implementation returns 9223372036854775808
            // - For 0xFFFFFFFFFFFFFFFFUL, original returns 1, my implementation returns 1
            
            // This suggests the original might be handling the special case differently
            Assert.Equal(0, originalResult1); // Verify what the original actually returns
            Assert.Equal(1, originalResult2); // Verify what the original actually returns
        }

        /// <summary>
        /// <para>
        /// Tests the converter behavior is equivalent to the original Hybrid.AbsoluteValue.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void ConverterEquivalentToOriginalTest()
        {
            var converter = new RawNumberToAddressConverter<ulong>();
            
            // Test various values to ensure they match the original Hybrid behavior
            ulong[] testValues = { 0UL, 1UL, 0x8000000000000000UL, 0xFFFFFFFFFFFFFFFFUL, 0x7FFFFFFFFFFFFFFFUL, 0x1234567890ABCDEFUL };
            
            foreach (var value in testValues)
            {
                var converterResult = converter.Convert(value);
                var originalResult = new Hybrid<ulong>(value).AbsoluteValue;
                Assert.Equal((ulong)originalResult, converterResult);
            }
        }
    }
}