using System.Collections.Generic;
using Platform.Delegates;

namespace Platform.Data
{
    public struct WriteHandlerState<TLink>
    {
        private readonly EqualityComparer<TLink> _equalityComparer;
        public TLink Result;
        public WriteHandler<TLink>? Handler;
        public TLink Continue;
        public TLink Break;

        public WriteHandlerState(TLink @continue, TLink @break, WriteHandler<TLink>? handler)
        {
            _equalityComparer = EqualityComparer<TLink>.Default;
            Continue = @continue;
            Break = @break;
            Result = @continue;
            Handler = handler;
        }

        public void Apply(TLink result)
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


