namespace Platform::Data
{










    template<typename Self, typename TLinkAddress, typename TConstants>
    static bool IsPartialPoint(const ILinks<Self, TLinkAddress, TConstants> &links, TLinkAddress link)
    {
        if (IsExternalReference(links.Constants, link))
        {
            return true;
        }
        EnsureLinkExists(links, link);
        return Point<TLinkAddress>::IsPartialPoint(GetLink(links, link));
    }
}
