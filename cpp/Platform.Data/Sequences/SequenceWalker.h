namespace Platform::Data::Sequences
{
    class SequenceWalker
    {
        public: template <typename TLinkAddress> static void WalkRight(TLinkAddress sequence, Func<TLinkAddress, TLinkAddress> getSource, Func<TLinkAddress, TLinkAddress> getTarget, Func<TLinkAddress, bool> isElement, std::function<void(TLinkAddress)> visit)
        {
            auto stack = Stack<TLinkAddress>();
            auto element = sequence;
            if (isElement(element))
            {
                visit(element);
            }
            else
            {
                while (true)
                {
                    if (isElement(element))
                    {
                        if (stack.Count() == 0)
                        {
                            break;
                        }
                        element = stack.Pop();
                        auto source = getSource(element);
                        auto target = getTarget(element);
                        if (isElement(source))
                        {
                            visit(source);
                        }
                        if (isElement(target))
                        {
                            visit(target);
                        }
                        element = target;
                    }
                    else
                    {
                        stack.Push(element);
                        element = getSource(element);
                    }
                }
            }
        }

        public: template <typename TLinkAddress> static void WalkLeft(TLinkAddress sequence, Func<TLinkAddress, TLinkAddress> getSource, Func<TLinkAddress, TLinkAddress> getTarget, Func<TLinkAddress, bool> isElement, std::function<void(TLinkAddress)> visit)
        {
            auto stack = Stack<TLinkAddress>();
            auto element = sequence;
            if (isElement(element))
            {
                visit(element);
            }
            else
            {
                while (true)
                {
                    if (isElement(element))
                    {
                        if (stack.Count() == 0)
                        {
                            break;
                        }
                        element = stack.Pop();
                        auto source = getSource(element);
                        auto target = getTarget(element);
                        if (isElement(target))
                        {
                            visit(target);
                        }
                        if (isElement(source))
                        {
                            visit(source);
                        }
                        element = source;
                    }
                    else
                    {
                        stack.Push(element);
                        element = getTarget(element);
                    }
                }
            }
        }
    };
}
