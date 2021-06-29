namespace Platform::Data
{
    template <typename ...> class LinkAddress;
    template <typename TLinkAddress> class LinkAddress<TLinkAddress> : public IEquatable<LinkAddress<TLinkAddress>>, IList<TLinkAddress>
    {
        public: const TLinkAddress Index;

        public: TLinkAddress this[std::int32_t index]
        {
            get
            {
                if (index == 0)
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
            return 1;
        }

        public: bool IsReadOnly()
        {
            return true;
        }

        public: LinkAddress(TLinkAddress index) { Index = index; }

        public: void Add(TLinkAddress item) { throw std::logic_error("Not supported exception."); }

        public: void Clear() { throw std::logic_error("Not supported exception."); }

        public: virtual bool Contains(TLinkAddress item) { return item == Index ? true : false; }

        public: void CopyTo(TLinkAddress array[], std::int32_t arrayIndex) { array[arrayIndex] = Index; }

        public: IEnumerator<TLinkAddress> GetEnumerator()
        {
            yield return Index;
        }

        public: virtual std::int32_t IndexOf(TLinkAddress item) { return item == Index ? 0 : -1; }

        public: void Insert(std::int32_t index, TLinkAddress item) { throw std::logic_error("Not supported exception."); }

        public: bool Remove(TLinkAddress item) { throw std::logic_error("Not supported exception."); }

        public: void RemoveAt(std::int32_t index) { throw std::logic_error("Not supported exception."); }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Index;
        }

        public: virtual bool operator ==(const LinkAddress<TLinkAddress> &other) const { return other == nullptr ? false : Index == other.Index; }

        public: operator TLinkAddress() const { return this->Index; }

        public: LinkAddress(TLinkAddress linkAddress) : LinkAddress(linkAddress) { }

        public: operator std::string() const { return Platform::Converters::To<std::string>(Index).data(); }

        public: friend std::ostream & operator <<(std::ostream &out, const LinkAddress<TLinkAddress> &obj) { return out << (std::string)obj; }

        public: static bool operator ==(LinkAddress<TLinkAddress> left, LinkAddress<TLinkAddress> right)
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
    };
}

namespace std
{
    template <typename TLinkAddress>
    struct hash<Platform::Data::LinkAddress<TLinkAddress>>
    {
        std::size_t operator()(const Platform::Data::LinkAddress<TLinkAddress> &obj) const
        {
            return Index.GetHashCode();
        }
    };
}
