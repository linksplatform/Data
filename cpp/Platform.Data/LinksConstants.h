/// @file LinksConstants.h
/// @brief Core constants and configuration values for the links system.

namespace Platform::Data
{
    /// @brief Configuration constants for the links system.
    /// @tparam TLinkAddress The integral type used for link addressing.
    /// 
    /// This structure contains all the essential constants used throughout the links system,
    /// including part indices, control flow values, and address ranges.
    template<std::integral TLinkAddress>
    struct LinksConstants
    {
        /// @brief Default index for the target part of a link.
        static constexpr int DefaultTargetPart = 2;
        /// @brief Index for the link identifier part (typically 0).
    public:
        const TLinkAddress IndexPart{};

        /// @brief Index for the source part of a link (typically 1).
    public:
        const TLinkAddress SourcePart{};

        /// @brief Index for the target part of a link (configurable, typically 2).
    public:
        const TLinkAddress TargetPart{};

        /// @brief Value indicating continuation of operations.
    public:
        const TLinkAddress Continue{};

        /// @brief Value indicating termination of operations.
    public:
        const TLinkAddress Break{};

        /// @brief Value indicating to skip current item in operations.
    public:
        const TLinkAddress Skip{};

        /// @brief Value representing null/empty reference.
    public:
        const TLinkAddress Null{};

        /// @brief Value representing any/wildcard match in queries.
    public:
        const TLinkAddress Any{};

        /// @brief Value representing self-reference.
    public:
        const TLinkAddress Itself{};

        /// @brief Value representing error condition.
    public:
        const TLinkAddress Error{};

    public:
        const Ranges::Range<TLinkAddress> InternalReferencesRange{};

    public:
         const Ranges::Range<TLinkAddress> ExternalReferencesRange = std::nullopt;
         const bool IsExternalReferencesRangeEnabled;

    public:
        constexpr LinksConstants(TLinkAddress targetPart, const Ranges::Range<TLinkAddress>& possibleInternalReferencesRange, Ranges::Range<TLinkAddress> possibleExternalReferencesRange) noexcept
            :
            IndexPart(0),
            SourcePart(1),
            TargetPart(targetPart),
            Continue(possibleInternalReferencesRange.Maximum),
            Break(possibleInternalReferencesRange.Maximum - 1),
            Skip(possibleInternalReferencesRange.Maximum - 2),
            Null(TLinkAddress{}),
            Any(possibleInternalReferencesRange.Maximum - 3),
            Itself(possibleInternalReferencesRange.Maximum - 4),
            Error(possibleInternalReferencesRange.Maximum - 5),
            InternalReferencesRange(Ranges::Range{possibleInternalReferencesRange.Minimum, static_cast<TLinkAddress>(possibleInternalReferencesRange.Maximum - 6)}),
            ExternalReferencesRange(possibleExternalReferencesRange),
            IsExternalReferencesRangeEnabled(possibleExternalReferencesRange.Minimum != possibleExternalReferencesRange.Maximum)
        {
        }

    public:
        constexpr explicit LinksConstants(TLinkAddress targetPart, bool enableExternalReferencesSupport) noexcept :
            LinksConstants(targetPart, GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport))
        {
        }

    public:
        constexpr explicit LinksConstants(Ranges::Range<TLinkAddress> possibleInternalReferencesRange, Ranges::Range<TLinkAddress> possibleExternalReferencesRange) noexcept :
            LinksConstants(DefaultTargetPart, possibleInternalReferencesRange, possibleExternalReferencesRange)
        {
        }

    public:
        constexpr explicit LinksConstants(bool enableExternalReferencesSupport) noexcept :
            LinksConstants(GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport))
        {
        }

    public:
        constexpr explicit LinksConstants(TLinkAddress targetPart, Ranges::Range<TLinkAddress> possibleInternalReferencesRange) noexcept :
            LinksConstants(targetPart, possibleInternalReferencesRange, std::nullopt)
        {
        }

    public:
        constexpr explicit LinksConstants(Ranges::Range<TLinkAddress> possibleInternalReferencesRange) noexcept :
            LinksConstants(DefaultTargetPart, possibleInternalReferencesRange, std::nullopt)
        {
        }

    public:
        constexpr explicit LinksConstants() noexcept :
            LinksConstants(DefaultTargetPart, false)
        {
        }

    public:
        static constexpr Ranges::Range<TLinkAddress> GetDefaultInternalReferencesRange(bool enableExternalReferencesSupport) noexcept
        {
            using namespace Ranges;

            if (enableExternalReferencesSupport)
            {
                return Range{1, static_cast<TLinkAddress>(Hybrid<TLinkAddress>::HalfOfNumberValuesRange)};
            }
            else
            {
                return Range{1, std::numeric_limits<TLinkAddress>::max()};
            }
        }

    public:
        static constexpr auto GetDefaultExternalReferencesRange(bool enableExternalReferencesSupport) noexcept -> Ranges::Range<TLinkAddress>
        {
            if (enableExternalReferencesSupport)
            {
                return Ranges::Range{Hybrid<TLinkAddress>::ExternalZero, std::numeric_limits<TLinkAddress>::max()};
            }
            else
            {
                return Ranges::Range<TLinkAddress>{0};
            }
        }
    };
}// namespace Platform::Data
