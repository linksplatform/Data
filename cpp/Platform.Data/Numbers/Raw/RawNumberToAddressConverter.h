namespace Platform::Data::Numbers::Raw
{
    template <typename ...> class RawNumberToAddressConverter;
    template <typename TLink> class RawNumberToAddressConverter<TLink> : public IConverter<TLink>
    {
        private: static readonly UncheckedConverter<std::int64_t, TLink> _converter = UncheckedConverter<std::int64_t, TLink>.Default;

        public: TLink Convert(TLink source) { return _converter.Convert(Hybrid<TLink>(source).AbsoluteValue); }
    };
}
