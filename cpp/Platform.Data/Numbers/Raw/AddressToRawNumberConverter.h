namespace Platform::Data::Numbers::Raw
{
    template <typename ...> class AddressToRawNumberConverter;
    template <typename TLink> class AddressToRawNumberConverter<TLink> : public IConverter<TLink>
    {
        public: TLink Convert(TLink source) { return Hybrid<TLink>(source, isExternal: true); }
    };
}
