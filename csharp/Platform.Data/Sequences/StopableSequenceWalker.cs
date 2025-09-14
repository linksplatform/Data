using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Data.Sequences
{
    public static class StopableSequenceWalker
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WalkRight<TLink>(TLink sequence, Func<TLink, TLink> getSource, Func<TLink, TLink> getTarget, Func<TLink, bool> isElement, Action<TLink> visit)
        {
            return WalkRight(sequence, getSource, getTarget, isElement, _ => { }, _ => { }, _ => true, element => { visit(element); return true; });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WalkRight<TLink>(TLink sequence, Func<TLink, TLink> getSource, Func<TLink, TLink> getTarget, Func<TLink, bool> isElement, 
            Action<TLink> enter, Action<TLink> exit, Func<TLink, bool> canEnter, Func<TLink, bool> visit)
        {
            var stack = new Stack<(TLink element, bool processed)>();
            stack.Push((sequence, false));
            
            while (stack.Count > 0)
            {
                var (current, processed) = stack.Pop();
                
                if (processed)
                {
                    exit(current);
                    continue;
                }
                
                if (!canEnter(current))
                    continue;
                
                enter(current);
                
                if (isElement(current))
                {
                    if (!visit(current))
                        return false;
                }
                else
                {
                    // Push exit marker
                    stack.Push((current, true));
                    
                    var source = getSource(current);
                    var target = getTarget(current);
                    
                    // Push in reverse order for right-to-left traversal
                    if (!Equals(target, default(TLink)))
                        stack.Push((target, false));
                    if (!Equals(source, default(TLink)))
                        stack.Push((source, false));
                }
            }
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WalkLeft<TLink>(TLink sequence, Func<TLink, TLink> getSource, Func<TLink, TLink> getTarget, Func<TLink, bool> isElement, 
            Action<TLink> enter, Action<TLink> exit, Func<TLink, bool> canEnter, Func<TLink, bool> visit)
        {
            var stack = new Stack<(TLink element, bool processed)>();
            stack.Push((sequence, false));
            
            while (stack.Count > 0)
            {
                var (current, processed) = stack.Pop();
                
                if (processed)
                {
                    exit(current);
                    continue;
                }
                
                if (!canEnter(current))
                    continue;
                
                enter(current);
                
                if (isElement(current))
                {
                    if (!visit(current))
                        return false;
                }
                else
                {
                    // Push exit marker
                    stack.Push((current, true));
                    
                    var source = getSource(current);
                    var target = getTarget(current);
                    
                    // Push in order for left-to-right traversal
                    if (!Equals(source, default(TLink)))
                        stack.Push((source, false));
                    if (!Equals(target, default(TLink)))
                        stack.Push((target, false));
                }
            }
            
            return true;
        }
    }
}