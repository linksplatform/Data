namespace Platform::Data::Tests
{
    template<std::integral TLinkAddress = std::uint64_t, LinksConstants<TLinkAddress> VConstants = LinksConstants<TLinkAddress>{true}, typename TLink = std::vector<TLinkAddress>, typename TReadHandler = std::function<TLinkAddress(TLink)>, typename TWriteHandler = std::function<TLinkAddress(TLink, TLink)>>
    struct TestLinksForConcept : public ILinks<LinksOptions<TLinkAddress, VConstants, TLink, TReadHandler, TWriteHandler>>
    {
        using base = ILinks<LinksOptions<TLinkAddress, VConstants, TLink, TReadHandler, TWriteHandler>>;
        using typename base::LinkAddressType;
        using typename base::LinkType;
        using typename base::WriteHandlerType;
        using typename base::ReadHandlerType;
        using base::Constants;

        LinkAddressType Count(const std::vector<LinkAddressType>& restriction) const override { return 0; };

        LinkAddressType Each(const std::vector<LinkAddressType>& restriction, const ReadHandlerType& handler) const override { return 0; };

        LinkAddressType Create(const std::vector<LinkAddressType>& restriction, const WriteHandlerType& handler) override { return 0; };

        LinkAddressType Update(const std::vector<LinkAddressType>& restriction, const std::vector<LinkAddressType>& substitution, const WriteHandlerType& handler) override { return 0; };

        LinkAddressType Delete(const std::vector<LinkAddressType>& restriction, const WriteHandlerType& handler) override { return 0; };
    };

    // Test that our TestLinksForConcept satisfies the CLinks concept
    TEST(CLinksConceptTest, TestLinksForConceptSatisfiesConceptTest)
    {
        using TLinkAddress = uint64_t;
        using TestLinks = TestLinksForConcept<TLinkAddress>;
        
        // This should compile if the concept is satisfied
        static_assert(CLinks<TestLinks>, "TestLinksForConcept should satisfy CLinks concept");
        
        // Additional runtime test to ensure instance creation works
        TestLinks storage{};
        EXPECT_NO_THROW({
            std::vector<TLinkAddress> restriction{1};
            std::vector<TLinkAddress> substitution{1, 2};
            
            // Test Count method
            auto count = storage.Count(restriction);
            EXPECT_EQ(count, 0);
            
            // Test Each method  
            auto eachResult = storage.Each(restriction, [](const std::vector<TLinkAddress>& link){ return TLinkAddress{1}; });
            EXPECT_EQ(eachResult, 0);
            
            // Test Create method
            auto createResult = storage.Create(substitution, [](const std::vector<TLinkAddress>& before, const std::vector<TLinkAddress>& after){ return TLinkAddress{1}; });
            EXPECT_EQ(createResult, 0);
            
            // Test Update method
            auto updateResult = storage.Update(restriction, substitution, [](const std::vector<TLinkAddress>& before, const std::vector<TLinkAddress>& after){ return TLinkAddress{1}; });
            EXPECT_EQ(updateResult, 0);
            
            // Test Delete method
            auto deleteResult = storage.Delete(restriction, [](const std::vector<TLinkAddress>& before, const std::vector<TLinkAddress>& after){ return TLinkAddress{1}; });
            EXPECT_EQ(deleteResult, 0);
        });
    }

    // Test that a class missing required methods does not satisfy the concept
    struct IncompleteLinks
    {
        using LinkAddressType = uint64_t;
        using ReadHandlerType = std::function<uint64_t(std::vector<uint64_t>)>;
        using WriteHandlerType = std::function<uint64_t(std::vector<uint64_t>, std::vector<uint64_t>)>;
        static constexpr LinksConstants<uint64_t> Constants{true};
        
        // Missing Count, Each, Create, Update, Delete methods
    };

    TEST(CLinksConceptTest, IncompleteLinksDoesNotSatisfyConceptTest)
    {
        // This should not satisfy the concept due to missing methods
        static_assert(!CLinks<IncompleteLinks>, "IncompleteLinks should not satisfy CLinks concept");
    }

    // Test concept with different address types
    TEST(CLinksConceptTest, DifferentAddressTypesTest)
    {
        using TestLinks32 = TestLinksForConcept<uint32_t>;
        using TestLinks16 = TestLinksForConcept<uint16_t>;
        
        static_assert(CLinks<TestLinks32>, "TestLinksForConcept<uint32_t> should satisfy CLinks concept");
        static_assert(CLinks<TestLinks16>, "TestLinksForConcept<uint16_t> should satisfy CLinks concept");
    }
}