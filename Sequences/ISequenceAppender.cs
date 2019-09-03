#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Sequences
{
    public interface ISequenceAppender<TLinkAddress>
    {
        TLinkAddress Append(TLinkAddress sequence, TLinkAddress appendant);
    }
}
