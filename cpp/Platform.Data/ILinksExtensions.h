namespace Platform::Data
{
    template<typename TLinkAddress, typename TStorage>
    static TLinkAddress Create(TStorage storage)
    {
        constexpr std::array<TLinkAddress, 0> empty{};
        return storage.Create(empty);
    }

    template<typename TLinkAddress, typename TStorage>
    static TLinkAddress Count(const TStorage storage)
    // TODO: later add noexcept(expr)
    {
        constexpr std::array<TLinkAddress, 0> empty {};
        return storage.Count(empty);
    }

    template<typename TLinkAddress, typename TStorage>
    static TLinkAddress Count(const TStorage storage, std::convertible_to<TLinkAddress> auto... restrictions)
    // TODO: later add noexcept(expr)
    {
        TLinkAddress array[] = { static_cast<TLinkAddress>(restrictions)... };
        return storage.Count(array);
    }

    template<typename TLinkAddress, typename TStorage>
    static bool Exists(const TStorage storage, TLinkAddress link) noexcept
    {
        auto&& constants = storage.Constants;
        return IsExternalReference(constants, link) || (IsInternalReference(constants, link) && storage.Count(LinkAddress(link)) != 0);
    }

    template<typename TLinkAddress, typename TStorage>
    static void EnsureLinkExists(const TStorage storage, TLinkAddress link, const std::string& argument = {})
    {
        if (not storage.Exists(link))
        {
            throw ArgumentLinkDoesNotExistsException<TLinkAddress>(link, argument);
        }
    }

    template<typename TLinkAddress, typename TStorage>
    static TLinkAddress Each(const TStorage storage, auto&& handler, std::convertible_to<TLinkAddress> auto... restrictions)
    // TODO: later create noexcept(expr)
    {
        TLinkAddress array[] = { static_cast<TLinkAddress>(restrictions)... };
        return storage.Each(handler, array);
    }

    template<typename TLinkAddress, typename TStorage>
    static Interfaces::CArray auto&& GetLink(const TStorage storage, TLinkAddress link)
    {
        auto&& constants = storage.Constants;
        auto _continue = constants.Continue;
        if (IsExternalReference(constants, link))
        {
            return Point{link, constants.TargetPart + 1};
        }

        // TODO: dynamic polymorphism (for @Konard)
        //auto linkPartsSetter = Setter<IList<TLinkAddress>, TLinkAddress>(constants.Continue, constants.Break);
        //storage.Each(linkPartsSetter.SetAndReturnTrue, link);
        //return linkPartsSetter.Result;

        std::optional<std::vector<TLinkAddress>> resultLink {};
        storage.Each(link, [&resultLink, _continue](Interfaces::CArray auto&& link)
                   {
                       resultLink = { std::vector(std::ranges::begin(link), std::ranges::end(link)) };
                       return constants.Continue;
                   });
        Expects(resultLink.has_value());
        return resultLink;
    }

    template<typename TLinkAddress, typename TStorage>
    static bool IsFullPoint(const TStorage storage, TLinkAddress link)
    {
        if (IsExternalReference(storage.Constants, link))
        {
            return true;
        }
        storage.EnsureLinkExists(link);
        return Point<TLinkAddress>::IsFullPoint(storage.GetLink(link));
    }

    template<typename TLinkAddress, typename TStorage>
    static bool IsPartialPoint(const TStorage storage, TLinkAddress link)
    {
        if (IsExternalReference(storage.Constants, link))
        {
            return true;
        }
        storage.EnsureLinkExists(link);
        return Point<TLinkAddress>::IsPartialPoint(storage.GetLink(link));
    }
}
