namespace Platform::Data::Tests
{
    TEST(HybridTests, ObjectConstructorTest)
    {
        ASSERT_EQ(0, Hybrid(std::uint8_t{128}).AbsoluteValue());
        ASSERT_EQ(1, Hybrid(static_cast<uint8_t>(-1)).AbsoluteValue());
        ASSERT_EQ(0, Hybrid(std::uint8_t{0}).AbsoluteValue());
        ASSERT_EQ(1, Hybrid(std::uint8_t{1}).AbsoluteValue());
    };
}
