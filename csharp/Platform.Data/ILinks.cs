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
    /// </summary>
    /// <remarks>
    /// <para>This interface is independent of the size of the content of the link, meaning it is suitable for both doublets, triplets, and link sequences of any size.</para>
    /// </remarks>
    public interface ILinks<TLinkAddress, TConstants>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>
        where TConstants : LinksConstants<TLinkAddress>
    {
        #region Constants

        /// <summary>
        /// <para>Returns the set of constants that is necessary for effective communication with the methods of this interface.</para>
        /// </summary>
        /// <remarks>
        /// <para>These constants are not changed since the creation of the links storage access point.</para>
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
        /// </summary>
        /// <param name="restriction"><para>Restriction on the contents of links.</para></param>
        /// <returns><para>The total number of links in the storage that meet the specified restriction.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Count(IList<TLinkAddress>? restriction);

        /// <summary>
        /// <para>Passes through all the links matching the pattern, invoking a handler for each matching link.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the contents of links. Each constraint can have values: Constants.Null - the 0th link denoting a reference to the void, Any - the absence of a constraint, 1..∞ a specific link index.</para>
        /// </param>
        /// <param name="handler"><para>A handler for each matching link.</para></param>
        /// <returns><para>Constants.Continue, if the pass through the links was not interrupted, and Constants.Break otherwise.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Each(IList<TLinkAddress>? restriction, ReadHandler<TLinkAddress>? handler);

        #endregion

        #region Write

        /// <summary>
        /// <para>Creates a link.</para>
        /// </summary>
        /// <param name="substitution">
        /// <para>The content of a new link. This argument is optional, if the null passed as value that means no content of a link is set.</para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can use Constants.Continue to continue proccess each change. Constants.Break can be used to stop receiving of executed changes.</para>
        /// </param>
        /// <returns>
        /// <para>
        /// Constants.Continue if all executed changes are handled.
        /// Constants.Break if proccessing of handled changes is stoped.
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Create(IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler);

        /// <summary>
        /// <para>Updates a link with the specified restriction[Constants.IndexPart] as the link address to a link with the specified new content.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the content of links.</para>
        /// <para>It is assumed that a link index will be specified (in restriction[Constants.IndexPart]) followed by the link content.</para>
        /// <para>Each restriction can have values: Constants.Null - the 0th link denoting a reference to emptiness, Constants.Itself - requirement to set a reference to itself, 1..∞ specific index of another link.</para>
        /// </param>
        /// <param name="substitution"><para>The new content of the link.</para></param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can use Constants.Continue to continue proccess each change. Constants.Break can be used to stop receiving of executed changes.</para>
        /// </param>
        /// <returns>
        /// <para>
        /// Constants.Continue if all executed changes are handled.
        /// Constants.Break if proccessing of handled changes is stoped.
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Update(IList<TLinkAddress>? restriction, IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? handler);

        /// <summary>
        /// <para>Deletes links that match the specified restriction.</para>
        /// </summary>
        /// <param name="restriction">
        /// <para>Restriction on the content of a link. This argument is optional, if the null passed as value that means no restriction on the content of a link are set.</para>
        /// </param>
        /// <param name="handler">
        /// <para>A function to handle each executed change. This function can use Constants.Continue to continue proccess each change. Constants.Break can be used to stop receiving of executed changes.</para>
        /// </param>
        /// <returns>
        /// <para>
        /// Constants.Continue if all executed changes are handled.
        /// Constants.Break if proccessing of handled changes is stoped.
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        TLinkAddress Delete(IList<TLinkAddress>? restriction, WriteHandler<TLinkAddress>? handler);

        #endregion
    }
}
