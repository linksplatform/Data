namespace Platform::Data::Sequences
{
    template <typename ...> class ISequenceAppender;
    template <typename TLinkAddress> class ISequenceAppender<TLinkAddress>
    {
    public:
        virtual TLinkAddress Append(TLinkAddress sequence, TLinkAddress appendant) = 0;
    };
}
