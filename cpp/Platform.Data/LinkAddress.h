namespace Platform::Data
{
    template <std::integral TLinkAddress>
    class LinkAddress
    {
        public: const TLinkAddress Index;

        public: auto operator[](std::integral auto index) const -> const TLinkAddress&
        {
            if (index == 0)
            {
                return Index;
            }
            else
            {
                throw std::out_of_range{""};
            }
        }

        public: [[nodiscard]] constexpr auto size() const noexcept -> std::size_t
        {
            return 1;
        }

        public: explicit LinkAddress(TLinkAddress index)
            : Index(index)
        {
        }

        public: template<typename OtherTLinkAddress> explicit LinkAddress(const LinkAddress<OtherTLinkAddress>& linkAddress)
            : Index((TLinkAddress)linkAddress.Index)
        {
        }

        public: [[nodiscard]] auto begin() const noexcept -> const TLinkAddress*
        {
            return std::addressof(Index);
        }

        public: [[nodiscard]] auto end() const noexcept -> const TLinkAddress*
        {
            return std::addressof(Index) + size();
        }

        public: auto operator==(LinkAddress<TLinkAddress> other) const noexcept
        {
            return Index == other.Index;
        }

        public: explicit operator TLinkAddress() const noexcept
        {
            return Index;
        }

        public: explicit operator std::string() const { return Converters::To<std::string>(Index); }

        public: friend std::ostream& operator<<(std::ostream& out, LinkAddress<TLinkAddress> obj)
        {
            return out << (std::string)obj;
        }
    };

    template<std::integral TLinkAddress>
    LinkAddress(TLinkAddress) -> LinkAddress<TLinkAddress>;
}


namespace std
{
    template <typename TLinkAddress>
    struct hash<Platform::Data::LinkAddress<TLinkAddress>>
    {
        std::size_t operator()(auto&& self) const
        {
            return self.Index;
        }
    };
}
