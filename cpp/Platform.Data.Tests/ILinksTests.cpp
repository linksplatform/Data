namespace Platform::Data::Tests
{
    template<typename TLink, typename TWriteHandler = std::function<typename TLink::value_type(TLink, TLink)>, typename TReadHandler = std::function<typename TLink::value_type(TLink)>, LinksConstants<typename TLink::value_type> VConstants = LinksConstants<typename TLink::value_type>{true}>
    struct Links : public ILinks<LinksOptions<TLink, TWriteHandler, TReadHandler, VConstants>>
    {
        using TLinkAddress = typename TLink::value_type;
        TLinkAddress Count(Interfaces::CArray auto&& restriction) const { return 0; }
    
        TLinkAddress Each(auto&& handler, const Interfaces::CArray auto& restriction) const
        { return 0; }
    
        TLinkAddress Create(Interfaces::CArray auto&& restriction) { return 0; }
    
        TLinkAddress Update(Interfaces::CArray auto&& substitution, std::convertible_to<TLinkAddress> auto... restriction) { return 0; }
    
        void Delete(Interfaces::CArray auto&& restriction) {  }
    };
    TEST(ILinksDeriverTest, ConstructorAndMethodsTest)
    {
        using TLinkAddress = uint64_t;
        using TLink = std::vector<TLinkAddress>;
        Links<TLink> links{};
        const Links<TLink> const_links {links};
        int restriction[]{1,2,3};
        links.Create(restriction);
        links.Update(restriction, 1);
        links.Count(restriction);
        const_links.Count(restriction);
        links.Each([](TLinkAddress restriction_a){ return 1; }, restriction);
        const_links.Each([](TLinkAddress restriction_a){ return 1; }, restriction);
        links.Delete(restriction);
    }
}
