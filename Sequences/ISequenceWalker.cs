using System.Collections.Generic;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Sequences
{
    public interface ISequenceWalker<TLink>
    {
        IEnumerable<IList<TLink>> Walk(TLink sequence);
    }
}
