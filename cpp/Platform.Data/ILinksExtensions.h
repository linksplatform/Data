namespace Platform::Data
{
    using namespace Platform::Interfaces;
    template<typename TStorage>
    static typename TStorage::LinkAddressType Create(TStorage& storage, const std::vector<typename TStorage::LinkAddressType>& substitution)
    {
        auto $continue { storage.Constants.Continue };
        typename TStorage::LinkAddressType createdLinkAddress;
		storage.Create(substitution, [&createdLinkAddress, $continue] (const std::vector<typename TStorage::LinkAddressType>& before, const std::vector<typename TStorage::LinkAddressType>& after)
        {
            createdLinkAddress = after[0];
            return $continue;
        });
		return createdLinkAddress;
	}

    template<typename TStorage>
    static typename TStorage::LinkAddressType Create(TStorage& storage, std::convertible_to<typename TStorage::LinkAddressType> auto ...substitutionPack)
    {
        std::vector<typename TStorage::LinkAddressType> substitution { static_cast<typename TStorage::LinkAddressType>(substitutionPack)... };
        auto $continue { storage.Constants.Continue };
        typename TStorage::LinkAddressType createdLinkAddress;
        storage.Create(substitution, [&createdLinkAddress, $continue] (const std::vector<typename TStorage::LinkAddressType>& before, const std::vector<typename TStorage::LinkAddressType>& after)
                       {
                           createdLinkAddress = after[0];
                           return $continue;
                       });
        return createdLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Update(TStorage& storage, const std::vector<typename TStorage::LinkAddressType>& restriction, const std::vector<typename TStorage::LinkAddressType>& substitution)
    {
        auto $continue{storage.Constants.Continue};
        typename TStorage::LinkAddressType updatedLinkAddress;
        storage.Update(restriction, substitution, [&updatedLinkAddress, $continue] (const std::vector<typename TStorage::LinkAddressType>& before, const std::vector<typename TStorage::LinkAddressType>& after)
        {
            updatedLinkAddress = after[0];
            return $continue;
        });
        return updatedLinkAddress;
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Delete(TStorage& storage, const std::vector<typename TStorage::LinkAddressType>& restriction)
    {
        auto $continue{storage.Constants.Continue};
        typename TStorage::LinkAddressType deletedLinkAddress;
        storage.Delete(restriction, [&deletedLinkAddress, $continue] (const std::vector<typename TStorage::LinkAddressType>& before, const std::vector<typename TStorage::LinkAddressType>& after)
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
        storage.Delete(std::vector<typename TStorage::LinkAddressType>{linkAddress}, [&deletedLinkAddress, $continue] (const std::vector<typename TStorage::LinkAddressType>& before, const std::vector<typename TStorage::LinkAddressType>& after)
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
        std::vector<typename TStorage::LinkAddressType> restriction { static_cast<typename TStorage::LinkAddressType>(restrictionPack)... };
        return storage.Count(restriction);
    }

    template<typename TStorage>
    static bool Exists(const TStorage& storage, typename TStorage::LinkAddressType linkAddress) noexcept
    {
        auto constants = storage.Constants;
        return IsExternalReference(constants, linkAddress) || (IsInternalReference(constants, linkAddress) && Count(storage, linkAddress) > 0);
    }

    template<typename TStorage>
    static typename TStorage::LinkAddressType Each(const TStorage& storage, const typename TStorage::ReadHandlerType& handler, std::convertible_to<typename TStorage::LinkAddressType> auto... restriction)
    // TODO: later create noexcept(expr)
    {
        std::vector<typename TStorage::LinkAddressType> restrictionContainer { static_cast<typename TStorage::LinkAddressType>(restriction)... };
        return storage.Each(restrictionContainer, handler);
    }

    template<typename TStorage>
    static std::vector<typename TStorage::LinkAddressType> GetLink(const TStorage& storage, typename TStorage::LinkAddressType linkAddress)
    {
        auto constants = storage.Constants;
        auto $continue = constants.Continue;
        auto any = constants.Any;
        if (IsExternalReference(constants, linkAddress))
        {
            std::vector<typename TStorage::LinkAddressType> resultLink {linkAddress, linkAddress, linkAddress};
            return resultLink;
        }
        std::vector<typename TStorage::LinkAddressType> resultLink;
        storage.Each(std::vector<typename TStorage::LinkAddressType>{linkAddress}, [&resultLink, $continue](const std::vector<typename TStorage::LinkAddressType>& link)
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
