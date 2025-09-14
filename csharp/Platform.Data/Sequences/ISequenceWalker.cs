using System;

namespace Platform.Data.Sequences
{
    public interface ISequenceWalker<TLink>
    {
        void WalkRight<T>(T sequence, Func<T, T> getSource, Func<T, T> getTarget, Func<T, bool> isElement, Action<T> visit);
        void WalkLeft<T>(T sequence, Func<T, T> getSource, Func<T, T> getTarget, Func<T, bool> isElement, Action<T> visit);
    }
}