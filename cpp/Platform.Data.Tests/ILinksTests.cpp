namespace Platform::Data::Tests
{
    template<typename TLink, typename TWriteHandler = std::function<typename TLink::value_type(TLink, TLink)>, typename TReadHandler = std::function<typename TLink::value_type(TLink)>, LinksConstants<typename TLink::value_type> VConstants = LinksConstants<typename TLink::value_type>{true}>
    struct Links : public ILinks<LinksOptions<TLink, TWriteHandler, TReadHandler, VConstants>>
    {
        using base = ILinks<LinksOptions<TLink, TWriteHandler, TReadHandler, VConstants>>;
        using typename base::LinkAddressType;
        using typename base::LinkType;
        using typename base::WriteHandlerType;
        using typename base::ReadHandlerType;
        using base::Constants;

        LinkAddressType Count(const LinkType& restriction) const override { return 0; };

        LinkAddressType Each(const LinkType& restriction, const ReadHandlerType& handler) const override { return 0; };

        LinkAddressType Create(const LinkType& restriction, const WriteHandlerType& handler) override { return 0; };

        LinkAddressType Update(const LinkType& restriction, const LinkType& substitution, const WriteHandlerType& handler) override { return 0; };

        LinkAddressType Delete(const LinkType& restriction, const WriteHandlerType& handler) override { return 0; };
    };
    TEST(ILinksDeriverTest, ConstructorAndMethodsTest)
    {
        using TLinkAddress = uint64_t;
        using TLink = std::vector<TLinkAddress>;
        Links<TLink> storage{};
        const Links<TLink> const_links {storage};
        int restriction[]{1,2,3};
        storage.Create(restriction);
        storage.Update(restriction, 1);
        storage.Count(restriction);
        const_links.Count(restriction);
        storage.Each([](TLinkAddress restriction_a){ return 1; }, restriction);
        const_links.Each([](TLinkAddress restriction_a){ return 1; }, restriction);
        storage.Delete(restriction);
    }
}
