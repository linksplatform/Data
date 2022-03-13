using System.Collections.Generic;
using Platform.Delegates;

namespace Platform.Data
{
    public struct WriteHandlerState<TLinkAddress>
    {
        private readonly EqualityComparer<TLinkAddress> _equalityComparer;
        public TLinkAddress Result;
        public WriteHandler<TLinkAddress>? Handler;
        private TLinkAddress Break;

        public WriteHandlerState(TLinkAddress @continue, TLinkAddress @break, WriteHandler<TLinkAddress>? handler)
        {
            _equalityComparer = EqualityComparer<TLinkAddress>.Default;
            Break = @break;
            Result = @continue;
            Handler = handler;
        }

        public void Apply(TLinkAddress result)
        {
            var isAlreadyBreak = _equalityComparer.Equals(Break, Result);
            var isCurrentlyBreak = _equalityComparer.Equals(Break, result);
            if (isAlreadyBreak || !isCurrentlyBreak)
            {
                return;
            }
            Handler = null;
            Result = Break;

        }

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


