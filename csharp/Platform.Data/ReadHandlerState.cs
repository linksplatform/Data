using System.Collections.Generic;

namespace Platform.Data
{
    public struct ReadHandlerState<TLink>
    {
        private readonly EqualityComparer<TLink> _equalityComparer = EqualityComparer<TLink>.Default;
        public TLink Result;
        public ReadHandler<TLink> Handler;
        public TLink Continue;
        public TLink Break;

        public ReadHandlerState(TLink @continue, TLink @break, ReadHandler<TLink> handler)
        {
            Continue = @continue;
            Break = @break;
            Result = @continue;
            Handler = handler;
        }

        public void Apply(TLink result)
        {
            if (!_equalityComparer.Equals(Break, result))
            {
                return;
            }
            Handler = null;
            Result = Break;
        }
    }
}
