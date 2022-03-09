namespace Platform::Data::Tests
{
    TEST(LinksConstantsTests, ConstructorTest)
    {
        using namespace Platform::Data;

        auto constants = LinksConstants<std::uint64_t>(true);
        ASSERT_EQ(Hybrid<std::uint64_t>::ExternalZero, constants.ExternalReferencesRange.value().Minimum);
        ASSERT_EQ(std::numeric_limits<std::uint64_t>::max(), constants.ExternalReferencesRange.value().Maximum);
    }

    template<std::signed_integral TSigned, typename TUnsigned = std::make_unsigned_t<TSigned>>
    static void TestExternalReferences()
    {
        using namespace Platform::Data;
        using namespace Platform::Ranges;

        TUnsigned unsingedOne = 1;
        TUnsigned half = std::numeric_limits<TSigned>::max();
        auto constants = LinksConstants<TUnsigned>(Range{unsingedOne, half}, Range{TUnsigned(half + unsingedOne), std::numeric_limits<TUnsigned>::max()});

        auto minimum = Hybrid<TUnsigned>(0, true);
        auto maximum = Hybrid<TUnsigned>(half, true);

        ASSERT_TRUE(IsExternalReference(constants, minimum.Value));
        ASSERT_TRUE(minimum.IsExternal());
        ASSERT_FALSE(minimum.IsInternal());
        ASSERT_TRUE(IsExternalReference(constants, maximum.Value));
        ASSERT_TRUE(maximum.IsExternal());
        ASSERT_FALSE(maximum.IsInternal());
    }

    TEST(LinksConstantsTests, ExternalReferencesTest)
    {
        TestExternalReferences<std::int64_t>();
        TestExternalReferences<std::int32_t>();
        TestExternalReferences<std::int16_t>();
        TestExternalReferences<std::int8_t>();
    }
}
