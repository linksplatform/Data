using Platform.Ranges;
using Platform.Reflection;
using Platform.Converters;
using Platform.Numbers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    public class LinksConstants<TLinkAddress>
    {
        public const int DefaultTargetPart = 2;

        private static readonly TLinkAddress _one = Arithmetic<TLinkAddress>.Increment(default);
        private static readonly UncheckedConverter<ulong, TLinkAddress> _uInt64ToAddressConverter = UncheckedConverter<ulong, TLinkAddress>.Default;

        #region Link parts

        /// <summary>Возвращает индекс части, которая отвечает за индекс (адрес, идентификатор) самой связи.</summary>
        public int IndexPart { get; }

        /// <summary>Возвращает индекс части, которая отвечает за ссылку на связь-начало (первая часть-значение).</summary>
        public int SourcePart { get; }

        /// <summary>Возвращает индекс части, которая отвечает за ссылку на связь-конец (последняя часть-значение).</summary>
        public int TargetPart { get; }

        #endregion

        #region Flow control

        /// <summary>Возвращает значение, обозначающее продолжение прохода по связям.</summary>
        /// <remarks>Используется в функции обработчике, который передаётся в функцию Each.</remarks>
        public TLinkAddress Continue { get; }

        /// <summary>Возвращает значение, обозначающее пропуск в проходе по связям.</summary>
        public TLinkAddress Skip { get; }

        /// <summary>Возвращает значение, обозначающее остановку прохода по связям.</summary>
        /// <remarks>Используется в функции обработчике, который передаётся в функцию Each.</remarks>
        public TLinkAddress Break { get; }

        #endregion

        #region Special symbols

        /// <summary>Возвращает значение, обозначающее отсутствие связи.</summary>
        public TLinkAddress Null { get; }

        /// <summary>Возвращает значение, обозначающее любую связь.</summary>
        /// <remarks>Возможно нужно зарезервировать отдельное значение, тогда можно будет создавать все варианты последовательностей в функции Create.</remarks>
        public TLinkAddress Any { get; }

        /// <summary>Возвращает значение, обозначающее связь-ссылку на саму связь.</summary>
        public TLinkAddress Itself { get; }

        #endregion

        #region References

        /// <summary>Возвращает диапазон возможных индексов для внутренних связей (внутренних ссылок).</summary>
        public Range<TLinkAddress> InternalReferencesRange { get; }

        /// <summary>Возвращает диапазон возможных индексов для внешних связей (внешних ссылок).</summary>
        public Range<TLinkAddress>? ExternalReferencesRange { get; }

        #endregion

        public LinksConstants(int targetPart, Range<TLinkAddress> possibleInternalReferencesRange, Range<TLinkAddress>? possibleExternalReferencesRange)
        {
            IndexPart = 0;
            SourcePart = 1;
            TargetPart = targetPart;
            Null = default;
            Break = default;
            var currentInternalReferenceIndex = possibleInternalReferencesRange.Maximum;
            Continue = currentInternalReferenceIndex;
            Decrement(ref currentInternalReferenceIndex);
            Skip = currentInternalReferenceIndex;
            Decrement(ref currentInternalReferenceIndex);
            Any = currentInternalReferenceIndex;
            Decrement(ref currentInternalReferenceIndex);
            Itself = currentInternalReferenceIndex;
            Decrement(ref currentInternalReferenceIndex);
            InternalReferencesRange = (possibleInternalReferencesRange.Minimum, currentInternalReferenceIndex);
            ExternalReferencesRange = possibleExternalReferencesRange;
        }

        public LinksConstants(int targetPart, bool enableExternalReferencesSupport) : this(targetPart, GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) {}

        public LinksConstants(Range<TLinkAddress> possibleInternalReferencesRange, Range<TLinkAddress>? possibleExternalReferencesRange) : this(DefaultTargetPart, possibleInternalReferencesRange, possibleExternalReferencesRange) { }

        public LinksConstants(bool enableExternalReferencesSupport) : this(GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) { }

        public LinksConstants(int targetPart, Range<TLinkAddress> possibleInternalReferencesRange) : this(targetPart, possibleInternalReferencesRange, null) { }

        public LinksConstants(Range<TLinkAddress> possibleInternalReferencesRange) : this(DefaultTargetPart, possibleInternalReferencesRange, null) { }

        public LinksConstants() : this(DefaultTargetPart, enableExternalReferencesSupport: false) { }

        public static Range<TLinkAddress> GetDefaultInternalReferencesRange(bool enableExternalReferencesSupport)
        {
            if (enableExternalReferencesSupport)
            {
                return (_one, _uInt64ToAddressConverter.Convert(Hybrid<TLinkAddress>.HalfOfNumberValuesRange));
            }
            else
            {
                return (_one, NumericType<TLinkAddress>.MaxValue);
            }
        }

        public static Range<TLinkAddress>? GetDefaultExternalReferencesRange(bool enableExternalReferencesSupport)
        {
            if (enableExternalReferencesSupport)
            {
                return (_uInt64ToAddressConverter.Convert(Hybrid<TLinkAddress>.HalfOfNumberValuesRange + 1UL), NumericType<TLinkAddress>.MaxValue);
            }
            else
            {
                return null;
            }
        }

        private static void Decrement(ref TLinkAddress currentInternalReferenceIndex) => currentInternalReferenceIndex = Arithmetic.Decrement(currentInternalReferenceIndex);
    }
}
