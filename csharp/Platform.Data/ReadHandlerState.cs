using System.Collections.Generic;
using Platform.Delegates;

namespace Platform.Data
{
    public struct ReadHandlerState<TLinkAddress>
    {
        private readonly EqualityComparer<TLinkAddress> _equalityComparer;
        public TLinkAddress Result;
        public ReadHandler<TLinkAddress>? Handler;
        public TLinkAddress Continue;
        public TLinkAddress Break;

        public ReadHandlerState(TLinkAddress @continue, TLinkAddress @break, ReadHandler<TLinkAddress>? handler)
        {
            _equalityComparer = EqualityComparer<TLinkAddress>.Default;
            Continue = @continue;
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
