using System;
using System.Collections.Generic;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    /// <summary>
    /// <para>Provides a base class for implementing dynamic polymorphic links storage.</para>
    /// <para>Предоставляет базовый класс для реализации динамического полиморфного хранилища связей.</para>
    /// </summary>
    /// <remarks>
    /// <para>This abstract class enables runtime polymorphism by providing virtual methods that can be overridden by derived classes.</para>
    /// <para>Этот абстрактный класс обеспечивает полиморфизм времени выполнения, предоставляя виртуальные методы, которые могут быть переопределены в производных классах.</para>
    /// </remarks>
    public abstract class LinksBase : IDynamicLinks
    {
        /// <summary>
        /// <para>Counts and returns the total number of links in the storage that meet the specified restriction.</para>
        /// <para>Подсчитывает и возвращает общее число связей находящихся в хранилище, соответствующих указанному ограничению.</para>
        /// </summary>
        /// <param name="restriction"><para>Restriction on the contents of links.</para><para>Ограничение на содержимое связей.</para></param>
        /// <returns><para>The total number of links in the storage that meet the specified restriction.</para><para>Общее число связей находящихся в хранилище, соответствующих указанному ограничению.</para></returns>
        public abstract object Count(IList<object>? restriction);

        /// <summary>
        /// <para>Passes through all the links matching the pattern, invoking a handler for each matching link.</para>
        /// <para>Выполняет проход по всем связям, соответствующим шаблону, вызывая обработчик для каждой подходящей связи.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links. Each constraint can have values: null - reference to void, any object - absence of constraint, specific values - concrete link indices.</para>
        /// <para>Ограничение на содержимое связей. Каждое ограничение может иметь значения: null - ссылка на пустоту, любой объект - отсутствие ограничения, конкретные значения - индексы связей.</para>
        /// </param>
        /// <param name="handler"><para>A handler for each matching link.</para><para>Обработчик для каждой подходящей связи.</para></param>
        /// <returns><para>Continue constant if the pass through the links was not interrupted, and Break constant otherwise.</para><para>Константа Continue, в случае если проход по связям не был прерван и константа Break в обратном случае.</para></returns>
        public abstract object Each(IList<object>? restriction, Delegate? handler);

        /// <summary>
        /// <para>Creates a link.</para>
        /// <para>Создаёт связь.</para>
        /// <param name="substitution">
        /// <para>The content of a new link. This argument is optional, if null is passed as value that means no content of a link is set.</para>
        /// <para>Содержимое новой связи. Этот аргумент опционален, если null передан в качестве значения это означает, что никакого содержимого для связи не установлено.</para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can return continue constant to continue processing each change. Break constant can be used to stop receiving executed changes.</para>
        /// <para>Функция для обработки каждого выполненного изменения. Эта функция может вернуть константу continue чтобы продолжить обрабатывать каждое изменение. Константа Break может быть использована для остановки получения выполненных изменений.</para>
        /// </param>
        /// </summary>
        /// <returns>
        /// <para>Continue constant if all executed changes are handled. Break constant if processing of handled changes is stopped.</para>
        /// <para>Константа Continue если все выполненные изменения обработаны. Константа Break если обработка выполненных изменений остановлена.</para>
        /// </returns>
        public abstract object Create(IList<object>? substitution, Delegate? handler);

        /// <summary>
        /// <para>Updates a link with the specified restriction to a link with the specified new content.</para>
        /// <para>Обновляет связь с указанным ограничением на связь с указанным новым содержимым.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links. It is assumed that the link index will be specified and then its content will follow.</para>
        /// <para>Ограничение на содержимое связей. Предполагается, что будет указан индекс связи и далее за ним будет следовать содержимое связи.</para>
        /// </param>
        /// <param name="substitution"><para>New content for the link.</para><para>Новое содержимое для связи.</para></param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can return continue constant to continue processing each change. Break constant can be used to stop receiving executed changes.</para>
        /// <para>Функция для обработки каждого выполненного изменения. Эта функция может вернуть константу continue чтобы продолжить обрабатывать каждое изменение. Константа Break может быть использована для остановки получения выполненных изменений.</para>
        /// </param>
        /// <returns>
        /// <para>Continue constant if all executed changes are handled. Break constant if processing of handled changes is stopped.</para>
        /// <para>Константа Continue если все выполненные изменения обработаны. Константа Break если обработка выполненных изменений остановлена.</para>
        /// </returns>
        public abstract object Update(IList<object>? restriction, IList<object>? substitution, Delegate? handler);

        /// <summary>
        /// <para>Deletes links that match the specified restriction.</para>
        /// <para>Удаляет связи соответствующие указанному ограничению.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the content of a link. This argument is optional, if null is passed as value that means no restriction on the content of a link is set.</para>
        /// <para>Ограничение на содержимое связи. Этот аргумент опционален, если null передан в качестве значения это означает, что никаких ограничений на содержимое связи не установлено.</para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can return continue constant to continue processing each change. Break constant can be used to stop receiving executed changes.</para>
        /// <para>Функция для обработки каждого выполненного изменения. Эта функция может вернуть константу continue чтобы продолжить обрабатывать каждое изменение. Константа Break может быть использована для остановки получения выполненных изменений.</para>
        /// </param>
        /// <returns>
        /// <para>Continue constant if all executed changes are handled. Break constant if processing of handled changes is stopped.</para>
        /// <para>Константа Continue если все выполненные изменения обработаны. Константа Break если обработка выполненных изменений остановлена.</para>
        /// </returns>
        public abstract object Delete(IList<object>? restriction, Delegate? handler);
    }
}