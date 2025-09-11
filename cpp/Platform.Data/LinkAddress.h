namespace Platform::Data
{
    /// @brief A wrapper class for link addresses that provides container-like semantics.
    /// @tparam TLinkAddress The integral type used for link addressing (e.g., uint64_t, uint32_t).
    /// 
    /// This class encapsulates a single link address and provides various conversion operators
    /// and container-like interface methods. It allows treating a single address as a 
    /// container with one element, which simplifies generic programming with link operations.
    template <std::integral TLinkAddress>
    class LinkAddress
    {
        /// @brief The stored link address index.
        public: const TLinkAddress Index;

        /// @brief Provides indexed access to the link address.
        /// @param index The index to access (must be 0 for single-element container).
        /// @return Reference to the stored Index value.
        /// @throws std::out_of_range if index is not 0.
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

        /// @brief Returns the size of this container (always 1).
        /// @return Always returns 1 since this contains a single address.
        public: [[nodiscard]] constexpr auto size() const noexcept -> std::size_t
        {
            return 1;
        }

        /// @brief Constructs a LinkAddress with the specified index value.
        /// @param index The link address value to store.
        public: explicit LinkAddress(TLinkAddress index)
            : Index(index)
        {
        }

        /// @brief Copy constructor for converting between different LinkAddress types.
        /// @tparam OtherTLinkAddress The source LinkAddress type.
        /// @param linkAddress The LinkAddress to copy from.
        public: template<typename OtherTLinkAddress> explicit LinkAddress(const LinkAddress<OtherTLinkAddress>& linkAddress)
            : Index(linkAddress.Index)
        {
        }

        /// @brief Returns an iterator to the beginning (pointing to Index).
        /// @return Pointer to the stored Index value.
        public: [[nodiscard]] auto begin() const noexcept -> const TLinkAddress*
        {
            return std::addressof(Index);
        }

        /// @brief Returns an iterator to the end (one past the Index).
        /// @return Pointer one position past the stored Index value.
        public: [[nodiscard]] auto end() const noexcept -> const TLinkAddress*
        {
            // TODO: maybe explicit write "+ 1" instead
            return std::addressof(Index) + size();
        }

        /// @brief Equality comparison operator.
        /// @param other The other LinkAddress to compare with.
        /// @return True if both LinkAddress objects have the same Index value.
        public: auto operator==(LinkAddress<TLinkAddress> other) const noexcept
        {
            return Index == other.Index;
        }

        /// @brief Explicit conversion to the underlying address type.
        /// @return The stored Index value.
        public: explicit operator TLinkAddress() const noexcept
        {
            return Index;
        }

        /// @brief Implicit conversion to vector containing the single address.
        /// @return Vector containing the Index value.
        public: operator std::vector<TLinkAddress>() const
        {
            return std::vector<TLinkAddress>{Index};
        }

        /// @brief Explicit conversion to string representation.
        /// @return String representation of the Index value.
        public: explicit operator std::string() const { return Converters::To<std::string>(Index); }

        /// @brief Stream output operator for printing LinkAddress objects.
        /// @param stream The output stream to write to.
        /// @param self The LinkAddress object to print.
        /// @return Reference to the output stream.
        public: friend std::ostream& operator<<(std::ostream& stream, LinkAddress<TLinkAddress> self)
        {
            return stream << static_cast<std::string>(self);
        }
    };

    /// @brief Deduction guide for LinkAddress template parameter inference.
    template<typename TLinkAddress>
    LinkAddress(TLinkAddress) -> LinkAddress<TLinkAddress>;
}

/// @brief Hash function specialization for LinkAddress to enable use in hash-based containers.
/// @tparam TLinkAddress The integral type of the link address.
template<typename TLinkAddress>
struct std::hash<Platform::Data::LinkAddress<TLinkAddress>>
{
    /// @brief Computes hash value for a LinkAddress object.
    /// @param self The LinkAddress object to hash.
    /// @return Hash value based on the Index member.
    std::size_t operator()(auto&& self) const
    {
        return self.Index;
    }
};
