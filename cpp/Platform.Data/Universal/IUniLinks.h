

// ReSharper disable TypeParameterCanBeVariant

namespace Platform::Data::Universal
{
    public partial interface IUniLinks<TLinkAddress>
    {
        IList<IList<IList<TLinkAddress>>> Trigger(IList<TLinkAddress> &condition, IList<TLinkAddress> &substitution);
    }

    public partial interface IUniLinks<TLinkAddress>
    {
        TLinkAddress Trigger(IList<TLinkAddress> &patternOrCondition, Func<IList<TLinkAddress>, TLinkAddress> matchHandler,
                      IList<TLinkAddress> &substitution, Func<IList<TLinkAddress>, IList<TLinkAddress>, TLinkAddress> substitutionHandler);

        TLinkAddress Trigger(IList<TLinkAddress> &restriction, Func<IList<TLinkAddress>, IList<TLinkAddress>, TLinkAddress> matchedHandler,
              IList<TLinkAddress> &substitution, Func<IList<TLinkAddress>, IList<TLinkAddress>, TLinkAddress> substitutedHandler);
    }

    public partial interface IUniLinks<TLinkAddress>
    {
        virtual TLinkAddress Count(IList<TLinkAddress> &restriction) = 0;
    }
}
