using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Data.Sequences
{
    public static class SequenceWalker
    {
        // TODO: Can use global stack (or multiple global stacks per thread)
        // TODO: Recursion depth limit might allow reduced stack size
        // TODO: Try to implement the algorithms using System.Reflection.Emit and a low-level stack
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkRight<TLink>(TLink sequence, Func<TLink, TLink> getSource, Func<TLink, TLink> getTarget, Func<TLink, bool> isElement, Action<TLink> visit)
        {
            var stack = new Stack<TLink>();
            stack.Push(sequence);
            
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                
                if (isElement(current))
                {
                    visit(current);
                }
                else
                {
                    var source = getSource(current);
                    var target = getTarget(current);
                    
                    // Push in reverse order for right-to-left traversal
                    if (!Equals(target, default(TLink)))
                        stack.Push(target);
                    if (!Equals(source, default(TLink)))
                        stack.Push(source);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkLeft<TLink>(TLink sequence, Func<TLink, TLink> getSource, Func<TLink, TLink> getTarget, Func<TLink, bool> isElement, Action<TLink> visit)
        {
            var stack = new Stack<TLink>();
            stack.Push(sequence);
            
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                
                if (isElement(current))
                {
                    visit(current);
                }
                else
                {
                    var source = getSource(current);
                    var target = getTarget(current);
                    
                    // Push in order for left-to-right traversal
                    if (!Equals(source, default(TLink)))
                        stack.Push(source);
                    if (!Equals(target, default(TLink)))
                        stack.Push(target);
                }
            }
        }
    }
}