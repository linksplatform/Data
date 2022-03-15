namespace Platform::Data
{
    template<std::integral TLinkAddress>
    struct LinksConstants : LinksConstantsBase
    {
    public:
        const TLinkAddress IndexPart{};

    public:
        const TLinkAddress SourcePart{};

    public:
        const TLinkAddress TargetPart{};

    public:
        const TLinkAddress Continue{};

    public:
        const TLinkAddress Break{};

    public:
        const TLinkAddress Skip{};

    public:
        const TLinkAddress Null{};

    public:
        const TLinkAddress Any{};

    public:
        const TLinkAddress Itself{};

    public:
        const TLinkAddress Error{};

    public:
        const Ranges::Range<TLinkAddress> InternalReferencesRange{};

    public:
        const Ranges::Range<TLinkAddress> ExternalReferencesRange = std::nullopt;

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
            ExternalReferencesRange(possibleExternalReferencesRange)
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
