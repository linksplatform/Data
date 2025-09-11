/// @file Point.h
/// @brief Defines Point class for representing repeated link elements.
///
/// A point in the context of links is a pattern where multiple positions
/// in a link contain the same value, indicating a self-reference or loop.

#include <algorithm>

namespace Platform::Data
{
    namespace Internal
    {
        /// @brief Creates a function that always returns the specified index value.
        /// @param index The index value to return for any input.
        /// @return Lambda function that ignores input and returns the fixed index.
        auto gen_always_index(auto index)
        {
            return [index](auto) { return index; };
        }

        /// @brief Base type for Point class using ranges view.
        /// @tparam TLinkAddress The address type for the point.
        template<typename TLinkAddress>
        using PointBase = decltype(std::views::iota(std::size_t{}, std::size_t{}) | std::views::transform(Internal::gen_always_index(TLinkAddress{})));
    }

    /// @brief Represents a point - a link structure where multiple positions contain the same address.
    /// @tparam TLinkAddress The integral type used for addressing.
    /// 
    /// A point is a special case of a link where one or more positions contain the same value,
    /// creating self-references or loops. This class provides container-like semantics for
    /// accessing repeated values.
    template <std::integral TLinkAddress>
    class Point : public Internal::PointBase<TLinkAddress>
    {
        /// @brief Base class type alias.
        using base = Internal::PointBase<TLinkAddress>;

        /// @brief The repeated index value.
        // TODO: maybe remove or convert to property
        public: const TLinkAddress Index;

        /// @brief The size of the point (number of repetitions).
        // TODO: maybe remove or convert to property
        public: const std::size_t Size;

        /// @brief Provides indexed access to the repeated value.
        /// @param index The position to access.
        /// @return The Index value (same for all valid positions).
        /// @throws std::out_of_range if index >= Size.
        public: auto operator[](std::size_t index) const -> TLinkAddress
        {
            if (index < Size)
            {
                return Index;
            }
            else
            {
                throw std::out_of_range{""};
            }
        }

        /// @brief Constructs a Point with specified index and size.
        /// @param index The value to repeat at all positions.
        /// @param size The number of repetitions.
        public: Point(TLinkAddress index, std::size_t size) :
            Index(index), Size(size), base(std::views::iota(std::size_t(0), size), Internal::gen_always_index(index))
        {
        }

        /// @brief Equality comparison with LinkAddress.
        /// @param other The LinkAddress to compare with.
        /// @return True if Index values are equal.
        public: auto operator==(const LinkAddress<TLinkAddress> &other) const noexcept
        {
            return Index == other.Index;
        }

        /// @brief Explicit conversion to the underlying address type.
        /// @return The Index value.
        public: explicit operator TLinkAddress() const noexcept { return Index; }

        /// @brief Explicit conversion to string representation.
        /// @return String representation of the Index value.
        public: explicit operator std::string() const { return Converters::To<std::string>(Index).data(); }

        /// @brief Stream output operator.
        /// @param stream The output stream.
        /// @param self The Point object to output.
        /// @return Reference to the output stream.
        public: friend std::ostream& operator<<(std::ostream& stream, const Point<TLinkAddress>& self) { return stream << static_cast<std::string>(self); }
    };

    /// @brief Deduction guide for Point template parameter inference.
    template<std::integral TLinkAddress, typename... Args>
    Point(TLinkAddress, Args...) -> Point<TLinkAddress>;

    /// @brief Checks if a link is a full point without validation (all elements are the same).
    /// @param link The link to check.
    /// @return True if all elements after the first are equal to the first element.
    static bool IsFullPointUnchecked(Interfaces::CEnumerable auto&& link)
    requires std::integral<typename Interfaces::Enumerable<decltype(link)>::Item>
    {
        auto iter = std::ranges::begin(link);
        auto end = std::ranges::end(link);
        return std::ranges::all_of(iter + 1, end, [&iter](auto&& item) {return item == *iter; });
    }

    /// @brief Checks if a link is a full point with validation (all elements are the same).
    /// @param link The link to check.
    /// @return True if all elements are equal and link size is valid.
    /// @throws ArgumentOutOfRangeException if link size is less than 2.
    static bool IsFullPoint(Interfaces::CEnumerable auto&& link)
    requires std::integral<typename Interfaces::Enumerable<decltype(link)>::Item>
    {
        using namespace Ranges;
        constexpr auto link_range = Range{2, std::numeric_limits<std::int32_t>::max()};

        Platform::Ranges::Ensure::Always::ArgumentInRange(std::ranges::size(link), link_range, "link", "Cannot determine link's pointness using only its identifier.");
        return IsFullPointUnchecked(link);
    }

    /// @brief Checks if parameters form a full point (all values are the same).
    /// @param params Variable arguments representing link elements.
    /// @return True if all parameters are equal.
    static bool IsFullPoint(std::integral auto... params)
    {
        std::common_type_t<decltype(params)...> link[] = { params... };
        return IsFullPoint(link);
    }

    /// @brief Checks if a link is a partial point without validation (some elements are the same).
    /// @param link The link to check.
    /// @return True if any element after the first equals the first element.
    static bool IsPartialPointUnchecked(Interfaces::CEnumerable auto&& link)
    requires std::integral<typename Interfaces::Enumerable<decltype(link)>::Item>
    {
        auto iter = std::ranges::begin(link);
        auto end = std::ranges::end(link);

        return std::ranges::any_of(iter + 1, end, [&iter](auto&& item) { return item == *iter; });
    }

    /// @brief Checks if a link is a partial point with validation (some elements are the same).
    /// @param link The link to check.
    /// @return True if at least one element equals the first element and link size is valid.
    /// @throws ArgumentOutOfRangeException if link size is less than 2.
    static bool IsPartialPoint(Interfaces::CEnumerable auto&& link)
    {
        using namespace Platform::Ranges;

        // TODO: after my PR
        Ensure::Always::ArgumentInRange(std::ranges::size(link) , Range{2, std::numeric_limits<std::int32_t>::max()}, "link", "Cannot determine link's pointness using only its identifier.");
        return IsPartialPointUnchecked(link);
    }

    /// @brief Checks if parameters form a partial point (some values are the same).
    /// @param params Variable arguments representing link elements.
    /// @return True if any parameter equals the first parameter.
    // TODO: we don't have args... to array-like converter
    static bool IsPartialPoint(std::integral auto... params)
    {
        std::common_type_t<decltype(params)...> link[] = { params... };
        static_assert(Interfaces::CEnumerable<decltype(link)>);
        return IsPartialPoint(link);
    }
}


/// @brief Hash function specialization for Point to enable use in hash-based containers.
/// @tparam TLinkAddress The address type of the Point.
template <typename TLinkAddress>
struct std::hash<Platform::Data::Point<TLinkAddress>>
{
    /// @brief Computes hash value for a Point object.
    /// @param self The Point object to hash.
    /// @return Hash value based on the Index member.
    std::size_t operator()(const Platform::Data::Point<TLinkAddress>& self) const noexcept
    {
        return self.Index;
    }
};

