namespace Platform::Data::Sequences
{
    template <typename ...> class ISequenceWalker;
    template <typename TLinkAddress> class ISequenceWalker<TLinkAddress>
    {
    public:
        IEnumerable<IList<TLinkAddress>> Walk(TLinkAddress sequence);
    };
}
