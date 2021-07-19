namespace Platform::Data::Numbers::Raw
{
    template<std::integral TLink> class RawNumberToAddressConverter
    {
        // TODO: maybe use C++ functor style? [std::hash and other]
        public: TLink Convert(TLink source) const noexcept { return Hybrid<TLink>(source).AbsoluteValue(); }
    };
}
