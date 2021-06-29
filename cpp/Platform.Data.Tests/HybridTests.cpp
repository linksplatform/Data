namespace Platform::Data::Tests
{
    TEST_CLASS(HybridTests)
    {
        public: TEST_METHOD(ObjectConstructorTest)
        {
            Assert::AreEqual(0, Hybrid<std::uint8_t>(unchecked((std::uint8_t)128)).AbsoluteValue);
            Assert::AreEqual(0, Hybrid<std::uint8_t>((void*)128).AbsoluteValue);
            Assert::AreEqual(1, Hybrid<std::uint8_t>(unchecked((std::uint8_t)-1)).AbsoluteValue);
            Assert::AreEqual(1, Hybrid<std::uint8_t>((void*)-1).AbsoluteValue);
            Assert::AreEqual(0, Hybrid<std::uint8_t>(unchecked((std::uint8_t)0)).AbsoluteValue);
            Assert::AreEqual(0, Hybrid<std::uint8_t>((void*)0).AbsoluteValue);
            Assert::AreEqual(1, Hybrid<std::uint8_t>(unchecked((std::uint8_t)1)).AbsoluteValue);
            Assert::AreEqual(1, Hybrid<std::uint8_t>((void*)1).AbsoluteValue);
        }
    };
}
