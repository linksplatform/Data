using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Ranges;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    /// <summary>
    /// <para>Represents an interface for constants used by links storage.</para>
    /// <para>Представляет интерфейс для констант, используемых хранилищем связей.</para>
    /// </summary>
    /// <typeparam name="TLinkAddress"><para>The type of link address.</para><para>Тип адреса связи.</para></typeparam>
    public interface ILinksConstants<TLinkAddress>
    {
        #region Link parts

        /// <summary>Возвращает индекс части, которая отвечает за индекс (адрес, идентификатор) самой связи.</summary>
        int IndexPart
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Возвращает индекс части, которая отвечает за ссылку на связь-начало (первая часть-значение).</summary>
        int SourcePart
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Возвращает индекс части, которая отвечает за ссылку на связь-конец (последняя часть-значение).</summary>
        int TargetPart
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion

        #region Flow control

        /// <summary>Возвращает значение, обозначающее продолжение прохода по связям.</summary>
        /// <remarks>Используется в функции обработчике, который передаётся в функцию Each.</remarks>
        TLinkAddress Continue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Возвращает значение, обозначающее остановку прохода по связям.</summary>
        /// <remarks>Используется в функции обработчике, который передаётся в функцию Each.</remarks>
        TLinkAddress Break
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Возвращает значение, обозначающее пропуск в проходе по связям.</summary>
        TLinkAddress Skip
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion

        #region Special symbols

        /// <summary>Возвращает значение, обозначающее отсутствие связи.</summary>
        TLinkAddress Null
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Возвращает значение, обозначающее любую связь.</summary>
        /// <remarks>Возможно нужно зарезервировать отдельное значение, тогда можно будет создавать все варианты последовательностей в функции Create.</remarks>
        TLinkAddress Any
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Возвращает значение, обозначающее связь-ссылку на саму связь.</summary>
        TLinkAddress Itself
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        TLinkAddress Error
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion

        #region References

        /// <summary>Возвращает диапазон возможных индексов для внутренних связей (внутренних ссылок).</summary>
        Range<TLinkAddress> InternalReferencesRange
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>Возвращает диапазон возможных индексов для внешних связей (внешних ссылок).</summary>
        Range<TLinkAddress>? ExternalReferencesRange
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion
    }
}