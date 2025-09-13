using System.Numerics;
using System.Runtime.CompilerServices;
using Platform.Converters;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Numbers.Raw
{
    /// <summary>
    /// <para>
    /// Represents the address to raw number converter.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="IConverter{TLinkAddress}"/>
    public class AddressToRawNumberConverter<TLinkAddress> : IConverter<TLinkAddress> where TLinkAddress : IUnsignedNumber<TLinkAddress>
    {
        private static readonly UncheckedConverter<TLinkAddress, ulong> _addressToUInt64Converter = UncheckedConverter<TLinkAddress, ulong>.Default;
        private static readonly UncheckedConverter<ulong, TLinkAddress> _uInt64ToAddressConverter = UncheckedConverter<ulong, TLinkAddress>.Default;
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
            var ulongValue = _addressToUInt64Converter.Convert(source);
            
            // Calculate MSB mask based on the bit size of TLinkAddress
            var bitSize = System.Runtime.InteropServices.Marshal.SizeOf<TLinkAddress>() * 8;
            var msbMask = 1UL << (bitSize - 1);
            
            // Set the most significant bit
            var result = ulongValue | msbMask;
            
            return _uInt64ToAddressConverter.Convert(result);
        }
    }
}
