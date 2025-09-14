using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Platform.Data.Sequences
{
    public static class EmitOptimizedSequenceWalker
    {
        private static readonly ConcurrentDictionary<string, Delegate> _compiledMethods = new();
        
        // Global stack per thread to avoid allocation overhead
        [ThreadStatic]
        private static Stack<object>? _globalStack;
        
        private static Stack<object> GetGlobalStack()
        {
            return _globalStack ??= new Stack<object>(256); // Pre-allocate for better performance
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkRight<TLink>(TLink sequence, Func<TLink, TLink> getSource, Func<TLink, TLink> getTarget, 
            Func<TLink, bool> isElement, Action<TLink> visit)
        {
            var compiled = GetOrCreateCompiledWalker<TLink>(WalkerDirection.Right);
            compiled(sequence, getSource, getTarget, isElement, visit);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkLeft<TLink>(TLink sequence, Func<TLink, TLink> getSource, Func<TLink, TLink> getTarget, 
            Func<TLink, bool> isElement, Action<TLink> visit)
        {
            var compiled = GetOrCreateCompiledWalker<TLink>(WalkerDirection.Left);
            compiled(sequence, getSource, getTarget, isElement, visit);
        }
        
        private static Action<TLink, Func<TLink, TLink>, Func<TLink, TLink>, Func<TLink, bool>, Action<TLink>> 
            GetOrCreateCompiledWalker<TLink>(WalkerDirection direction)
        {
            var key = $"{typeof(TLink).FullName}_{direction}";
            
            return (Action<TLink, Func<TLink, TLink>, Func<TLink, TLink>, Func<TLink, bool>, Action<TLink>>)
                _compiledMethods.GetOrAdd(key, _ => CreateCompiledWalker<TLink>(direction));
        }
        
        private static Delegate CreateCompiledWalker<TLink>(WalkerDirection direction)
        {
            var method = new DynamicMethod(
                $"CompiledWalk{direction}_{typeof(TLink).Name}",
                typeof(void),
                new[] { typeof(TLink), typeof(Func<TLink, TLink>), typeof(Func<TLink, TLink>), typeof(Func<TLink, bool>), typeof(Action<TLink>) },
                typeof(EmitOptimizedSequenceWalker),
                true);
            
            var il = method.GetILGenerator();
            
            // Local variables
            var stackLocal = il.DeclareLocal(typeof(Stack<object>)); // stack
            var currentLocal = il.DeclareLocal(typeof(object));       // current
            var sourceLocal = il.DeclareLocal(typeof(TLink));         // source  
            var targetLocal = il.DeclareLocal(typeof(TLink));         // target
            var typedCurrentLocal = il.DeclareLocal(typeof(TLink));   // typed current
            
            // Labels
            var loopStart = il.DefineLabel();
            var loopEnd = il.DefineLabel();
            var isElementLabel = il.DefineLabel();
            var notElementLabel = il.DefineLabel();
            var checkTarget = il.DefineLabel();
            var checkSource = il.DefineLabel();
            var skipTarget = il.DefineLabel();
            var skipSource = il.DefineLabel();
            
            // Get global stack
            il.EmitCall(OpCodes.Call, typeof(EmitOptimizedSequenceWalker).GetMethod(nameof(GetGlobalStack), BindingFlags.NonPublic | BindingFlags.Static)!, null);
            il.Emit(OpCodes.Stloc, stackLocal);
            
            // Clear the stack (in case it has leftover items)
            il.Emit(OpCodes.Ldloc, stackLocal);
            il.EmitCall(OpCodes.Callvirt, typeof(Stack<object>).GetMethod("Clear")!, null);
            
            // Push initial sequence (boxed)
            il.Emit(OpCodes.Ldloc, stackLocal);
            il.Emit(OpCodes.Ldarg_0); // sequence
            il.Emit(OpCodes.Box, typeof(TLink));
            il.EmitCall(OpCodes.Callvirt, typeof(Stack<object>).GetMethod("Push")!, null);
            
            // Main loop
            il.MarkLabel(loopStart);
            
            // Check if stack is empty
            il.Emit(OpCodes.Ldloc, stackLocal);
            il.EmitCall(OpCodes.Callvirt, typeof(Stack<object>).GetProperty("Count")!.GetGetMethod()!, null);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Beq, loopEnd);
            
            // Pop current
            il.Emit(OpCodes.Ldloc, stackLocal);
            il.EmitCall(OpCodes.Callvirt, typeof(Stack<object>).GetMethod("Pop")!, null);
            il.Emit(OpCodes.Stloc, currentLocal);
            
            // Unbox current
            il.Emit(OpCodes.Ldloc, currentLocal);
            il.Emit(OpCodes.Unbox_Any, typeof(TLink));
            il.Emit(OpCodes.Stloc, typedCurrentLocal);
            
            // Check if element
            il.Emit(OpCodes.Ldarg, 3); // isElement
            il.Emit(OpCodes.Ldloc, typedCurrentLocal);
            il.EmitCall(OpCodes.Callvirt, typeof(Func<TLink, bool>).GetMethod("Invoke")!, null);
            il.Emit(OpCodes.Brtrue, isElementLabel);
            
            // Not an element - get source and target
            il.MarkLabel(notElementLabel);
            
            // Get source
            il.Emit(OpCodes.Ldarg_1); // getSource
            il.Emit(OpCodes.Ldloc, typedCurrentLocal);
            il.EmitCall(OpCodes.Callvirt, typeof(Func<TLink, TLink>).GetMethod("Invoke")!, null);
            il.Emit(OpCodes.Stloc, sourceLocal);
            
            // Get target
            il.Emit(OpCodes.Ldarg_2); // getTarget
            il.Emit(OpCodes.Ldloc, typedCurrentLocal);
            il.EmitCall(OpCodes.Callvirt, typeof(Func<TLink, TLink>).GetMethod("Invoke")!, null);
            il.Emit(OpCodes.Stloc, targetLocal);
            
            // Push elements based on direction
            if (direction == WalkerDirection.Right)
            {
                // Push target first (for right-to-left traversal)
                il.Emit(OpCodes.Br, checkTarget);
                
                il.MarkLabel(checkTarget);
                EmitPushIfNotDefault(il, stackLocal, targetLocal, typeof(TLink));
                
                il.Emit(OpCodes.Br, checkSource);
                
                il.MarkLabel(checkSource);
                EmitPushIfNotDefault(il, stackLocal, sourceLocal, typeof(TLink));
            }
            else
            {
                // Push source first (for left-to-right traversal)
                il.Emit(OpCodes.Br, checkSource);
                
                il.MarkLabel(checkSource);
                EmitPushIfNotDefault(il, stackLocal, sourceLocal, typeof(TLink));
                
                il.Emit(OpCodes.Br, checkTarget);
                
                il.MarkLabel(checkTarget);
                EmitPushIfNotDefault(il, stackLocal, targetLocal, typeof(TLink));
            }
            
            il.Emit(OpCodes.Br, loopStart);
            
            // Is element - visit it
            il.MarkLabel(isElementLabel);
            il.Emit(OpCodes.Ldarg, 4); // visit
            il.Emit(OpCodes.Ldloc, typedCurrentLocal);
            il.EmitCall(OpCodes.Callvirt, typeof(Action<TLink>).GetMethod("Invoke")!, null);
            il.Emit(OpCodes.Br, loopStart);
            
            // End of method
            il.MarkLabel(loopEnd);
            il.Emit(OpCodes.Ret);
            
            return method.CreateDelegate(typeof(Action<TLink, Func<TLink, TLink>, Func<TLink, TLink>, Func<TLink, bool>, Action<TLink>>));
        }
        
        private static void EmitPushIfNotDefault(ILGenerator il, LocalBuilder stackLocal, LocalBuilder valueLocal, Type linkType)
        {
            var skipLabel = il.DefineLabel();
            
            // Load value and default for comparison
            il.Emit(OpCodes.Ldloc, valueLocal);
            
            // For reference types, compare with null
            if (!linkType.IsValueType)
            {
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Beq, skipLabel);
            }
            else
            {
                // For value types, use default comparison
                var defaultLocal = il.DeclareLocal(linkType);
                il.Emit(OpCodes.Ldloca, defaultLocal);
                il.Emit(OpCodes.Initobj, linkType);
                il.Emit(OpCodes.Ldloc, defaultLocal);
                
                // Use EqualityComparer<T>.Default.Equals for value type comparison
                var comparerType = typeof(EqualityComparer<>).MakeGenericType(linkType);
                il.EmitCall(OpCodes.Call, comparerType.GetProperty("Default")!.GetGetMethod()!, null);
                il.Emit(OpCodes.Ldloc, valueLocal);
                il.Emit(OpCodes.Ldloc, defaultLocal);
                il.EmitCall(OpCodes.Callvirt, comparerType.GetMethod("Equals", new[] { linkType, linkType })!, null);
                il.Emit(OpCodes.Brtrue, skipLabel);
            }
            
            // Push value (boxed) onto stack
            il.Emit(OpCodes.Ldloc, stackLocal);
            il.Emit(OpCodes.Ldloc, valueLocal);
            il.Emit(OpCodes.Box, linkType);
            il.EmitCall(OpCodes.Callvirt, typeof(Stack<object>).GetMethod("Push")!, null);
            
            il.MarkLabel(skipLabel);
        }
        
        private enum WalkerDirection
        {
            Left,
            Right
        }
    }
}