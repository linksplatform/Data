using System.Collections.Generic;
using Platform.Delegates;

namespace Platform.Data
{
    public struct WriteHandlerState<TLinkAddress> where TLinkAddress : struct
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
            if (_equalityComparer.Equals(Break, Result))
            {
                return;
            }
            Handler = null;
            Result = Break;
        }

        public TLinkAddress Handle(IList<TLinkAddress> before, IList<TLinkAddress> after)
        {
            return Handler?.Invoke(before, after) ?? Result;
        }
    }
}


