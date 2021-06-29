namespace Platform::Data::Sequences
{
    class StopableSequenceWalker
    {
        public: template <typename TLinkAddress> static bool WalkRight(TLinkAddress sequence, Func<TLinkAddress, TLinkAddress> getSource, Func<TLinkAddress, TLinkAddress> getTarget, Func<TLinkAddress, bool> isElement, std::function<void(TLinkAddress)> enter, std::function<void(TLinkAddress)> exit, Func<TLinkAddress, bool> canEnter, Func<TLinkAddress, bool> visit)
        {
            auto exited = 0;
            auto stack = Stack<TLinkAddress>();
            auto element = sequence;
            if (isElement(element))
            {
                return visit(element);
            }
            while (true)
            {
                if (isElement(element))
                {
                    if (stack.Count() == 0)
                    {
                        return true;
                    }
                    element = stack.Pop();
                    exit(element);
                    exited++;
                    auto source = getSource(element);
                    auto target = getTarget(element);
                    if ((isElement(source) || (exited == 1 && !canEnter(source))) && !visit(source))
                    {
                        return false;
                    }
                    if ((isElement(target) || !canEnter(target)) && !visit(target))
                    {
                        return false;
                    }
                    element = target;
                }
                else
                {
                    if (canEnter(element))
                    {
                        enter(element);
                        exited = 0;
                        stack.Push(element);
                        element = getSource(element);
                    }
                    else
                    {
                        if (stack.Count() == 0)
                        {
                            return true;
                        }
                        element = stack.Pop();
                        exit(element);
                        exited++;
                        auto source = getSource(element);
                        auto target = getTarget(element);
                        if ((isElement(source) || (exited == 1 && !canEnter(source))) && !visit(source))
                        {
                            return false;
                        }
                        if ((isElement(target) || !canEnter(target)) && !visit(target))
                        {
                            return false;
                        }
                        element = target;
                    }
                }
            }
        }

        public: template <typename TLinkAddress> static bool WalkRight(TLinkAddress sequence, Func<TLinkAddress, TLinkAddress> getSource, Func<TLinkAddress, TLinkAddress> getTarget, Func<TLinkAddress, bool> isElement, Func<TLinkAddress, bool> visit)
        {
            auto stack = Stack<TLinkAddress>();
            auto element = sequence;
            if (isElement(element))
            {
                return visit(element);
            }
            while (true)
            {
                if (isElement(element))
                {
                    if (stack.Count() == 0)
                    {
                        return true;
                    }
                    element = stack.Pop();
                    auto source = getSource(element);
                    auto target = getTarget(element);
                    if (isElement(source) && !visit(source))
                    {
                        return false;
                    }
                    if (isElement(target) && !visit(target))
                    {
                        return false;
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

        public: template <typename TLinkAddress> static bool WalkLeft(TLinkAddress sequence, Func<TLinkAddress, TLinkAddress> getSource, Func<TLinkAddress, TLinkAddress> getTarget, Func<TLinkAddress, bool> isElement, Func<TLinkAddress, bool> visit)
        {
            auto stack = Stack<TLinkAddress>();
            auto element = sequence;
            if (isElement(element))
            {
                return visit(element);
            }
            while (true)
            {
                if (isElement(element))
                {
                    if (stack.Count() == 0)
                    {
                        return true;
                    }
                    element = stack.Pop();
                    auto source = getSource(element);
                    auto target = getTarget(element);
                    if (isElement(target) && !visit(target))
                    {
                        return false;
                    }
                    if (isElement(source) && !visit(source))
                    {
                        return false;
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
    };
}
