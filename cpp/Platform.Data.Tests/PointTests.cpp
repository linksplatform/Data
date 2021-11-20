namespace Platform::Data::Tests
{
    TEST(PointTests, IsPartialPointTest)
    {
        ASSERT_TRUE(IsPartialPoint(1, 1, 2));
        ASSERT_TRUE(IsPartialPoint(1, 2, 1));
        ASSERT_TRUE(IsPartialPoint(1, 1, 1));
        ASSERT_FALSE(IsPartialPoint(1, 2, 3));
    };
    TEST(PointTests, IsFullPointTest)
    {
        ASSERT_TRUE(IsFullPoint(1, 1, 1));
        ASSERT_FALSE(IsFullPoint(1, 2, 1));
        ASSERT_FALSE(IsFullPoint(1, 1, 2));
    };
}