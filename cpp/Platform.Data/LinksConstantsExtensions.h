namespace Platform::Data
{
    template <typename TLinkAddress, LinksConstants<TLinkAddress> VLinksConstants>
    static bool IsInternalReference(TLinkAddress linkAddress) noexcept
    {
        return VLinksConstants.InternalReferencesRange.Contains(linkAddress);
    }

    template <typename TLinkAddress, LinksConstants<TLinkAddress> VLinksConstants>
    static bool IsReference(TLinkAddress linkAddress) noexcept
    {
        return IsInternalReference(VLinksConstants, linkAddress) || IsExternalReference(VLinksConstants, linkAddress);
    }

    template <typename TLinkAddress, LinksConstants<TLinkAddress> VLinksConstants>
    static bool IsExternalReference(TLinkAddress linkAddress) noexcept
    {
        auto&& range = VLinksConstants.ExternalReferencesRange;
        return range.Contains(linkAddress);
    }
}
