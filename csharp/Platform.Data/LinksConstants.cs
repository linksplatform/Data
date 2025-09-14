using System.Numerics;
using System.Runtime.CompilerServices;
using Platform.Ranges;
using Platform.Reflection;
using Platform.Converters;
using Platform.Numbers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    /// <summary>
    /// <para>
    /// Represents the links constants.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="LinksConstantsBase"/>
    public class LinksConstants<TLinkAddress> : LinksConstantsBase where TLinkAddress : IUnsignedNumber<TLinkAddress>
    {
        private static readonly UncheckedConverter<ulong, TLinkAddress> _uInt64ToAddressConverter = UncheckedConverter<ulong, TLinkAddress>.Default;

        #region Link parts

        /// <summary>Returns the index of the part that is responsible for the index (address, identifier) of the link itself.</summary>
        public int IndexPart
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Returns the index of the part that is responsible for the reference to the source link (first part-value).</summary>
        public int SourcePart
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Returns the index of the part that is responsible for the reference to the target link (last part-value).</summary>
        public int TargetPart
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion

        #region Flow control

        /// <summary>Returns a value that represents the continuation of link traversal.</summary>
        /// <remarks>Используется в функции обработчике, который передаётся в функцию Each.</remarks>
        public TLinkAddress Continue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Returns a value that represents the stopping of link traversal.</summary>
        /// <remarks>Используется в функции обработчике, который передаётся в функцию Each.</remarks>
        public TLinkAddress Break
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Returns a value that represents the skipping in link traversal.</summary>
        public TLinkAddress Skip
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion

        #region Special symbols

        /// <summary>Returns a value that represents the absence of a link.</summary>
        public TLinkAddress Null
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Returns a value that represents any link.</summary>
        /// <remarks>Возможно нужно зарезервировать отдельное значение, тогда можно будет создавать все варианты последовательностей в функции Create.</remarks>
        public TLinkAddress Any
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Returns a value that represents a link-reference to the link itself.</summary>
        public TLinkAddress Itself
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        public TLinkAddress Error { get; }

        #endregion

        #region References

        /// <summary>Returns the range of possible indexes for internal links (internal references).</summary>
        public Range<TLinkAddress> InternalReferencesRange
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Returns the range of possible indexes for external links (external references).</summary>
        public Range<TLinkAddress>? ExternalReferencesRange
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinksConstants{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="targetPart">
        /// <para>A target part.</para>
        /// <para></para>
        /// </param>
        /// <param name="possibleInternalReferencesRange">
        /// <para>A possible internal references range.</para>
        /// <para></para>
        /// </param>
        /// <param name="possibleExternalReferencesRange">
        /// <para>A possible external references range.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinksConstants(int targetPart, Range<TLinkAddress> possibleInternalReferencesRange, Range<TLinkAddress>? possibleExternalReferencesRange)
        {
            IndexPart = 0;
            SourcePart = 1;
            TargetPart = targetPart;
            var currentInternalReferenceIndex = possibleInternalReferencesRange.Maximum;
            Null = default;
            Continue = currentInternalReferenceIndex;
            Break = --currentInternalReferenceIndex;
            Skip = --currentInternalReferenceIndex;
            Any = --currentInternalReferenceIndex;
            Itself = --currentInternalReferenceIndex;
            Error = --currentInternalReferenceIndex;
            --currentInternalReferenceIndex;
            InternalReferencesRange = (possibleInternalReferencesRange.Minimum, currentInternalReferenceIndex);
            ExternalReferencesRange = possibleExternalReferencesRange;
        }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinksConstants{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="targetPart">
        /// <para>A target part.</para>
        /// <para></para>
        /// </param>
        /// <param name="enableExternalReferencesSupport">
        /// <para>A enable external references support.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinksConstants(int targetPart, bool enableExternalReferencesSupport) : this(targetPart, GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinksConstants{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="possibleInternalReferencesRange">
        /// <para>A possible internal references range.</para>
        /// <para></para>
        /// </param>
        /// <param name="possibleExternalReferencesRange">
        /// <para>A possible external references range.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinksConstants(Range<TLinkAddress> possibleInternalReferencesRange, Range<TLinkAddress>? possibleExternalReferencesRange) : this(DefaultTargetPart, possibleInternalReferencesRange, possibleExternalReferencesRange) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinksConstants{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="enableExternalReferencesSupport">
        /// <para>A enable external references support.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinksConstants(bool enableExternalReferencesSupport) : this(GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinksConstants{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="targetPart">
        /// <para>A target part.</para>
        /// <para></para>
        /// </param>
        /// <param name="possibleInternalReferencesRange">
        /// <para>A possible internal references range.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinksConstants(int targetPart, Range<TLinkAddress> possibleInternalReferencesRange) : this(targetPart, possibleInternalReferencesRange, null) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinksConstants{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="possibleInternalReferencesRange">
        /// <para>A possible internal references range.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinksConstants(Range<TLinkAddress> possibleInternalReferencesRange) : this(DefaultTargetPart, possibleInternalReferencesRange, null) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="LinksConstants{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinksConstants() : this(DefaultTargetPart, enableExternalReferencesSupport: false) { }

        /// <summary>
        /// <para>
        /// Gets the default internal references range using the specified enable external references support.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="enableExternalReferencesSupport">
        /// <para>The enable external references support.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A range of t link address</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Range<TLinkAddress> GetDefaultInternalReferencesRange(bool enableExternalReferencesSupport)
        {
            if (enableExternalReferencesSupport)
            {
                return (TLinkAddress.One, Hybrid<TLinkAddress>.HalfOfNumberValuesRange);
            }
            else
            {
                return (TLinkAddress.One, NumericType<TLinkAddress>.MaxValue);
            }
        }

        /// <summary>
        /// <para>
        /// Gets the default external references range using the specified enable external references support.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="enableExternalReferencesSupport">
        /// <para>The enable external references support.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A range of t link address</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Range<TLinkAddress>? GetDefaultExternalReferencesRange(bool enableExternalReferencesSupport)
        {
            if (enableExternalReferencesSupport)
            {
                return (Hybrid<TLinkAddress>.ExternalZero, NumericType<TLinkAddress>.MaxValue);
            }
            else
            {
                return null;
            }
        }
    }
}
