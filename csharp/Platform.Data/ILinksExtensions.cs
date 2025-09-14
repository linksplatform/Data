using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Platform.Setters;
using Platform.Data.Exceptions;
using Platform.Delegates;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    /// <summary>
    /// <para>
    /// Represents the links extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class ILinksExtensions
    {
        public static TLinkAddress Create<TLinkAddress>(this ILinks<TLinkAddress, LinksConstants<TLinkAddress>> links) where TLinkAddress : IUnsignedNumber<TLinkAddress> => links.Create(null);

        public static TLinkAddress Create<TLinkAddress>(this ILinks<TLinkAddress, LinksConstants<TLinkAddress>> links, IList<TLinkAddress>? substitution) where TLinkAddress : IUnsignedNumber<TLinkAddress>
        {
            var constants = links.Constants;
            Setter<TLinkAddress, TLinkAddress> setter = new Setter<TLinkAddress, TLinkAddress>(constants.Continue, constants.Break, constants.Null);
            links.Create(substitution, setter.SetFirstFromNonNullSecondListAndReturnTrue);
            return setter.Result;
        }

        public static TLinkAddress Update<TLinkAddress>(this ILinks<TLinkAddress, LinksConstants<TLinkAddress>> links, IList<TLinkAddress>? restriction, IList<TLinkAddress>? substitution) where TLinkAddress : IUnsignedNumber<TLinkAddress>
        {
            var constants = links.Constants;
            Setter<TLinkAddress, TLinkAddress> setter = new(constants.Continue, constants.Break, constants.Null);
            links.Update(restriction, substitution, setter.SetFirstFromNonNullSecondListAndReturnTrue);
            return setter.Result;
        }

        public static TLinkAddress Delete<TLinkAddress>(this ILinks<TLinkAddress, LinksConstants<TLinkAddress>> links, TLinkAddress linkToDelete) where TLinkAddress : IUnsignedNumber<TLinkAddress> => Delete(links, (IList<TLinkAddress>?)new LinkAddress<TLinkAddress>(linkToDelete));

        public static TLinkAddress Delete<TLinkAddress>(this ILinks<TLinkAddress, LinksConstants<TLinkAddress>> links, IList<TLinkAddress>? restriction) where TLinkAddress : IUnsignedNumber<TLinkAddress>
        {
            var constants = links.Constants;
            Setter<TLinkAddress, TLinkAddress> setter = new Setter<TLinkAddress, TLinkAddress>(constants.Continue, constants.Break, constants.Null);
            links.Delete(restriction, setter.SetFirstFromNonNullFirstListAndReturnTrue);
            return setter.Result;
        }

        /// <summary>
        /// <para>
        /// Counts the links.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TLinkAddress">
        /// <para>The link address.</para>
        /// <para></para>
        /// </typeparam>
        /// <typeparam name="TConstants">
        /// <para>The constants.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="links">
        /// <para>The links.</para>
        /// <para></para>
        /// </param>
        /// <param name="restrictions">
        /// <para>The restrictions.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link address</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TLinkAddress Count<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, params TLinkAddress[] restrictions) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
            => links.Count(restrictions);

        /// <summary>
        /// Returns a value indicating whether a link with the specified index exists in the links storage.
        /// </summary>
        /// <param name="links">The links storage.</param>
        /// <param name="link">The index of the link being checked for existence.</param>
        /// <returns>A value indicating whether the link exists.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Exists<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, TLinkAddress link) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
        {
            var constants = links.Constants;
            return constants.IsExternalReference(link) || (constants.IsInternalReference(link) && Comparer<TLinkAddress>.Default.Compare(links.Count(new LinkAddress<TLinkAddress>(link)), default) > 0);
        }

        /// <param name="links">The links storage.</param>
        /// <param name="link">The index of the link being checked for existence.</param>
        /// <remarks>
        /// TODO: May be move to EnsureExtensions or make it both there and here
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnsureLinkExists<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, TLinkAddress link) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (!links.Exists(link))
            {
                throw new ArgumentLinkDoesNotExistsException<TLinkAddress>(link);
            }
        }

        /// <param name="links">The links storage.</param>
        /// <param name="link">The index of the link being checked for existence.</param>
        /// <param name="argumentName">The name of the argument to which the link index is passed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnsureLinkExists<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, TLinkAddress link, string argumentName) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (!links.Exists(link))
            {
                throw new ArgumentLinkDoesNotExistsException<TLinkAddress>(link, argumentName);
            }
        }

        /// <summary>
        /// Performs traversal of all links matching the pattern, calling the handler for each suitable link.
        /// </summary>
        /// <param name="links">The links storage.</param>
        /// <param name="handler">The handler for each suitable link.</param>
        /// <param name="restrictions">Restrictions on the contents of links. Each restriction can have values: Constants.Null - the 0th link, representing a reference to emptiness, Any - no restriction, 1..∞ specific link index.</param>
        /// <returns>True if the link traversal was not interrupted, and False otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TLinkAddress Each<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, ReadHandler<TLinkAddress>? handler, params TLinkAddress[] restrictions) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
            => links.Each(restrictions, handler);

        /// <summary>
        /// Returns the part-values for the link with the specified index.
        /// </summary>
        /// <param name="links">The links storage.</param>
        /// <param name="link">The link index.</param>
        /// <returns>The unique link.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<TLinkAddress>? GetLink<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, TLinkAddress link) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
        {
            var constants = links.Constants;
            if (constants.IsExternalReference(link))
            {
                return new Point<TLinkAddress>(link, constants.TargetPart + 1);
            }
            var linkPartsSetter = new Setter<IList<TLinkAddress>?, TLinkAddress>(constants.Continue, constants.Break);
            links.Each(linkPartsSetter.SetAndReturnTrue, link);
            return linkPartsSetter.Result;
        }

        #region Points

        /// <summary>Returns a value indicating whether the link with the specified index is a full point (a link closed on itself twice).</summary>
        /// <param name="links">The links storage.</param>
        /// <param name="link">The index of the link being checked.</param>
        /// <returns>A value indicating whether the link is a full point.</returns>
        /// <remarks>
        /// Связь точка - это связь, у которой начало (Source) и конец (Target) есть сама эта связь.
        /// Но что, если точка уже есть, а нужно создать пару с таким же значением? Должны ли точка и пара существовать одновременно?
        /// Или в качестве решения для точек нужно использовать 0 в качестве начала и конца, а сортировать по индексу в массиве связей?
        /// Какое тогда будет значение Source и Target у точки? 0 или её индекс?
        /// Или точка должна быть одновременно точкой и парой, а также последовательностями из самой себя любого размера?
        /// Как только есть ссылка на себя, появляется этот парадокс, причём достаточно даже одной ссылки на себя (частичной точки).
        /// А что если не выбирать что является точкой, пара нулей (цикл через пустоту) или 
        /// самостоятельный цикл через себя? Что если предоставить все варианты использования связей?
        /// Что если разрешить и нули, а так же частичные варианты?
        /// 
        /// Что если точка, это только в том случае когда link.Source == link &amp;&amp; link.Target == link , т.е. дважды ссылка на себя.
        /// А пара это тогда, когда link.Source == link.Target &amp;&amp; link.Source != link , т.е. ссылка не на себя а во вне.
        /// 
        /// Тогда если у нас уже создана пара, но нам нужна точка, мы можем используя промежуточную связь,
        /// например "DoubletOf" обозначить что является точно парой, а что точно точкой.
        /// И наоборот этот же метод поможет, если уже существует точка, но нам нужна пара.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFullPoint<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, TLinkAddress link) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (links.Constants.IsExternalReference(link))
            {
                return true;
            }
            links.EnsureLinkExists(link);
            return Point<TLinkAddress>.IsFullPoint(links.GetLink(link));
        }

        /// <summary>Returns a value indicating whether the link with the specified index is a partial point (a link closed on itself at least once).</summary>
        /// <param name="links">The links storage.</param>
        /// <param name="link">The index of the link being checked.</param>
        /// <returns>A value indicating whether the link is a partial point.</returns>
        /// <remarks>
        /// Достаточно любой одной ссылки на себя.
        /// Также в будущем можно будет проверять и всех родителей, чтобы проверить есть ли ссылки на себя (на эту связь).
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPartialPoint<TLinkAddress, TConstants>(this ILinks<TLinkAddress, TConstants> links, TLinkAddress link) where TLinkAddress : IUnsignedNumber<TLinkAddress>
            where TConstants : LinksConstants<TLinkAddress>
        {
            if (links.Constants.IsExternalReference(link))
            {
                return true;
            }
            links.EnsureLinkExists(link);
            return Point<TLinkAddress>.IsPartialPoint(links.GetLink(link));
        }

        #endregion
    }
}
