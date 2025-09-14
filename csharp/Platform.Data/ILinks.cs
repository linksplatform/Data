using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Platform.Delegates;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    /// <summary>
    /// <para>Represents an interface for manipulating data in the Links (links storage) format.</para>
    /// <para>Представляет интерфейс для манипуляции с данными в формате Links (хранилища связей).</para>
    /// </summary>
    /// <remarks>
    /// <para>This interface is independent of the size of the content of the link, meaning it is suitable for both doublets, triplets, and link sequences of any size.</para>
    /// <para>Этот интерфейс не зависит от размера содержимого связи, а значит подходит как для дуплетов, триплетов и последовательностей связей любого размера.</para>
    /// </remarks>
    public interface ILinks<TLinkAddress, TConstants>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>
        where TConstants : LinksConstants<TLinkAddress>
    {
        #region Constants

        /// <summary>
        /// <para>Returns the set of constants that is necessary for effective communication with the methods of this interface.</para>
        /// <para>Возвращает набор констант, который необходим для эффективной коммуникации с методами этого интерфейса.</para>
        /// </summary>
        /// <remarks>
        /// <para>These constants are not changed since the creation of the links storage access point.</para>
        /// <para>Эти константы не меняются с момента создания точки доступа к хранилищу связей.</para>
        /// </remarks>
        TConstants Constants
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        #endregion

        #region Read

        /// <summary>
        /// <para>Counts and returns the total number of links in the storage that meet the specified restriction.</para>
        /// <para>Подсчитывает и возвращает общее число связей находящихся в хранилище, соответствующих указанному ограничению.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links. If null is passed, returns 0 (no links match an empty restriction). To count all links, use a restriction array containing only Constants.Any.</para>
        /// <para>Ограничение на содержимое связей. Если передан null, возвращается 0 (ни одна связь не соответствует пустому ограничению). Чтобы подсчитать все связи, используйте массив ограничений, содержащий только Constants.Any.</para>
        /// </param>
        /// <returns><para>The total number of links in the storage that meet the specified restriction.</para><para>Общее число связей находящихся в хранилище, соответствующих указанному ограничению.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Count(IList<TLinkAddress>? restriction);

        /// <summary>
        /// <para>Passes through all the links matching the pattern, invoking a handler for each matching link.</para>
        /// <para>Выполняет проход по всем связям, соответствующим шаблону, вызывая обработчик (handler) для каждой подходящей связи.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links. If null is passed, no links are processed. Each constraint in a non-null restriction can have values: Constants.Null - the 0th link denoting a reference to the void, Constants.Any - matches any link in that position, 1..∞ a specific link index.</para>
        /// <para>Ограничение на содержимое связей. Если передан null, ни одна связь не обрабатывается. Каждое ограничение в не-null ограничении может иметь значения: Constants.Null - 0-я связь, обозначающая ссылку на пустоту, Constants.Any - соответствует любой связи в этой позиции, 1..∞ конкретный индекс связи.</para>
        /// </param>
        /// <param name="handler"><para>A handler for each matching link.</para><para>Обработчик для каждой подходящей связи.</para></param>
        /// <returns><para>Constants.Continue, if the pass through the links was not interrupted, and Constants.Break otherwise.</para><para>Constants.Continue, в случае если проход по связям не был прерван и Constants.Break в обратном случае.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Each(IList<TLinkAddress>? restriction, ReadHandler<TLinkAddress>? handler);

        #endregion

        #region Write

        /// <summary>
        /// <para>Creates a link.</para>
        /// <para>Создаёт связь.</para>
        /// <param name="substitution">
        /// <para>The content of a new link. If null is passed, creates a point (self-referencing link). For other link types, provide an array with source and target addresses.</para>
        /// <para>Содержимое новой связи. Если передан null, создаётся точка (самосылающаяся связь). Для других типов связей предоставьте массив с адресами источника и цели.</para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can use Constants.Continue to continue proccess each change. Constants.Break can be used to stop receiving of executed changes.</para>
        /// <para>Функция для обработки каждого выполненного изменения. Эта функция может использовать Constants.Continue чтобы продолжить обрабатывать каждое изменение. Constants.Break может быть использована для остановки получения выполненных изменений.</para>
        /// </param>
        /// </summary>
        /// <returns>
        /// <para>
        /// Constants.Continue if all executed changes are handled.
        /// Constants.Break if proccessing of handled changes is stoped.
        /// </para>
        /// <para>
        /// Constants.Continue если все выполненные изменения обработаны.
        /// Constants.Break если обработака выполненных изменений остановлена.
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Create(IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler);

        /// <summary>
        /// Обновляет связь с указанными restriction[Constants.IndexPart] в адресом связи
        /// на связь с указанным новым содержимым.
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links. If null is passed, no links are updated (safer default).</para>
        /// <para>Ограничение на содержимое связей. Если передан null, ни одна связь не обновляется (более безопасное поведение по умолчанию).</para>
        /// <para>Expected format: restriction[Constants.IndexPart] contains the link address, followed by link content.</para>
        /// <para>Предполагается, что будет указан индекс связи (в restriction[Constants.IndexPart]) и далее за ним будет следовать содержимое связи.</para>
        /// <para>Each constraint can have values: Constants.Null - the 0th link (void reference), Constants.Itself - self-reference requirement, 1..∞ specific link index.</para>
        /// <para>Каждое ограничение может иметь значения: Constants.Null - 0-я связь, обозначающая ссылку на пустоту, Constants.Itself - требование установить ссылку на себя, 1..∞ конкретный индекс другой связи.</para>
        /// </param>
        /// <param name="substitution"></param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can use Constants.Continue to continue proccess each change. Constants.Break can be used to stop receiving of executed changes.</para>
        /// <para>Функция для обработки каждого выполненного изменения. Эта функция может использовать Constants.Continue чтобы продолжить обрабатывать каждое изменение. Constants.Break может быть использована для остановки получения выполненных изменений.</para>
        /// </param>
        /// <returns>
        /// <para>
        /// Constants.Continue if all executed changes are handled.
        /// Constants.Break if proccessing of handled changes is stoped.
        /// </para>
        /// <para>
        /// Constants.Continue если все выполненные изменения обработаны.
        /// Constants.Break если обработака выполненных изменений остановлена.
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Update(IList<TLinkAddress>? restriction, IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler);

        /// <summary>
        /// <para>Deletes links that match the specified restriction.</para>
        /// <para>Удаляет связи соответствующие указанному ограничению.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the content of links to delete. IMPORTANT: If null is passed, NO links are deleted (safe default). To delete all links, explicitly pass an array containing only Constants.Any. To delete specific links, provide their addresses or patterns.</para>
        /// <para>Ограничение на содержимое связей для удаления. ВАЖНО: Если передан null, НИ ОДНА связь не удаляется (безопасное поведение по умолчанию). Чтобы удалить все связи, явно передайте массив, содержащий только Constants.Any. Чтобы удалить конкретные связи, предоставьте их адреса или шаблоны.</para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can use Constants.Continue to continue proccess each change. Constants.Break can be used to stop receiving of executed changes.</para>
        /// <para>Функция для обработки каждого выполненного изменения. Эта функция может использовать Constants.Continue чтобы продолжить обрабатывать каждое изменение. Constants.Break может быть использована для остановки получения выполненных изменений.</para>
        /// </param>
        /// <returns>
        /// <para>
        /// Constants.Continue if all executed changes are handled.
        /// Constants.Break if proccessing of handled changes is stoped.
        /// </para>
        /// <para>
        /// Constants.Continue если все выполненные изменения обработаны.
        /// Constants.Break если обработака выполненных изменений остановлена.
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Delete(IList<TLinkAddress>? restriction, WriteHandler<TLinkAddress>? handler);

        #endregion
    }
}
