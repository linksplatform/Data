namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename TStorage>
    static typename TStorage::LinkAddressType Create(TStorage& storage, const typename TStorage::LinkType& substitution)
    {
        auto $continue { storage.Constants.Continue };
        typename TStorage::LinkAddressType createdLinkAddress;
		storage.Create(substitution, [&createdLinkAddress, $continue] (const typename TStorage::LinkType& before, const typename TStorage::LinkType& after)
        {
            createdLinkAddress = after[0];
            return $continue;
        });
		return createdLinkAddress;
	}

    template<typename TStorage>
    static typename TStorage::LinkAddressType Create(TStorage& storage, std::convertible_to<typename TStorage::LinkAddressType> auto ...substitutionPack)
    {
        typename TStorage::LinkType substitution { static_cast<typename TStorage::LinkAddressType>(substitutionPack)... };
        auto $continue { storage.Constants.Continue };
        typename TStorage::LinkAddressType createdLinkAddress;
        storage.Create(substitution, [&createdLinkAddress, $continue] (const typename TStorage::LinkType& before, const typename TStorage::LinkType& after)
                       {
                           createdLinkAddress = after[0];
                           return $continue;
                       });
        return createdLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Update(TStorage& storage, const typename TStorage::LinkType& restriction, const typename TStorage::LinkType& substitution)
    {
        auto $continue{storage.Constants.Continue};
        typename TStorage::LinkAddressType updatedLinkAddress;
        storage.Update(restriction, substitution, [&updatedLinkAddress, $continue] (const typename TStorage::LinkType& before, const typename TStorage::LinkType& after)
        {
            updatedLinkAddress = after[0];
            return $continue;
        });
        return updatedLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Delete(TStorage& storage, const typename TStorage::LinkType& restriction)
    {
        auto $continue{storage.Constants.Continue};
        typename TStorage::LinkAddressType deletedLinkAddress;
        storage.Delete(restriction, [&deletedLinkAddress, $continue] (const typename TStorage::LinkType& before, const typename TStorage::LinkType& after)
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
        storage.Delete(LinkAddress{linkAddress}, [&deletedLinkAddress, $continue] (const typename TStorage::LinkType& before, const typename TStorage::LinkType& after)
        {
            deletedLinkAddress = after[0];
            return $continue;
        });
        return deletedLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Count(const TStorage& storage, std::convertible_to<typename TStorage::LinkAddressType> auto ...restrictionPack)
    // TODO: later add noexcept(expr)
    {
        typename TStorage::LinkType restriction { static_cast<typename TStorage::LinkType>(restrictionPack)... };
        return storage.Count(restriction);
    }

    template<typename TStorage>
    static bool Exists(const TStorage& storage, typename TStorage::LinkAddressType linkAddress) noexcept
    {
        auto constants = storage.Constants;
        return IsExternalReference(constants, linkAddress) || (IsInternalReference(constants, linkAddress) && Count(storage, linkAddress) != 0);
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Each(const TStorage& storage, const typename TStorage::ReadHandlerType& handler, std::convertible_to<typename TStorage::LinkAddressType> auto... restriction)
    // TODO: later create noexcept(expr)
    {
        typename TStorage::LinkType restrictionContainer { static_cast<typename TStorage::LinkAddressType>(restriction)... };
        return storage.Each(restrictionContainer, handler);
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType GetLink(const TStorage& storage, typename TStorage::LinkAddressType linkAddress)
    {
        auto constants = storage.Constants;
        auto $continue = constants.Continue;
        auto any = constants.Any;
        if (IsExternalReference(constants, linkAddress))
        {
            typename TStorage::LinkType resultLink {linkAddress, linkAddress, linkAddress};
            return resultLink;
        }
        typename TStorage::LinkType resultLink;
        storage.Each(LinkAddress{linkAddress}, [&resultLink, $continue](const typename TStorage::LinkType& link)
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
        Ensures(Exists(storage, link));
        return Point<typename TStorage::LinkAddressType>::IsFullPoint(storage.GetLink(link));
    }

    template<typename TStorage>
    static bool IsPartialPoint(TStorage& storage, typename TStorage::LinkAddressType link)
    {
        if (IsExternalReference(storage.Constants, link))
        {
            return true;
        }
        Ensures(Exists(storage, link));
        return Point<typename TStorage::LinkAddressType>::IsPartialPoint(storage.GetLink(link));
    }
}
