namespace Platform::Data
{
    template<std::integral TLinkAddress>
    class LinksConstants : LinksConstantsBase
    {
        private: static constexpr TLinkAddress _one = /* Arithmetic<TLinkAddress>.Increment(default); */ 1; // o_o

        public: const TLinkAddress IndexPart{};

        public: const TLinkAddress SourcePart{};

        public: const TLinkAddress TargetPart{};

        public: const TLinkAddress Break{};

        public: const TLinkAddress Null{};

        public: const TLinkAddress Continue{};

        public: const TLinkAddress Skip{};

        public: const TLinkAddress Any{};

        public: const TLinkAddress Itself{};

        public: const Ranges::Range<TLinkAddress> InternalReferencesRange{};

        public: const std::optional<Ranges::Range<TLinkAddress>> ExternalReferencesRange = std::nullopt;

        public: constexpr LinksConstants(TLinkAddress targetPart, const Ranges::Range<TLinkAddress>& possibleInternalReferencesRange, std::optional<Ranges::Range<TLinkAddress>> possibleExternalReferencesRange) noexcept
            : IndexPart(0),
              SourcePart(1),
              TargetPart(targetPart),
              Break(TLinkAddress{}),
              Null(TLinkAddress{}),
              Continue(possibleInternalReferencesRange.Maximum),
              Skip(possibleInternalReferencesRange.Maximum - 1),
              Any(possibleInternalReferencesRange.Maximum - 2),
              Itself(possibleInternalReferencesRange.Maximum - 3),
              InternalReferencesRange(Ranges::Range{possibleInternalReferencesRange.Minimum, static_cast<TLinkAddress>(possibleInternalReferencesRange.Maximum - 4)}),
              ExternalReferencesRange(std::move(possibleExternalReferencesRange))
        {
        }

        public: constexpr LinksConstants(TLinkAddress targetPart, bool enableExternalReferencesSupport) : LinksConstants(targetPart, GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) { }

        public: constexpr LinksConstants(Ranges::Range<TLinkAddress> possibleInternalReferencesRange, std::optional<Ranges::Range<TLinkAddress>> possibleExternalReferencesRange) : LinksConstants(DefaultTargetPart, possibleInternalReferencesRange, possibleExternalReferencesRange) { }

        public: constexpr explicit LinksConstants(bool enableExternalReferencesSupport) : LinksConstants(GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) { }

        public: constexpr LinksConstants(TLinkAddress targetPart, Ranges::Range<TLinkAddress> possibleInternalReferencesRange) : LinksConstants(targetPart, possibleInternalReferencesRange, std::nullopt) { }

        public: constexpr explicit LinksConstants(Ranges::Range<TLinkAddress> possibleInternalReferencesRange) : LinksConstants(DefaultTargetPart, possibleInternalReferencesRange, std::nullopt) { }

        public: constexpr LinksConstants() : LinksConstants(DefaultTargetPart, false) { }

        public: static constexpr Ranges::Range<TLinkAddress> GetDefaultInternalReferencesRange(bool enableExternalReferencesSupport)
        {
            if (enableExternalReferencesSupport)
            {
                return Ranges::Range{ _one, static_cast<TLinkAddress>(Hybrid<TLinkAddress>::HalfOfNumberValuesRange) };
            }
            else
            {
                return Ranges::Range{ _one, std::numeric_limits<TLinkAddress>::max() };
            }
        }

        public: static constexpr auto GetDefaultExternalReferencesRange(bool enableExternalReferencesSupport) -> std::optional<Ranges::Range<TLinkAddress>>
        {
            if (enableExternalReferencesSupport)
            {
                return std::optional(Ranges::Range{Hybrid<TLinkAddress>::ExternalZero, std::numeric_limits<TLinkAddress>::max()});
            }
            else
            {
                return std::nullopt;
            }
        }
    };
}
