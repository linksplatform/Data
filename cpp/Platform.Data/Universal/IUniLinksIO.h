

// ReSharper disable TypeParameterCanBeVariant

namespace Platform::Data::Universal
{
    template <typename ...> class IUniLinksIO;
    template <typename TLinkAddress> class IUniLinksIO<TLinkAddress>
    {
    public:
        virtual bool Out(Func<IList<TLinkAddress>, bool> handler, IList<TLinkAddress> &pattern) = 0;

        virtual TLinkAddress In(IList<TLinkAddress> &before, IList<TLinkAddress> &after) = 0;
    };
}