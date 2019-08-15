#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Constants
{
    public interface ILinksCombinedConstants<TDecision, TAddress, TPartIndex, TLinksConstants> :
        ILinksDecisionConstants<TDecision>,
        ILinksAddressConstants<TAddress>,
        ILinksPartConstants<TPartIndex>
        where TLinksConstants : ILinksDecisionConstants<TDecision>, ILinksAddressConstants<TAddress>, ILinksPartConstants<TPartIndex>
    {
    }
}
