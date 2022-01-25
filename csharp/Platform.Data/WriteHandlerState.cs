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
            if (_equalityComparer.Equals(Break, Result))
            {
                return;
            }
            if (!_equalityComparer.Equals(Break, result))
            {
                return;
            }
            Handler = null;
            Result = Break;
        }
    }
}


