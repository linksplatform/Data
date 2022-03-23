namespace Platform::Data
{
    template <typename TLinkAddress>
    static bool IsInternalReference(const LinksConstants<TLinkAddress>& linksConstants, TLinkAddress address) noexcept
    {
        return linksConstants.InternalReferencesRange.Contains(address);
    }

    template <typename TLinkAddress>
    static bool IsReference(const LinksConstants<TLinkAddress>& linksConstants, TLinkAddress address) noexcept
    {
        return IsInternalReference(linksConstants, address) || IsExternalReference(linksConstants, address);
    }

    template <typename TLinkAddress>
    static bool IsExternalReference(const LinksConstants<TLinkAddress>& linksConstants, TLinkAddress address) noexcept
    {
        auto&& range = linksConstants.ExternalReferencesRange;
        return range.Contains(address);
    }
}
