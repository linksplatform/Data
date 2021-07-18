namespace Platform::Data
{
    template<std::integral TLinkAddress>
    class LinksConstants: LinksConstantsBase
    {
        private: static constexpr TLinkAddress _one = /* Arithmetic<TLinkAddress>.Increment(default); */ 1; // o_o

        public: const int IndexPart{};

        public: const int SourcePart{};

        public: const int TargetPart{};

        public: const TLinkAddress Continue;

        public: const TLinkAddress Skip;

        public: const TLinkAddress Break;

        public: const TLinkAddress Null;

        public: const TLinkAddress Any;

        public: const TLinkAddress Itself;

        public: const Ranges::Range<TLinkAddress> InternalReferencesRange;

        public: const std::optional<Ranges::Range<TLinkAddress>> ExternalReferencesRange;

        public: constexpr LinksConstants(int targetPart, const Ranges::Range<TLinkAddress>& possibleInternalReferencesRange, std::optional<Ranges::Range<TLinkAddress>> possibleExternalReferencesRange) noexcept
            : IndexPart(0),
              SourcePart(1),
              TargetPart(targetPart),
              Null(TLinkAddress{}),
              Break(TLinkAddress{}),
              Continue(possibleInternalReferencesRange.Maximum),
              Skip(possibleInternalReferencesRange.Maximum - 1),
              Any(possibleInternalReferencesRange.Maximum - 2),
              Itself(possibleInternalReferencesRange.Maximum - 3),
              InternalReferencesRange(Ranges::Range{possibleInternalReferencesRange.Minimum, possibleInternalReferencesRange.Maximum - 4}),
              ExternalReferencesRange(std::move(possibleExternalReferencesRange))
        {
        }

        public: constexpr LinksConstants(int targetPart, bool enableExternalReferencesSupport) : LinksConstants(targetPart, GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) { }

        public: constexpr LinksConstants(Ranges::Range<TLinkAddress> possibleInternalReferencesRange, std::optional<Ranges::Range<TLinkAddress>> possibleExternalReferencesRange) : LinksConstants(DefaultTargetPart, possibleInternalReferencesRange, possibleExternalReferencesRange) { }

        public: constexpr explicit LinksConstants(bool enableExternalReferencesSupport) : LinksConstants(GetDefaultInternalReferencesRange(enableExternalReferencesSupport), GetDefaultExternalReferencesRange(enableExternalReferencesSupport)) { }

        public: constexpr LinksConstants(int targetPart, Ranges::Range<TLinkAddress> possibleInternalReferencesRange) : LinksConstants(targetPart, possibleInternalReferencesRange, std::nullopt) { }

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
