namespace Platform::Data
{
    template<typename Self, std::integral TLinkAddress, std::derived_from<LinksConstants<TLinkAddress>> TConstants>
    class ILinks
    {
        auto&& self() & noexcept { return static_cast<Self&>(*this); }
        auto&& self() && noexcept { return static_cast<Self&&>(*this); }
        auto&& self() const & noexcept { return static_cast<const Self&>(*this); }
        auto&& self() const && noexcept { return static_cast<const Self&&>(*this); }

    public:
        const TConstants Constants{};

        // TODO: maybe mark methods as const
        TLinkAddress Count(Interfaces::IArray auto&& restriction) const { return self().Count(restriction); }

        TLinkAddress Each(auto&& handler, Interfaces::IArray auto& restrictions)
            requires requires { { handler(restrictions) } -> std::same_as<TLinkAddress>; }
        { return self().Each(handler, restrictions); }

        TLinkAddress Each(auto&& handler, const Interfaces::IArray auto& restrictions) const
            requires requires { { handler(restrictions) } -> std::same_as<TLinkAddress>; }
        { return self().Each(handler, restrictions); }

        TLinkAddress Create(Interfaces::IArray auto&& restriction) { return self().Create(restriction); }

        TLinkAddress Update(Interfaces::IArray auto&& substitution) { return self().Update(restriction, substitution); }

        void Delete(Interfaces::IArray auto&& restriction) { self().Delete(restriction); }

    // EXTENSIONS
        auto Count(std::convertible_to<TLinkAddress> auto... restrictions) const -> TLinkAddress
        // TODO: later add noexcept(expr)
        {
            auto&& links = *this;
            TLinkAddress array[] = { static_cast<TLinkAddress>(restrictions)... };
            return links.Count(array);
        }

        auto Exists(TLinkAddress link) const noexcept -> bool
        {
            auto&& links = *this;
            auto&& constants = links.Constants;
            return IsExternalReference(constants, link) || (IsInternalReference(constants, link) && links.Count(LinkAddress(link)) != 0);
        }

        auto EnsureLinkExists(TLinkAddress link, const std::string& argument = {}) const -> void
        {
            auto&& links = *this;
            if (not links.Exists(link))
            {
                throw ArgumentLinkDoesNotExistsException<TLinkAddress>(link, argument);
            }
        }

        auto Each(auto&& handler, std::convertible_to<TLinkAddress> auto... restrictions) const -> TLinkAddress
        // TODO: later create noexcept(expr)
        {
            auto&& links = *this;
            TLinkAddress array[] = { static_cast<TLinkAddress>(restrictions)... };
            return links.Each(handler, array);
        }

        auto GetLink(TLinkAddress link) const -> Interfaces::IArray auto
        {
            auto&& links = *this;
            auto&& constants = links.Constants;
            if (IsExternalReference(constants, link))
            {
                return Point(link, constants.TargetPart + 1);
            }

            // TODO: dynamic polymorphism (for @Konard)
            //auto linkPartsSetter = Setter<IList<TLinkAddress>, TLinkAddress>(constants.Continue, constants.Break);
            //links.Each(linkPartsSetter.SetAndReturnTrue, link);
            //return linkPartsSetter.Result;

            std::vector<TLinkAddress> wrapper;
            links.Each([&wrapper, &constants](Interfaces::IArray auto&& link)
            {
                wrapper = std::vector(std::ranges::begin(link), std::ranges::end(link));
                return constants.Continue;
            }, link);
            return wrapper;
        }

        auto IsFullPoint(TLinkAddress link) const -> bool
        {
            auto&& links = *this;
            if (IsExternalReference(links.Constants, link))
            {
                return true;
            }
            links.EnsureLinkExists(link);
            return Point<TLinkAddress>::IsFullPoint(links.GetLink(link));
        }

        auto IsPartialPoint(TLinkAddress link) const -> bool
        {
            auto&& links = *this;
            if (IsExternalReference(links.Constants, link))
            {
                return true;
            }
            links.EnsureLinkExists(link);
            return Point<TLinkAddress>::IsPartialPoint(links.GetLink(link));
        }
    };
}
