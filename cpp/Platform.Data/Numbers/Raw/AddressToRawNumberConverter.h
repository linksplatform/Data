namespace Platform::Data::Numbers::Raw
{
    template<std::integral TLink> class AddressToRawNumberConverter
    {
        public: TLink Convert(TLink source) const noexcept { return static_cast<TLink>(Hybrid<TLink>(source, true)); }
    };
}
