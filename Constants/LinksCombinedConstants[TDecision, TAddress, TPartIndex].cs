using Platform.Numbers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Constants
{
    public class LinksCombinedConstants<TDecision, TAddress, TPartIndex> : LinksCombinedConstants<TDecision, TAddress>, ILinksCombinedConstants<TDecision, TAddress, TPartIndex, LinksCombinedConstants<TDecision, TAddress, TPartIndex>>
    {
        public TPartIndex IndexPart { get; } = Integer<TPartIndex>.Zero;
        public TPartIndex SourcePart { get; } = Integer<TPartIndex>.One;
        public TPartIndex TargetPart { get; } = Integer<TPartIndex>.Two;
    }
}
