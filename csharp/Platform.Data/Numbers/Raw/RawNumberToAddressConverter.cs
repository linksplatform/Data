using System.Numerics;
using System.Runtime.CompilerServices;
using Platform.Converters;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Numbers.Raw
{
    /// <summary>
    /// <para>
    /// Represents the raw number to address converter.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="IConverter{TLinkAddress}"/>
    public class RawNumberToAddressConverter<TLinkAddress> : IConverter<TLinkAddress> where TLinkAddress : IUnsignedNumber<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Converts the source.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="source">
        /// <para>The source.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLinkAddress Convert(TLinkAddress source)
        {
            // Simplified: just clear the most significant bit to get a positive address
            // This replaces the complex Hybrid<TLinkAddress>(source).AbsoluteValue logic  
            // The key insight is that we just need to ensure the result has MSB = 0
            var longValue = long.CreateTruncating(source);
            var clearedMSB = longValue & 0x7FFFFFFFFFFFFFFF; // Clear MSB using bitwise AND
            return TLinkAddress.CreateTruncating(clearedMSB);
        }
    }
}
