namespace Platform::Data
{
    class LinksConstantsExtensions
    {
        public: template <typename TLinkAddress> static bool IsReference(LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) { return linksConstants.IsInternalReference(address) || linksConstants.IsExternalReference(address); }

        public: template <typename TLinkAddress> static bool IsInternalReference(LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) { return linksConstants.InternalReferencesRange.Contains(address); }

        public: template <typename TLinkAddress> static bool IsExternalReference(LinksConstants<TLinkAddress> linksConstants, TLinkAddress address) { return linksConstants.ExternalReferencesRange?.Contains(address) ?? false; }
    };
}
