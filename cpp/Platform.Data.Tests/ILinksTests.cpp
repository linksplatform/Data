namespace Platform::Data::Tests
{
    template<std::integral TLinkAddress>
    struct Links : public ILinks<Links<TLinkAddress>, TLinkAddress, LinksConstants<TLinkAddress>>
    {
        TLinkAddress Count(Interfaces::CArray auto&& restriction) const { return 0; }
    
        TLinkAddress Each(auto&& handler, const Interfaces::CArray auto& restrictions) const
        { return 0; }
    
        TLinkAddress Create(Interfaces::CArray auto&& restriction) { return 0; }
    
        TLinkAddress Update(Interfaces::CArray auto&& substitution, std::convertible_to<TLinkAddress> auto... restrictions) { return 0; }
    
        void Delete(Interfaces::CArray auto&& restriction) {  }
    };
    TEST(ILinksDeriverTest, ConstructorAndMethodsTest)
    {
        using TLink = uint64_t;
        Links<TLink> links{};
        const Links<TLink> const_links {links};
        int restriction[]{1,2,3};
        links.Create(restriction);
        links.Update(restriction, 1);
        links.Count(restriction);
        const_links.Count(restriction);
        links.Each([](TLink restriction_a){ return 1; }, restriction);
        const_links.Each([](TLink restriction_a){ return 1; }, restriction);
        links.Delete(restriction);
    }
}
