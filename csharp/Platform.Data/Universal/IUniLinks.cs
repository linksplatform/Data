using System;
using System.Collections.Generic;
using Platform.Delegates;

// ReSharper disable TypeParameterCanBeVariant
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Universal
{
    /// <remarks>Minimal sufficient universal Links API (for bulk operations).</remarks>
    public partial interface IUniLinks<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Triggers the condition.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="condition">
        /// <para>The condition.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitution">
        /// <para>The substitution.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A list of i list i list t link address</para>
        /// <para></para>
        /// </returns>
        IList<IList<IList<TLinkAddress>?>> Trigger(IList<TLinkAddress>? condition, IList<TLinkAddress>? substitution);
    }

    /// <summary>
    /// <para>
    /// Defines the universal links interface for step by step operations.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <remarks>Minimal sufficient universal Links API (for step by step operations).</remarks>
    public partial interface IUniLinks<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Triggers an operation based on pattern matching and substitution with handlers.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="patternOrCondition">
        /// <para>The pattern or condition for matching links.</para>
        /// <para></para>
        /// </param>
        /// <param name="matchHandler">
        /// <para>The handler function called for each matched link.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitution">
        /// <para>The substitution content to apply.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitutionHandler">
        /// <para>The handler function called for each substitution operation.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// TLinkAddress that represents True (was finished fully) or TLinkAddress that represents False (was stopped).
        /// This is done to assure ability to push up stop signal through recursion stack.
        /// </returns>
        /// <remarks>
        /// { 0, 0, 0 } => { itself, itself, itself } // create
        /// { 1, any, any } => { itself, any, 3 } // update
        /// { 3, any, any } => { 0, 0, 0 } // delete
        /// </remarks>
        TLinkAddress Trigger(IList<TLinkAddress>? patternOrCondition, ReadHandler<TLinkAddress>? matchHandler,
                      IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? substitutionHandler);

        /// <summary>
        /// <para>
        /// Triggers the restriction.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="restriction">
        /// <para>The restriction.</para>
        /// <para></para>
        /// </param>
        /// <param name="matchedHandler">
        /// <para>The matched handler.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitution">
        /// <para>The substitution.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitutedHandler">
        /// <para>The substituted handler.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The link address</para>
        /// <para></para>
        /// </returns>
        TLinkAddress Trigger(IList<TLinkAddress>? restriction, WriteHandler<TLinkAddress>? matchedHandler,
              IList<TLinkAddress>? substitution, WriteHandler<TLinkAddress>? substitutedHandler);
    }

    /// <summary>
    /// <para>
    /// Defines the universal links interface extended with optimization methods.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <remarks>Extended with small optimization.</remarks>
    public partial interface IUniLinks<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Counts the number of links that match the specified restrictions.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="restrictions">
        /// <para>The restrictions to apply when counting links.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The count of links matching the restrictions.</para>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// Something simple should be simple and optimized.
        /// </remarks>
        TLinkAddress Count(IList<TLinkAddress>? restrictions);
    }
}
