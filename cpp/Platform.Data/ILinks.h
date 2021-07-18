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
        TLinkAddress Count(Interfaces::IArray<TLinkAddress> auto&& restriction) { return self().Count(restriction); }

        TLinkAddress Each(auto&& handler, Interfaces::IArray<TLinkAddress> auto&& restrictions)
            requires requires { { handler(restrictions) } -> std::same_as<TLinkAddress>; }
        { return self().Each(handler, restrictions); }

        TLinkAddress Create(Interfaces::IArray<TLinkAddress> auto&& restriction) { return self().Create(restriction); }

        TLinkAddress Update(Interfaces::IArray<TLinkAddress> auto&& restriction, Interfaces::IArray<TLinkAddress> auto&& substitution) { return self().Update(restriction, substitution); }

        void Delete(Interfaces::IArray<TLinkAddress> auto&& restriction) { self().Delete(restriction); }
    };
}
