namespace Platform::Data
{
    template <typename ...> class Point;
    template <typename TLinkAddress> class Point<TLinkAddress> : public IEquatable<LinkAddress<TLinkAddress>>, IList<TLinkAddress>
    {
        public: const TLinkAddress Index;

        public: const std::int32_t Size;

        public: TLinkAddress this[std::int32_t index]
        {
            get
            {
                if (index < Size)
                {
                    return Index;
                }
                else
                {
                    throw IndexOutOfRangeException();
                }
            }
            set => throw std::logic_error("Not supported exception.");
        }

        public: std::int32_t Count()
        {
            return std::numeric_limits<std::int32_t>::max();
        }

        public: bool IsReadOnly()
        {
            return true;
        }

        public: Point(TLinkAddress index, std::int32_t size)
        {
            Index = index;
            Size = size;
        }

        public: void Add(TLinkAddress item) { throw std::logic_error("Not supported exception."); }

        public: void Clear() { throw std::logic_error("Not supported exception."); }

        public: virtual bool Contains(TLinkAddress item) { return item == Index ? true : false; }

        public: void CopyTo(TLinkAddress array[], std::int32_t arrayIndex) { array[arrayIndex] = Index; }

        public: IEnumerator<TLinkAddress> GetEnumerator()
        {
            for (std::int32_t i = 0; i < Size; i++)
            {
                yield return Index;
            }
        }

        public: virtual std::int32_t IndexOf(TLinkAddress item) { return item == Index ? 0 : -1; }

        public: void Insert(std::int32_t index, TLinkAddress item) { throw std::logic_error("Not supported exception."); }

        public: bool Remove(TLinkAddress item) { throw std::logic_error("Not supported exception."); }

        public: void RemoveAt(std::int32_t index) { throw std::logic_error("Not supported exception."); }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (std::int32_t i = 0; i < Size; i++)
            {
                yield return Index;
            }
        }

        public: virtual bool operator ==(const LinkAddress<TLinkAddress> &other) const { return other == nullptr ? false : Index == other.Index; }

        public: operator TLinkAddress() const { return this->Index; }

        public: operator std::string() const { return Platform::Converters::To<std::string>(Index).data(); }

        public: friend std::ostream & operator <<(std::ostream &out, const Point<TLinkAddress> &obj) { return out << (std::string)obj; }

        public: static bool operator ==(Point<TLinkAddress> left, Point<TLinkAddress> right)
        {
            if (left == nullptr && right == nullptr)
            {
                return true;
            }
            if (left == nullptr)
            {
                return false;
            }
            return left.Equals(right);
        }

        public: static bool IsFullPoint(params TLinkAddress link[]) { return IsFullPoint((IList<TLinkAddress>)link); }

        public: static bool IsFullPoint(IList<TLinkAddress> &link)
        {
            Ensure.Always.ArgumentNotEmpty(link, "link");
            Platform::Ranges::EnsureExtensions::ArgumentInRange(Platform::Exceptions::Ensure::Always, link.Count(), (2, std::numeric_limits<std::int32_t>::max()), "link", "Cannot determine link's pointness using only its identifier.");
            return IsFullPointUnchecked(link);
        }

        public: static bool IsFullPointUnchecked(IList<TLinkAddress> &link)
        {
            auto result = true;
            for (auto i = 1; result && i < link.Count(); i++)
            {
                result = link[0] == link[i];
            }
            return result;
        }

        public: static bool IsPartialPoint(params TLinkAddress link[]) { return IsPartialPoint((IList<TLinkAddress>)link); }

        public: static bool IsPartialPoint(IList<TLinkAddress> &link)
        {
            Ensure.Always.ArgumentNotEmpty(link, "link");
            Platform::Ranges::EnsureExtensions::ArgumentInRange(Platform::Exceptions::Ensure::Always, link.Count(), (2, std::numeric_limits<std::int32_t>::max()), "link", "Cannot determine link's pointness using only its identifier.");
            return IsPartialPointUnchecked(link);
        }

        public: static bool IsPartialPointUnchecked(IList<TLinkAddress> &link)
        {
            auto result = false;
            for (auto i = 1; !result && i < link.Count(); i++)
            {
                result = link[0] == link[i];
            }
            return result;
        }
    };
}

namespace std
{
    template <typename TLinkAddress>
    struct hash<Platform::Data::Point<TLinkAddress>>
    {
        std::size_t operator()(const Platform::Data::Point<TLinkAddress> &obj) const
        {
            return Index.GetHashCode();
        }
    };
}
