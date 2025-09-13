using System.Collections.Generic;
using System.Numerics;
using Platform.Delegates;

namespace Platform.Data
{
    /// <summary>
    /// <para>
    /// Represents a state manager for write handler operations in the links system.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type used to represent link addresses.</para>
    /// <para></para>
    /// </typeparam>
    public struct WriteHandlerState<TLinkAddress>  where TLinkAddress : IUnsignedNumber<TLinkAddress>
    {
        /// <summary>
        /// <para>
        /// Gets or sets the result of the write handler operation.
        /// </para>
        /// <para></para>
        /// </summary>
        public TLinkAddress Result;
        
        /// <summary>
        /// <para>
        /// Gets or sets the write handler function used to process link changes.
        /// </para>
        /// <para></para>
        /// </summary>
        public WriteHandler<TLinkAddress>? Handler;
        
        private TLinkAddress Break;

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="WriteHandlerState{TLinkAddress}"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="continue">
        /// <para>A value indicating that processing should continue.</para>
        /// <para></para>
        /// </param>
        /// <param name="break">
        /// <para>A value indicating that processing should break/stop.</para>
        /// <para></para>
        /// </param>
        /// <param name="handler">
        /// <para>A handler function for processing write operations.</para>
        /// <para></para>
        /// </param>
        public WriteHandlerState(TLinkAddress @continue, TLinkAddress @break, WriteHandler<TLinkAddress>? handler)
        {
            Break = @break;
            Result = @continue;
            Handler = handler;
        }

        /// <summary>
        /// <para>
        /// Applies the specified result and updates the handler state accordingly.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="result">
        /// <para>The result value to apply to the state.</para>
        /// <para></para>
        /// </param>
        public void Apply(TLinkAddress result)
        {
            var isAlreadyBreak = (Break == Result);
            var isCurrentlyBreak = (Break == result);
            if (isAlreadyBreak || !isCurrentlyBreak)
            {
                return;
            }
            Handler = null;
            Result = Break;

        }

        /// <summary>
        /// <para>
        /// Handles the write operation with the specified before and after link states.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="before">
        /// <para>The link state before the operation.</para>
        /// <para></para>
        /// </param>
        /// <param name="after">
        /// <para>The link state after the operation.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The result of the handler operation.</para>
        /// <para></para>
        /// </returns>
        public TLinkAddress Handle(IList<TLinkAddress> before, IList<TLinkAddress> after)
        {
            if (Handler != null)
            {
                Apply(Handler(before, after));
            }
            return Result;
        }
    }
}


