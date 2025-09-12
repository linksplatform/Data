#pragma once
#include <concepts>
#include <vector>
#include <type_traits>

namespace Platform::Data
{
    /// <summary>
    /// <para>Concept that defines the core requirements for ILinks interface implementation.</para>
    /// <para>Концепт, который определяет основные требования для реализации интерфейса ILinks.</para>
    /// </summary>
    /// <remarks>
    /// <para>This concept includes only the main methods of ILinks without extensions.</para>
    /// <para>Этот концепт включает только основные методы ILinks без расширений.</para>
    /// <para>Based on selective polymorphism approach as suggested in the issue comment.</para>
    /// <para>Основан на подходе селективного полиморфизма, как предложено в комментарии к задаче.</para>
    /// </remarks>
    template<typename T>
    concept CLinks = requires(T links, 
                              const std::vector<typename T::LinkAddressType>& restriction,
                              const std::vector<typename T::LinkAddressType>& substitution,
                              const typename T::ReadHandlerType& readHandler,
                              const typename T::WriteHandlerType& writeHandler)
    {
        /// <summary>
        /// <para>Link address type used by the implementation.</para>
        /// <para>Тип адреса связи, используемый реализацией.</para>
        /// </summary>
        typename T::LinkAddressType;
        
        /// <summary>
        /// <para>Read handler type for traversal operations.</para>
        /// <para>Тип обработчика чтения для операций обхода.</para>
        /// </summary>
        typename T::ReadHandlerType;
        
        /// <summary>
        /// <para>Write handler type for modification operations.</para>
        /// <para>Тип обработчика записи для операций изменения.</para>
        /// </summary>
        typename T::WriteHandlerType;
        
        /// <summary>
        /// <para>Constants that provide necessary values for effective communication with interface methods.</para>
        /// <para>Константы, которые предоставляют необходимые значения для эффективной коммуникации с методами интерфейса.</para>
        /// </summary>
        T::Constants;
        
        /// <summary>
        /// <para>Counts and returns the total number of links in the storage that meet the specified restriction.</para>
        /// <para>Подсчитывает и возвращает общее число связей находящихся в хранилище, соответствующих указанному ограничению.</para>
        /// </summary>
        /// <param name="restriction">Restriction on the contents of links.</param>
        /// <returns>The total number of links in the storage that meet the specified restriction.</returns>
        { links.Count(restriction) } -> std::convertible_to<typename T::LinkAddressType>;
        
        /// <summary>
        /// <para>Passes through all the links matching the pattern, invoking a handler for each matching link.</para>
        /// <para>Выполняет проход по всем связям, соответствующим шаблону, вызывая обработчик для каждой подходящей связи.</para>
        /// </summary>
        /// <param name="restriction">Restriction on the contents of links.</param>
        /// <param name="handler">A handler for each matching link.</param>
        /// <returns>Constants.Continue if the pass through the links was not interrupted, and Constants.Break otherwise.</returns>
        { links.Each(restriction, readHandler) } -> std::convertible_to<typename T::LinkAddressType>;
        
        /// <summary>
        /// <para>Creates a link.</para>
        /// <para>Создаёт связь.</para>
        /// </summary>
        /// <param name="substitution">The content of a new link.</param>
        /// <param name="handler">A function to handle each executed change.</param>
        /// <returns>Constants.Continue if all executed changes are handled, Constants.Break if processing is stopped.</returns>
        { links.Create(substitution, writeHandler) } -> std::convertible_to<typename T::LinkAddressType>;
        
        /// <summary>
        /// <para>Updates links that match the specified restriction with new content.</para>
        /// <para>Обновляет связи, соответствующие указанному ограничению, новым содержимым.</para>
        /// </summary>
        /// <param name="restriction">Restriction on the contents of links to update.</param>
        /// <param name="substitution">New content for the links.</param>
        /// <param name="handler">A function to handle each executed change.</param>
        /// <returns>Constants.Continue if all executed changes are handled, Constants.Break if processing is stopped.</returns>
        { links.Update(restriction, substitution, writeHandler) } -> std::convertible_to<typename T::LinkAddressType>;
        
        /// <summary>
        /// <para>Deletes links that match the specified restriction.</para>
        /// <para>Удaляет связи соответствующие указанному ограничению.</para>
        /// </summary>
        /// <param name="restriction">Restriction on the content of links to delete.</param>
        /// <param name="handler">A function to handle each executed change.</param>
        /// <returns>Constants.Continue if all executed changes are handled, Constants.Break if processing is stopped.</returns>
        { links.Delete(restriction, writeHandler) } -> std::convertible_to<typename T::LinkAddressType>;
    };
}