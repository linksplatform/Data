namespace Platform::Data::Tests
{
    TEST_CLASS(LinksConstantsTests)
    {
        public: TEST_METHOD(ConstructorTest)
        {
            auto constants = LinksConstants<std::uint64_t>(enableExternalReferencesSupport: true);
            Assert::AreEqual(Hybrid<std::uint64_t>.ExternalZero, constants.ExternalReferencesRange.Value.Minimum);
            Assert::AreEqual(std::numeric_limits<std::uint64_t>::max(), constants.ExternalReferencesRange.Value.Maximum);
        }

        public: TEST_METHOD(ExternalReferencesTest)
        {
            TestExternalReferences<std::uint64_t, std::int64_t>();
            TestExternalReferences<std::uint32_t, std::int32_t>();
            TestExternalReferences<std::uint16_t, std::int16_t>();
            TestExternalReferences<std::uint8_t, std::int8_t>();
        }

        private: static void TestExternalReferences<TUnsigned, TSigned>()
        {
            auto unsingedOne = 0(TUnsigned) + 1;
            auto converter = UncheckedConverter<TSigned, TUnsigned>.Default;
            auto half = converter.Convert(NumericType<TSigned>.MaxValue);
            LinksConstants<TUnsigned> constants = LinksConstants<TUnsigned>({unsingedOne, half}, {half + unsingedOne, NumericType<TUnsigned>.MaxValue});

            auto minimum = Hybrid<TUnsigned>(0, isExternal: true);
            auto maximum = Hybrid<TUnsigned>(half, isExternal: true);

            Assert::IsTrue(constants.IsExternalReference(minimum));
            Assert::IsTrue(minimum.IsExternal);
            Assert::IsFalse(minimum.IsInternal);
            Assert::IsTrue(constants.IsExternalReference(maximum));
            Assert::IsTrue(maximum.IsExternal);
            Assert::IsFalse(maximum.IsInternal);
        }
    };
}
