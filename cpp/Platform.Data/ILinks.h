namespace Platform::Data {
template <typename Self, std::integral TLinkAddress,
          std::derived_from<LinksConstants<TLinkAddress>> TConstants>
class ILinks {
  auto &&self() &noexcept { return static_cast<Self &>(*this); }
  auto &&self() &&noexcept { return static_cast<Self &&>(*this); }
  auto &&self() const &noexcept { return static_cast<const Self &>(*this); }
  auto &&self() const &&noexcept { return static_cast<const Self &&>(*this); }

public:
  const TConstants Constants{};

  // TODO: maybe mark methods as const
  TLinkAddress Count(Interfaces::IArray auto &&restriction) const {
    return self().Count(restriction);
  }

  TLinkAddress Count() const {
    TLinkAddress array[0];
    return self().Count(array);
  }

  TLinkAddress Each(auto &&handler,
                    const Interfaces::IArray auto &restrictions) const {
    return self().Each(handler, restrictions);
  }

  TLinkAddress Create(Interfaces::IArray auto &&restriction) {
    return self().Create(restriction);
  }

  TLinkAddress Update(Interfaces::IArray auto &&substitution,
                      Interfaces::IArray auto &&restrictions) {
    return self().Update(substitution, restrictions);
  }

  TLinkAddress Update(Interfaces::IArray auto &&substitution,
                      std::convertible_to<TLinkAddress> auto... restrictions) {
    TLinkAddress array[] = {static_cast<TLinkAddress>(restrictions)...};
    return Update(substitution, array);
  }
  // EXTENSIONS
};
} // namespace Platform::Data
