namespace Platform::Data
{
    template<typename TStorage>
    static typename TStorage::LinkAddressType Create(TStorage& storage, Interfaces::CArray auto&& substitution)
    {
        auto _continue { storage.Constants.Continue };
        typename TStorage::LinkAddressType createdLinkAddress;
		storage.Create(substitution, [&createdLinkAddress, _continue] (Interfaces::CArray auto&& before, Interfaces::CArray auto&& after)
        {
            createdLinkAddress = after[0];
            return _continue;
        });
		return createdLinkAddress;
	}

    template<typename TStorage>
    static typename TStorage::LinkAddressType Create(TStorage& storage)
    {
        constexpr std::array<typename TStorage::LinkAddressType, 0> empty{};
        return Create<typename TStorage::LinkAddressType>(storage, empty);
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Count(TStorage& storage)
    // TODO: later add noexcept(expr)
    {
        constexpr std::array<typename TStorage::LinkAddressType, 0> empty {};
        return storage.Count(empty);
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Count(TStorage& storage, std::convertible_to<typename TStorage::LinkAddressType> auto... restriction)
    // TODO: later add noexcept(expr)
    {
        std::array<typename TStorage::LinkAddressType, sizeof...(restriction)> array { static_cast<typename TStorage::LinkAddressType>(restriction)... };
        return storage.Count(array);
    }

//    template<typename typename TStorage::LinkAddressType>
//    static bool Exists(auto&& storage, typename TStorage::LinkAddressType link) noexcept
//    {
//        auto constants = storage.Constants;
//        return IsExternalReference(constants, link) || (IsInternalReference(constants, link) && Count<typename TStorage::LinkAddressType>(storage, link) != 0);
//    }

    template<typename TStorage>
    static void EnsureLinkExists(TStorage& storage, typename TStorage::LinkAddressType link, const std::string& argument = {})
    {
        if (!storage.Exists(link))
        {
            throw ArgumentLinkDoesNotExistsException<typename TStorage::LinkAddressType>(link, argument);
        }
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Each(TStorage& storage, auto&& handler, std::convertible_to<typename TStorage::LinkAddressType> auto... restrictions)
    // TODO: later create noexcept(expr)
    {
        std::array<typename TStorage::LinkAddressType, sizeof...(restrictions)> restrictionArray { static_cast<typename TStorage::LinkAddressType>(restrictions)... };
        return storage.Each(restrictionArray, handler);
    }

    template<typename TStorage>
    static Interfaces::CArray auto GetLink(TStorage& storage, typename TStorage::LinkAddressType link)
    {
        auto constants = storage.Constants;
        auto _continue = constants.Continue;
        auto any = constants.Any;
        if (IsExternalReference(constants, link))
        {
            std::vector resultLink {link,link,link};
            return resultLink;
        }

        std::vector<typename TStorage::LinkAddressType> resultLink;
        storage.Each(std::array{link, any, any}, [&resultLink, _continue](Interfaces::CArray auto&& link)
        {
            resultLink = { std::ranges::begin(link), std::ranges::end(link) };
            return _continue;
        });
        Expects(!resultLink.empty());
        return resultLink;
    }

    template<typename TStorage>
    static bool IsFullPoint(TStorage& storage, typename TStorage::LinkAddressType link)
    {
        if (IsExternalReference(storage.Constants, link))
        {
            return true;
        }
        storage.EnsureLinkExists(link);
        return Point<typename TStorage::LinkAddressType>::IsFullPoint(storage.GetLink(link));
    }

    template<typename TStorage>
    static bool IsPartialPoint(TStorage& storage, typename TStorage::LinkAddressType link)
    {
        if (IsExternalReference(storage.Constants, link))
        {
            return true;
        }
        storage.EnsureLinkExists(link);
        return Point<typename TStorage::LinkAddressType>::IsPartialPoint(storage.GetLink(link));
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Delete(TStorage& storage, std::convertible_to<typename TStorage::LinkAddressType> auto ...restriction)
    {
        auto constants = storage.Constants;
        auto _continue = constants.Continue;
        std::array restrictionArray { static_cast<typename TStorage::LinkAddressType>(restriction)... };
        typename TStorage::LinkAddressType deletedLinkAddress;
        storage.Delete(restrictionArray, [&deletedLinkAddress, _continue](Interfaces::CArray auto before, Interfaces::CArray auto after)
        {
            deletedLinkAddress = before[0];
            return _continue;
        });
        return deletedLinkAddress;
    }
}
