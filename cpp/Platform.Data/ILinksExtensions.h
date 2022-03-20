namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename TStorage>
    static typename TStorage::LinkAddressType Create(TStorage& storage, CArray<typename TStorage::LinkAddressType> auto&& substitution)
    {
        auto $continue { storage.Constants.Continue };
        typename TStorage::LinkAddressType createdLinkAddress;
		storage.Create(substitution, [&createdLinkAddress, $continue] (const typename TStorage::HandlerParameterType& before, const typename TStorage::HandlerParameterType& after)
        {
            createdLinkAddress = after[0];
            return $continue;
        });
		return createdLinkAddress;
	}

    template<typename TStorage>
    static typename TStorage::LinkAddressType Update(TStorage& storage, CArray<typename TStorage::LinkAddressType> auto&& restriction, CArray<typename TStorage::LinkAddressType> auto&& substitution)
    {
        auto $continue{storage.Constants.Continue};
        typename TStorage::LinkAddressType updatedLinkAddress;
        storage.Update(restriction, substitution, [&updatedLinkAddress, $continue] (const typename TStorage::HandlerParameterType& before, const typename TStorage::HandlerParameterType& after)
        {
            updatedLinkAddress = after[0];
            return $continue;
        });
        return updatedLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Delete(TStorage& storage, CArray<typename TStorage::LinkAddressType> auto&& restriction)
    {
        auto $continue{storage.Constants.Continue};
        typename TStorage::LinkAddressType deletedLinkAddress;
        storage.Delete(restriction, [&deletedLinkAddress, $continue] (const typename TStorage::HandlerParameterType& before, const typename TStorage::HandlerParameterType& after)
        {
            deletedLinkAddress = after[0];
            return $continue;
        });
        return deletedLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Delete(TStorage& storage, typename TStorage::LinkAddressType linkAddress)
    {
        auto $continue{storage.Constants.Continue};
        typename TStorage::LinkAddressType deletedLinkAddress;
        storage.Delete(LinkAddress{linkAddress}, [&deletedLinkAddress, $continue] (const typename TStorage::HandlerParameterType& before, const typename TStorage::HandlerParameterType& after)
        {
            deletedLinkAddress = after[0];
            return $continue;
        });
        return deletedLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Count(const TStorage& storage, std::convertible_to<typename TStorage::LinkAddressType> auto ...restriction)
    // TODO: later add noexcept(expr)
    {
        typename TStorage::HandlerParameterType restrictionContainer { static_cast<typename TStorage::HandlerParameterType>(restriction)... };
        return storage.Count(restrictionContainer);
    }

    template<typename TStorage>
    static bool Exists(auto&& storage, typename TStorage::LinkAddressType linkAddress) noexcept
    {
        auto constants = storage.Constants;
        return IsExternalReference(constants, link) || (IsInternalReference(constants, link) && Count(storage, link) != 0);
    }

    template<typename TStorage>
    static void EnsureLinkExists(TStorage& storage, typename TStorage::LinkAddressType link, const std::string& argument = {})
    {
        if (!storage.Exists(link))
        {
            throw ArgumentLinkDoesNotExistsException<typename TStorage::LinkAddressType>(link, argument);
        }
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Each(const TStorage& storage, auto&& handler, std::convertible_to<typename TStorage::LinkAddressType> auto... restrictions)
    // TODO: later create noexcept(expr)
    {
        typename TStorage::HandlerParameterType restrictionContainer { static_cast<typename TStorage::LinkAddressType>(restrictions)... };
        return storage.Each(restrictionContainer, handler);
    }

    template<typename TStorage>
    static Interfaces::CArray auto GetLink(const TStorage& storage, typename TStorage::LinkAddressType linkAddress)
    {
        auto constants = storage.Constants;
        auto $continue = constants.Continue;
        auto any = constants.Any;
        if (IsExternalReference(constants, linkAddress))
        {
            typename TStorage::HandlerParameterType resultLink {linkAddress, linkAddress, linkAddress};
            return resultLink;
        }
        typename TStorage::HandlerParameterType resultLink;
        storage.Each(LinkAddress{linkAddress}, [&resultLink, $continue](const typename TStorage::HandlerParameterType& link)
        {
            resultLink = link;
            return $continue;
        });
        Ensures(!resultLink.empty());
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
}
