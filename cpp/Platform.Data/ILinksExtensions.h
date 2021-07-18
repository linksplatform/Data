namespace Platform::Data
{
    template<typename Self, typename TLinkAddress, typename TConstants>
    static TLinkAddress Count(const ILinks<Self, TLinkAddress, TConstants>& links, std::convertible_to<TLinkAddress> auto... restrictions)
    // TODO: later create noexcept(expr)
    {
        TLinkAddress array[] = { static_cast<TLinkAddress>(restrictions)... };
        return links.Count(array);
    }

    template<typename Self, typename TLinkAddress, typename TConstants>
    static bool Exists(const ILinks<Self, TLinkAddress, TConstants>&& links, TLinkAddress link) noexcept
    {
        auto&& constants = links.Constants;
        return IsExternalReference(constants, link) || (IsInternalReference(constants, link) && links.Count(LinkAddress(link)) == 0);
    }

    template<typename Self, typename TLinkAddress, typename TConstants>
    static void EnsureLinkExists(const ILinks<Self, TLinkAddress, TConstants>& links, TLinkAddress link)
    {
        if (not Exists(links, link))
        {
            throw ArgumentLinkDoesNotExistsException<TLinkAddress>(link);
        }
    }

    template<typename Self, typename TLinkAddress, typename TConstants>
    static void EnsureLinkExists(ILinks<Self, TLinkAddress, TConstants> &links, TLinkAddress link, const std::string& argumentName)
    {
        if (!links.Exists(link))
        {
            throw ArgumentLinkDoesNotExistsException<TLinkAddress>(link, argumentName);
        }
    }

    template<typename Self, typename TLinkAddress, typename TConstants>
    static TLinkAddress Each(ILinks<Self, TLinkAddress, TConstants>& links, auto&& handler, std::convertible_to<TLinkAddress> auto... restrictions)
    // TODO: later create noexcept(expr)
    {
        TLinkAddress array[] = { static_cast<TLinkAddress>(restrictions)... };
        return links.Each(handler, array);
    }

    template<typename Self, typename TLinkAddress, typename TConstants>
    static Interfaces::IArray<TLinkAddress> auto&& GetLink(ILinks<Self, TLinkAddress, TConstants>& links, TLinkAddress link)
    {
        auto&& constants = links.Constants;
        if (IsExternalReference(constants, link))
        {
            return Point(link, constants.TargetPart + 1);
        }

        //auto linkPartsSetter = Setter<IList<TLinkAddress>, TLinkAddress>(constants.Continue, constants.Break);
        //links.Each(linkPartsSetter.SetAndReturnTrue, link);
        //return linkPartsSetter.Result;

        std::vector<TLinkAddress> wrapper;
        links.Each([&wrapper, &constants](Interfaces::IArray<TLinkAddress> auto&& link)
        {
            wrapper = std::vector(std::ranges::begin(link), std::ranges::end(link));
            return constants.Continue;
        }, link);
        return wrapper;
    }

    template<typename Self, typename TLinkAddress, typename TConstants>
    static bool IsFullPoint(ILinks<Self, TLinkAddress, TConstants>& links, TLinkAddress link)
    {
        if (IsExternalReference(links.Constants, link))
        {
            return true;
        }
        EnsureLinkExists(links, link);
        return Point<TLinkAddress>::IsFullPoint(GetLink(links, link));
    }

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
