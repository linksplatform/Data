#include <algorithm>

namespace Platform::Data
{
    namespace Internal
    {
        auto gen_always_index(auto index)
        {
            return [index](auto) { return index; };
        }

        template<typename TLinkAddress>
        using PointBase = decltype(std::views::iota(std::size_t{}, std::size_t{}) | std::views::transform(Internal::gen_always_index(TLinkAddress{})));
    }

    template <std::integral TLinkAddress>
    class Point : public Internal::PointBase<TLinkAddress>
    {
        using base = Internal::PointBase<TLinkAddress>;

        // TODO: maybe remove or convert to property
        public: const TLinkAddress Index;

        // TODO: maybe remove or convert to property
        public: const std::size_t Size;

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

        public: Point(TLinkAddress index, std::size_t size) :
            Index(index), Size(size), base(std::views::iota(std::size_t(0), size), Internal::gen_always_index(index))
        {
        }

        public: auto operator==(const LinkAddress<TLinkAddress> &other) const noexcept
        {
            return Index == other.Index;
        }

        public: explicit operator TLinkAddress() const noexcept { return Index; }

        public: explicit operator std::string() const { return Converters::To<std::string>(Index).data(); }

        public: friend std::ostream& operator<<(std::ostream& stream, const Point<TLinkAddress>& self) { return stream << static_cast<std::string>(self); }
    };

    template<std::integral TLinkAddress, typename... Args>
    Point(TLinkAddress, Args...) -> Point<TLinkAddress>;

    static bool IsFullPointUnchecked(Interfaces::CEnumerable auto&& link)
    requires std::integral<typename Interfaces::Enumerable<decltype(link)>::Item>
    {
        auto iter = std::ranges::begin(link);
        auto end = std::ranges::end(link);
        return std::ranges::all_of(iter + 1, end, [&iter](auto&& item) {return item == *iter; });
    }

    static bool IsFullPoint(Interfaces::CEnumerable auto&& link)
    requires std::integral<typename Interfaces::Enumerable<decltype(link)>::Item>
    {
        using namespace Ranges;
        constexpr auto link_range = Range{2, std::numeric_limits<std::int32_t>::max()};

        Platform::Ranges::Ensure::Always::ArgumentInRange(std::ranges::size(link), link_range, "link", "Cannot determine link's pointness using only its identifier.");
        return IsFullPointUnchecked(link);
    }

    static bool IsFullPoint(std::integral auto... params)
    {
        std::common_type_t<decltype(params)...> link[] = { params... };
        return IsFullPoint(link);
    }

    static bool IsPartialPointUnchecked(Interfaces::CEnumerable auto&& link)
    requires std::integral<typename Interfaces::Enumerable<decltype(link)>::Item>
    {
        auto iter = std::ranges::begin(link);
        auto end = std::ranges::end(link);

        return std::ranges::any_of(iter + 1, end, [&iter](auto&& item) { return item == *iter; });
    }

    static bool IsPartialPoint(Interfaces::CEnumerable auto&& link)
    {
        using namespace Platform::Ranges;

        // TODO: after my PR
        Ensure::Always::ArgumentInRange(std::ranges::size(link) , Range{2, std::numeric_limits<std::int32_t>::max()}, "link", "Cannot determine link's pointness using only its identifier.");
        return IsPartialPointUnchecked(link);
    }

    // TODO: we don't have args... to array-like converter
    static bool IsPartialPoint(std::integral auto... params)
    {
        std::common_type_t<decltype(params)...> link[] = { params... };
        static_assert(Interfaces::CEnumerable<decltype(link)>);
        return IsPartialPoint(link);
    }
}


template <typename TLinkAddress>
struct std::hash<Platform::Data::Point<TLinkAddress>>
{
    std::size_t operator()(const Platform::Data::Point<TLinkAddress>& self) const noexcept
    {
        return self.Index;
    }
};

