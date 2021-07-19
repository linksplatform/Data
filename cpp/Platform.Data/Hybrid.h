namespace Platform::Data
{
    namespace Internal
    {
        constexpr auto smart_to_signed(std::integral auto value) noexcept
        {
            using raw_type = decltype(value);
            return static_cast<std::make_signed_t<raw_type>>(value);
        }
    }

    template<std::integral TLinkAddress>
    class Hybrid
    {
        public: static constexpr TLinkAddress HalfOfNumberValuesRange = std::numeric_limits<TLinkAddress>::max() / 2;
        public: static constexpr TLinkAddress ExternalZero = static_cast<TLinkAddress>(HalfOfNumberValuesRange + 1);

        public: const TLinkAddress Value = 0;

        public: [[nodiscard]] bool IsNothing() const noexcept
        {
            return (Value == ExternalZero) || (SignedValue() == 0);
        }

        public: [[nodiscard]] bool IsInternal() const noexcept
        {
            return SignedValue() > 0;
        }

        public: [[nodiscard]] bool IsExternal() const noexcept
        {
            return (Value == ExternalZero) || (SignedValue() < 0);
        }

        public: [[nodiscard]] auto SignedValue() const noexcept -> decltype(Internal::smart_to_signed(Value))
        {
            return Internal::smart_to_signed(Value);
        }

        public: [[nodiscard]] TLinkAddress AbsoluteValue() const noexcept
        {
            return (Value == ExternalZero) ? 0 : std::abs(SignedValue());
        }

        public: explicit Hybrid(TLinkAddress value) noexcept : Value(value) { }

        public: Hybrid(TLinkAddress value, bool isExternal) noexcept : Value(external_constructor(value, isExternal)) { }

        public: template<std::integral TOutAddress> explicit operator TOutAddress() const noexcept { return static_cast<TOutAddress>(Value); }

        public: explicit operator std::string() const { return IsExternal() ? std::string("<").append(Converters::To<std::string>(AbsoluteValue())).append(1, '>') : Converters::To<std::string>(Value); }

        public: friend std::ostream& operator<<(std::ostream& stream, const Hybrid<TLinkAddress>& self) { return stream << static_cast<std::string>(self); }

        public: constexpr auto operator==(const Hybrid<TLinkAddress>& other) const noexcept { return Value == other.Value; }

        private: static TLinkAddress external_constructor(TLinkAddress value, bool isExternal) noexcept
        {
            if (value == 0 && isExternal)
            {
                return ExternalZero;
            }
            else
            {
                auto absolute = std::abs(Internal::smart_to_signed(value)); // std::abs is not overloaded for unsigned types
                return (isExternal) ? -absolute : absolute;
            }
        }
    };

    template<std::integral TLinkAddress, typename... Args>
    Hybrid(TLinkAddress, Args...) -> Hybrid<TLinkAddress>;
}

namespace std
{
    template <typename TLinkAddress>
    struct hash<Platform::Data::Hybrid<TLinkAddress>>
    {
        std::size_t operator()(auto&& self) const
        {
            return self.Value;
        }
    };
}
