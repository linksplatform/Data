using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Platform.Exceptions;
using Platform.Reflection;
using Platform.Converters;
using Platform.Numbers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    public struct Hybrid<TLinkAddress> : IEquatable<Hybrid<TLinkAddress>>
    {
        public static readonly ulong HalfOfNumberValuesRange = ((ulong)(Integer<TLinkAddress>)NumericType<TLinkAddress>.MaxValue) / 2;
        public static readonly TLinkAddress ExternalZero = (Integer<TLinkAddress>)(HalfOfNumberValuesRange + 1UL);

        private static readonly EqualityComparer<TLinkAddress> _equalityComparer = EqualityComparer<TLinkAddress>.Default;
        private static readonly Func<object, TLinkAddress> _negateAndConvert = CompileNegateAndConvertDelegate();
        private static readonly Func<object, TLinkAddress> _unboxAbsAndConvert = CompileUnboxAbsAndConvertDelegate();
        private static readonly Func<object, TLinkAddress> _unboxAbsNegateAndConvert = CompileUnboxAbsNegateAndConvertDelegate();

        public readonly TLinkAddress Value;

        public bool IsNothing
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _equalityComparer.Equals(Value, ExternalZero) || Convert.ToInt64(Value) == 0;
        }

        public bool IsInternal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Convert.ToInt64(To.Signed(Value)) > 0;
        }

        public bool IsExternal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _equalityComparer.Equals(Value, ExternalZero) || Convert.ToInt64(To.Signed(Value)) < 0;
        }

        public long AbsoluteValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _equalityComparer.Equals(Value, ExternalZero) ? 0 : Platform.Numbers.Math.Abs(Convert.ToInt64(To.Signed(Value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Hybrid(TLinkAddress value)
        {
            Ensure.OnDebug.IsUnsignedInteger<TLinkAddress>();
            Value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Hybrid(TLinkAddress value, bool isExternal)
        {
            if (_equalityComparer.Equals(value, default) && isExternal)
            {
                Value = ExternalZero;
            }
            else
            {
                if (isExternal)
                {
                    Value = _negateAndConvert(value);
                }
                else
                {
                    Value = value;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Hybrid(object value) => Value = To.UnsignedAs<TLinkAddress>(Convert.ChangeType(value, NumericType<TLinkAddress>.SignedVersion));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Hybrid(object value, bool isExternal)
        {
            if (IsDefault(value) && isExternal)
            {
                Value = ExternalZero;
            }
            else
            {
                if (isExternal)
                {
                    Value = _unboxAbsNegateAndConvert(value);
                }
                else
                {
                    Value = _unboxAbsAndConvert(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Hybrid<TLinkAddress>(TLinkAddress integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(ulong integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(long integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(uint integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(int integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(ushort integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(short integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(byte integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Hybrid<TLinkAddress>(sbyte integer) => new Hybrid<TLinkAddress>(integer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator TLinkAddress(Hybrid<TLinkAddress> hybrid) => hybrid.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator ulong(Hybrid<TLinkAddress> hybrid) => Convert.ToUInt64(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator long(Hybrid<TLinkAddress> hybrid) => hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint(Hybrid<TLinkAddress> hybrid) => Convert.ToUInt32(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator int(Hybrid<TLinkAddress> hybrid) => (int)hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator ushort(Hybrid<TLinkAddress> hybrid) => Convert.ToUInt16(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator short(Hybrid<TLinkAddress> hybrid) => (short)hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator byte(Hybrid<TLinkAddress> hybrid) => Convert.ToByte(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator sbyte(Hybrid<TLinkAddress> hybrid) => (sbyte)hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => IsNothing ? default(TLinkAddress) == null ? "Nothing" : default(TLinkAddress).ToString() : IsExternal ? $"<{AbsoluteValue}>" : Value.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Hybrid<TLinkAddress> other) => _equalityComparer.Equals(Value, other.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Hybrid<TLinkAddress> hybrid ? Equals(hybrid) : false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => Value.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Hybrid<TLinkAddress> left, Hybrid<TLinkAddress> right) => left.Equals(right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Hybrid<TLinkAddress> left, Hybrid<TLinkAddress> right) => !(left == right);

        private static bool IsDefault(object value)
        {
            var type = value.GetType();
            if (type.IsValueType)
            {
                return value.Equals(Activator.CreateInstance(type));
            }
            return value == null;
        }

        private static Func<object, TLinkAddress> CompileUnboxAbsNegateAndConvertDelegate()
        {
            return DelegateHelpers.Compile<Func<object, TLinkAddress>>(emiter =>
            {
                Ensure.Always.IsUnsignedInteger<TLinkAddress>();
                emiter.LoadArgument(0);
                var signedVersion = NumericType<TLinkAddress>.SignedVersion;
                var signedVersionField = typeof(NumericType<TLinkAddress>).GetTypeInfo().GetField("SignedVersion", BindingFlags.Static | BindingFlags.Public);
                //emiter.LoadField(signedVersionField);
                emiter.Emit(OpCodes.Ldsfld, signedVersionField);
                var changeTypeMethod = typeof(Convert).GetTypeInfo().GetMethod("ChangeType", Types<object, Type>.Array);
                emiter.Call(changeTypeMethod);
                emiter.UnboxValue(signedVersion);
                var absMethod = typeof(System.Math).GetTypeInfo().GetMethod("Abs", new[] { signedVersion });
                emiter.Call(absMethod);
                var negateMethod = typeof(Platform.Numbers.Math).GetTypeInfo().GetMethod("Negate").MakeGenericMethod(signedVersion);
                emiter.Call(negateMethod);
                var unsignedMethod = typeof(To).GetTypeInfo().GetMethod("Unsigned", new[] { signedVersion });
                emiter.Call(unsignedMethod);
                emiter.Return();
            });
        }

        private static Func<object, TLinkAddress> CompileUnboxAbsAndConvertDelegate()
        {
            return DelegateHelpers.Compile<Func<object, TLinkAddress>>(emiter =>
            {
                Ensure.Always.IsUnsignedInteger<TLinkAddress>();
                emiter.LoadArgument(0);
                var signedVersion = NumericType<TLinkAddress>.SignedVersion;
                var signedVersionField = typeof(NumericType<TLinkAddress>).GetTypeInfo().GetField("SignedVersion", BindingFlags.Static | BindingFlags.Public);
                //emiter.LoadField(signedVersionField);
                emiter.Emit(OpCodes.Ldsfld, signedVersionField);
                var changeTypeMethod = typeof(Convert).GetTypeInfo().GetMethod("ChangeType", Types<object, Type>.Array);
                emiter.Call(changeTypeMethod);
                emiter.UnboxValue(signedVersion);
                var absMethod = typeof(System.Math).GetTypeInfo().GetMethod("Abs", new[] { signedVersion });
                emiter.Call(absMethod);
                var unsignedMethod = typeof(To).GetTypeInfo().GetMethod("Unsigned", new[] { signedVersion });
                emiter.Call(unsignedMethod);
                emiter.Return();
            });
        }

        // TODO: Use directed negation instead provided by the next version of Platform.Numbers.Math.Negate with no conversions required
        private static Func<object, TLinkAddress> CompileNegateAndConvertDelegate()
        {
            return DelegateHelpers.Compile<Func<object, TLinkAddress>>(emiter =>
            {
                Ensure.Always.IsUnsignedInteger<TLinkAddress>();
                emiter.LoadArgument(0);
                var signedVersion = NumericType<TLinkAddress>.SignedVersion;
                var signedMethod = typeof(To).GetTypeInfo().GetMethod("Signed", new[] { typeof(TLinkAddress) });
                emiter.Call(signedMethod);
                var negateMethod = typeof(Platform.Numbers.Math).GetTypeInfo().GetMethod("Negate").MakeGenericMethod(signedVersion);
                emiter.Call(negateMethod);
                var unsignedMethod = typeof(To).GetTypeInfo().GetMethod("Unsigned", new[] { signedVersion });
                emiter.Call(unsignedMethod);
                emiter.Return();
            });
        }
    }
}
