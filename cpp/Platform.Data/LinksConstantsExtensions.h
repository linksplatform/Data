namespace Platform::Data
{
    template <typename TLinksConstants>
    static bool IsInternalReference(typename TLinksConstants::LinkAddressType linkAddress) noexcept
    {
        static constexpr TLinksConstants constants{true};
        return constants.InternalReferencesRange.Contains(linkAddress);
    }

    template <typename TLinksConstants>
    static bool IsReference(typename TLinksConstants::LinkAddressType linkAddress) noexcept
    {
        return IsInternalReference<TLinksConstants>(linkAddress) || IsExternalReference<TLinksConstants>(linkAddress);
    }

    template <typename TLinksConstants>
    static bool IsExternalReference(typename TLinksConstants::LinkAddressType linkAddress) noexcept
    {
        static constexpr TLinksConstants constants{true};
        auto&& range = constants.ExternalReferencesRange;
        return range.Contains(linkAddress);
    }
}
