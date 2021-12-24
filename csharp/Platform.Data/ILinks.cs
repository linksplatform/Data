using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
        /// <param name="restriction"><para>Restriction on the contents of links.</para><para>Ограничение на содержимое связей.</para></param>
        /// <returns><para>The total number of links in the storage that meet the specified restriction.</para><para>Общее число связей находящихся в хранилище, соответствующих указанному ограничению.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Count(IList<TLinkAddress> restriction);

        /// <summary>
        /// <para>Passes through all the links matching the pattern, invoking a handler for each matching link.</para>
        /// <para>Выполняет проход по всем связям, соответствующим шаблону, вызывая обработчик (handler) для каждой подходящей связи.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links. Each constraint can have values: Constants.Null - the 0th link denoting a reference to the void, Any - the absence of a constraint, 1..∞ a specific link index.</para>
        /// <para>Ограничение на содержимое связей. Каждое ограничение может иметь значения: Constants.Null - 0-я связь, обозначающая ссылку на пустоту, Any - отсутствие ограничения, 1..∞ конкретный индекс связи.</para>
        /// </param>
        /// <param name="handler"><para>A handler for each matching link.</para><para>Обработчик для каждой подходящей связи.</para></param>
        /// <returns><para>Constants.Continue, if the pass through the links was not interrupted, and Constants.Break otherwise.</para><para>Constants.Continue, в случае если проход по связям не был прерван и Constants.Break в обратном случае.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Each(IList<TLinkAddress> restriction, Func<IList<TLinkAddress>, TLinkAddress> handler);

        #endregion

        #region Write

        /// <summary>
        /// <para>Creates a link.</para>
        /// <para>Создаёт связь.</para>
        /// <param name="substitution">
        /// <para>The content of a new link. This argument is optional, if the null passed as value that means no content of a link is set.</para>
        /// <para>Содержимое новой связи. Этот аргумент опционален, если null передан в качестве значения это означает, что никакого содержимого для связи не установлено.</para>
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
        TLinkAddress Create(IList<TLinkAddress> substitution, Func<IList<TLinkAddress>, IList<TLinkAddress>, TLinkAddress> handler);

        /// <summary>
        /// Обновляет связь с указанными restriction[Constants.IndexPart] в адресом связи
        /// на связь с указанным новым содержимым.
        /// </summary>
        /// <param name="restriction">
        /// Ограничение на содержимое связей.
        /// Предполагается, что будет указан индекс связи (в restriction[Constants.IndexPart]) и далее за ним будет следовать содержимое связи.
        /// Каждое ограничение может иметь значения: Constants.Null - 0-я связь, обозначающая ссылку на пустоту,
        /// Constants.Itself - требование установить ссылку на себя, 1..∞ конкретный индекс другой связи.
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
        TLinkAddress Update(IList<TLinkAddress> restriction, IList<TLinkAddress> substitution, Func<IList<TLinkAddress>, IList<TLinkAddress>, TLinkAddress> handler);

        /// <summary>
        /// <para>Deletes links that match the specified restriction.</para>
        /// <para>Удаляет связи соответствующие указанному ограничению.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the content of a link. This argument is optional, if the null passed as value that means no restriction on the content of a link are set.</para>
        /// <para>Ограничение на содержимое связи. Этот аргумент опционален, если null передан в качестве значения это означает, что никаких ограничений на содержимое связи не установлено.</para>
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
        TLinkAddress Delete(IList<TLinkAddress> restriction, Func<IList<TLinkAddress>, IList<TLinkAddress>, TLinkAddress> handler);

        #endregion
    }
}
