namespace Platform::Data
{
    template<typename TLinkAddress>
    static TLinkAddress Create(auto&& storage, Interfaces::CArray auto&& substitution)
    {
        auto _continue { storage.Constants.Continue };
        TLinkAddress createdLinkAddress;
		storage.Create(substitution, [&createdLinkAddress, _continue] (Interfaces::CArray auto&& before, Interfaces::CArray auto&& after)
        {
            createdLinkAddress = after[0];
            return _continue;
        });
		return createdLinkAddress;
	}

    template<typename TLinkAddress>
    static TLinkAddress Create(auto&& storage)
    {
        constexpr std::array<TLinkAddress, 0> empty{};
        return Create<TLinkAddress>(storage, empty);
    }

    template<typename TLinkAddress>
    static TLinkAddress Count(auto&& storage)
    // TODO: later add noexcept(expr)
    {
        constexpr std::array<TLinkAddress, 0> empty {};
        return storage.Count(empty);
    }

    template<typename TLinkAddress>
    static TLinkAddress Count(auto&& storage, std::convertible_to<TLinkAddress> auto... restriction)
    // TODO: later add noexcept(expr)
    {
        std::array<TLinkAddress, sizeof...(restriction)> array { static_cast<TLinkAddress>(restriction)... };
        return storage.Count(array);
    }

    template<typename TLinkAddress>
    static bool Exists(auto&& storage, TLinkAddress link) noexcept
    {
        auto&& constants = storage.Constants;
        return IsExternalReference(constants, link) || (IsInternalReference(constants, link) && Count<TLinkAddress>(storage, link) != 0);
    }

    template<typename TLinkAddress>
    static void EnsureLinkExists(auto&& storage, TLinkAddress link, const std::string& argument = {})
    {
        if (not storage.Exists(link))
        {
            throw ArgumentLinkDoesNotExistsException<TLinkAddress>(link, argument);
        }
    }

    template<typename TLinkAddress>
    static TLinkAddress Each(auto&& storage, auto&& handler, std::convertible_to<TLinkAddress> auto... restrictions)
    // TODO: later create noexcept(expr)
    {
        TLinkAddress array[] = { static_cast<TLinkAddress>(restrictions)... };
        return storage.Each(handler, array);
    }

    template<typename TLinkAddress>
    static Interfaces::CArray auto GetLink(auto&& storage, TLinkAddress link)
    {
        auto constants = storage.Constants;
        auto _continue = constants.Continue;
        auto any = constants.Any;
        if (IsExternalReference(constants, link))
        {
            std::vector resultLink {link,link,link};
            return resultLink;
        }

        // TODO: dynamic polymorphism (for @Konard)
        //auto linkPartsSetter = Setter<IList<TLinkAddress>, TLinkAddress>(constants.Continue, constants.Break);
        //storage.Each(linkPartsSetter.SetAndReturnTrue, link);
        //return linkPartsSetter.Result;

        std::vector<TLinkAddress> resultLink;
        storage.Each(std::array{link, any, any}, [&resultLink, _continue](Interfaces::CArray auto&& link)
        {
			std::cout << "Link: " << link[0] << std::endl;
            resultLink = { std::ranges::begin(link), std::ranges::end(link) };
            return _continue;
        });
        Expects(!resultLink.empty());
        return resultLink;
    }

    template<typename TLinkAddress>
    static bool IsFullPoint(auto&& storage, TLinkAddress link)
    {
        if (IsExternalReference(storage.Constants, link))
        {
            return true;
        }
        storage.EnsureLinkExists(link);
        return Point<TLinkAddress>::IsFullPoint(storage.GetLink(link));
    }

    template<typename TLinkAddress>
    static bool IsPartialPoint(auto&& storage, TLinkAddress link)
    {
        if (IsExternalReference(storage.Constants, link))
        {
            return true;
        }
        storage.EnsureLinkExists(link);
        return Point<TLinkAddress>::IsPartialPoint(storage.GetLink(link));
    }

    template<typename TLinkAddress>
    static TLinkAddress Delete(auto&& storage, std::convertible_to<TLinkAddress> auto ...restriction)
    {
        auto constants = storage.Constants;
        auto _continue = constants.Continue;
        std::array restrictionArray { static_cast<TLinkAddress>(restriction)... };
        TLinkAddress deletedLinkAddress;
        storage.Delete(restrictionArray, [&deletedLinkAddress, _continue](Interfaces::CArray auto before, Interfaces::CArray auto after)
        {
            deletedLinkAddress = before[0];
            return _continue;
        });
        return deletedLinkAddress;
    }

    template<typename TLinkAddress>
    static bool Exists(const auto&& storage, TLinkAddress linkAddress)
    {
        auto constants = storage.Constants;
        return constants.IsExternalReference(linkAddress) || (constants.IsInternalReference(linkAddress) && (Count(storage, linkAddress) > 0));
    }
}
