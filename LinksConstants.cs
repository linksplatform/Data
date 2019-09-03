using Platform.Numbers;
using Platform.Ranges;
using Platform.Reflection;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    public class LinksConstants<TLinkAddress>
    {
        public static readonly int DefaultTargetPart = 2;

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
        public Range<TLinkAddress> PossibleInnerReferencesRange { get; }

        /// <summary>Возвращает диапазон возможных индексов для внешних связей (внешних ссылок).</summary>
        public Range<TLinkAddress>? PossibleExternalReferencesRange { get; }

        #endregion

        public LinksConstants(int targetPart, Range<TLinkAddress> possibleInnerReferencesRange, Range<TLinkAddress>? possibleExternalReferencesRange)
        {
            IndexPart = 0;
            SourcePart = 1;
            TargetPart = targetPart;
            Null = Integer<TLinkAddress>.Zero;
            Break = Integer<TLinkAddress>.Zero;
            var currentInnerReferenceIndex = possibleInnerReferencesRange.Maximum;
            Continue = currentInnerReferenceIndex;
            Decrement(ref currentInnerReferenceIndex);
            Skip = currentInnerReferenceIndex;
            Decrement(ref currentInnerReferenceIndex);
            Any = currentInnerReferenceIndex;
            Decrement(ref currentInnerReferenceIndex);
            Itself = currentInnerReferenceIndex;
            Decrement(ref currentInnerReferenceIndex);
            PossibleInnerReferencesRange = (possibleInnerReferencesRange.Minimum, currentInnerReferenceIndex);
            PossibleExternalReferencesRange = possibleExternalReferencesRange;
        }

        private static void Decrement(ref TLinkAddress currentInnerReferenceIndex) => currentInnerReferenceIndex = Arithmetic.Decrement(currentInnerReferenceIndex);

        public LinksConstants(Range<TLinkAddress> possibleInnerReferencesRange, Range<TLinkAddress>? possibleExternalReferencesRange) : this(DefaultTargetPart, possibleInnerReferencesRange, possibleExternalReferencesRange) { }

        public LinksConstants(int targetPart, Range<TLinkAddress> possibleInnerReferencesRange) : this(targetPart, possibleInnerReferencesRange, null) { }

        public LinksConstants(Range<TLinkAddress> possibleInnerReferencesRange) : this(DefaultTargetPart, possibleInnerReferencesRange, null) { }

        public LinksConstants() : this(DefaultTargetPart, (Integer<TLinkAddress>.One, NumericType<TLinkAddress>.MaxValue), null) { }
    }
}
