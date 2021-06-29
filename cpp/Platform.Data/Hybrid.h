namespace Platform::Data
{
    template <typename ...> struct Hybrid;
    template <typename TLinkAddress> struct Hybrid<TLinkAddress>
    {
        private: static readonly UncheckedSignExtendingConverter<TLinkAddress, std::int64_t> _addressToInt64Converter = UncheckedSignExtendingConverter<TLinkAddress, std::int64_t>.Default;
        private: static readonly UncheckedConverter<std::int64_t, TLinkAddress> _int64ToAddressConverter = UncheckedConverter<std::int64_t, TLinkAddress>.Default;
        private: static readonly UncheckedConverter<TLinkAddress, std::uint64_t> _addressToUInt64Converter = UncheckedConverter<TLinkAddress, std::uint64_t>.Default;
        private: static readonly UncheckedConverter<std::uint64_t, TLinkAddress> _uInt64ToAddressConverter = UncheckedConverter<std::uint64_t, TLinkAddress>.Default;
        private: static readonly UncheckedConverter<void*, std::int64_t> _objectToInt64Converter = UncheckedConverter<void*, std::int64_t>.Default;

        public: inline static const std::uint64_t HalfOfNumberValuesRange = _addressToUInt64Converter.Convert(NumericType<TLinkAddress>.MaxValue) / 2;
        public: inline static const TLinkAddress ExternalZero = _uInt64ToAddressConverter.Convert(HalfOfNumberValuesRange + 1UL);

        public: TLinkAddress Value = 0;

        public: bool IsNothing()
        {
            return Value == ExternalZero || SignedValue == 0;
        }

        public: bool IsInternal()
        {
            return SignedValue > 0;
        }

        public: bool IsExternal()
        {
            return Value == ExternalZero || SignedValue < 0;
        }

        public: std::int64_t SignedValue()
        {
            return _addressToInt64Converter.Convert(Value);
        }

        public: std::int64_t AbsoluteValue()
        {
            return Value == ExternalZero ? 0 : Platform.Numbers.Math.Abs(SignedValue);
        }

        public: Hybrid(TLinkAddress value)
        {
            Ensure.OnDebug.IsUnsignedInteger<TLinkAddress>();
            Value = value;
        }

        public: Hybrid(TLinkAddress value, bool isExternal)
        {
            if (value == 0 && isExternal)
            {
                Value = ExternalZero;
            }
            else
            {
                if (isExternal)
                {
                    Value = Math<TLinkAddress>.Negate(value);
                }
                else
                {
                    Value = value;
                }
            }
        }

        public: Hybrid(void *value) { Value = _int64ToAddressConverter.Convert(_objectToInt64Converter.Convert(value)); }

        public: Hybrid(void *value, bool isExternal)
        {
            auto signedValue = value == nullptr ? 0 : _objectToInt64Converter.Convert(value);
            if (signedValue == 0 && isExternal)
            {
                Value = ExternalZero;
            }
            else
            {
                auto absoluteValue = System::Math::Abs(signedValue);
                Value = isExternal ? _int64ToAddressConverter.Convert(-absoluteValue) : _int64ToAddressConverter.Convert(absoluteValue);
            }
        }

        public: Hybrid(TLinkAddress integer) : Hybrid(integer) { }

        public: static explicit operator Hybrid<TLinkAddress>(std::uint64_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: static explicit operator Hybrid<TLinkAddress>(std::int64_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: static explicit operator Hybrid<TLinkAddress>(std::uint32_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: static explicit operator Hybrid<TLinkAddress>(std::int32_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: static explicit operator Hybrid<TLinkAddress>(std::uint16_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: static explicit operator Hybrid<TLinkAddress>(std::int16_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: static explicit operator Hybrid<TLinkAddress>(std::uint8_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: static explicit operator Hybrid<TLinkAddress>(std::int8_t integer) { return Hybrid<TLinkAddress>(integer); }

        public: operator TLinkAddress() const { return this->Value; }

        public: static explicit operator ulong(Hybrid<TLinkAddress> hybrid) { return CheckedConverter<TLinkAddress, std::uint64_t>.Default.Convert(hybrid.Value); }

        public: static explicit operator long(Hybrid<TLinkAddress> hybrid) { return hybrid.AbsoluteValue; }

        public: static explicit operator uint(Hybrid<TLinkAddress> hybrid) { return CheckedConverter<TLinkAddress, std::uint32_t>.Default.Convert(hybrid.Value); }

        public: static explicit operator int(Hybrid<TLinkAddress> hybrid) { return (std::int32_t)hybrid.AbsoluteValue; }

        public: static explicit operator ushort(Hybrid<TLinkAddress> hybrid) { return CheckedConverter<TLinkAddress, std::uint16_t>.Default.Convert(hybrid.Value); }

        public: static explicit operator short(Hybrid<TLinkAddress> hybrid) { return (std::int16_t)hybrid.AbsoluteValue; }

        public: static explicit operator byte(Hybrid<TLinkAddress> hybrid) { return CheckedConverter<TLinkAddress, std::uint8_t>.Default.Convert(hybrid.Value); }

        public: static explicit operator sbyte(Hybrid<TLinkAddress> hybrid) { return (std::int8_t)hybrid.AbsoluteValue; }

        public: operator std::string() const { return IsExternal ? std::string("<").append(Platform::Converters::To<std::string>(AbsoluteValue)).append(1, '>') : Platform::Converters::To<std::string>(Value).data(); }

        public: friend std::ostream & operator <<(std::ostream &out, const Hybrid<TLinkAddress> &obj) { return out << (std::string)obj; }

        public: bool operator ==(const Hybrid<TLinkAddress> &other) const { return Value == other.Value; }
    };
}

namespace std
{
    template <typename TLinkAddress>
    struct hash<Platform::Data::Hybrid<TLinkAddress>>
    {
        std::size_t operator()(const Platform::Data::Hybrid<TLinkAddress> &obj) const
        {
            return Value.GetHashCode();
        }
    };
}
