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
        private static readonly EqualityComparer<TLinkAddress> _equalityComparer = EqualityComparer<TLinkAddress>.Default;
        private static readonly UncheckedSignExtendingConverter<TLinkAddress, long> _addressToInt64Converter = UncheckedSignExtendingConverter<TLinkAddress, long>.Default;
        private static readonly UncheckedConverter<TLinkAddress, ulong> _addressToUInt64Converter = UncheckedConverter<TLinkAddress, ulong>.Default;
        private static readonly UncheckedConverter<ulong, TLinkAddress> _uInt64ToAddressConverter = UncheckedConverter<ulong, TLinkAddress>.Default;
        private static readonly Func<object, TLinkAddress> _unboxAbsAndConvert = CompileUnboxAbsAndConvertDelegate();
        private static readonly Func<object, TLinkAddress> _unboxAbsNegateAndConvert = CompileUnboxAbsNegateAndConvertDelegate();

        public static readonly ulong HalfOfNumberValuesRange = _addressToUInt64Converter.Convert(NumericType<TLinkAddress>.MaxValue) / 2;
        public static readonly TLinkAddress ExternalZero = _uInt64ToAddressConverter.Convert(HalfOfNumberValuesRange + 1UL);

        public readonly TLinkAddress Value;

        public bool IsNothing
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _equalityComparer.Equals(Value, ExternalZero) || SignedValue == 0;
        }

        public bool IsInternal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => SignedValue > 0;
        }

        public bool IsExternal
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _equalityComparer.Equals(Value, ExternalZero) || SignedValue < 0;
        }

        public long SignedValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _addressToInt64Converter.Convert(Value);
        }

        public long AbsoluteValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _equalityComparer.Equals(Value, ExternalZero) ? 0 : Platform.Numbers.Math.Abs(SignedValue);
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
                    Value = Math<TLinkAddress>.Negate(value);
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
        public static explicit operator ulong(Hybrid<TLinkAddress> hybrid) => CheckedConverter<TLinkAddress, ulong>.Default.Convert(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator long(Hybrid<TLinkAddress> hybrid) => hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint(Hybrid<TLinkAddress> hybrid) => CheckedConverter<TLinkAddress, uint>.Default.Convert(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator int(Hybrid<TLinkAddress> hybrid) => (int)hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator ushort(Hybrid<TLinkAddress> hybrid) => CheckedConverter<TLinkAddress, ushort>.Default.Convert(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator short(Hybrid<TLinkAddress> hybrid) => (short)hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator byte(Hybrid<TLinkAddress> hybrid) => CheckedConverter<TLinkAddress, byte>.Default.Convert(hybrid.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator sbyte(Hybrid<TLinkAddress> hybrid) => (sbyte)hybrid.AbsoluteValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => IsExternal ? $"<{AbsoluteValue}>" : Value.ToString();

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsDefault(object value)
        {
            if (value == null)
            {
                return true;
            }
            var type = value.GetType();
            return type.IsValueType ? value.Equals(Activator.CreateInstance(type)) : false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<object, TLinkAddress> CompileUnboxAbsNegateAndConvertDelegate()
        {
            return DelegateHelpers.Compile<Func<object, TLinkAddress>>(emiter =>
            {
                Ensure.Always.IsUnsignedInteger<TLinkAddress>();
                emiter.LoadArgument(0);
                var signedVersion = NumericType<TLinkAddress>.SignedVersion;
                var signedVersionField = typeof(NumericType<TLinkAddress>).GetTypeInfo().GetField("SignedVersion", BindingFlags.Static | BindingFlags.Public);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<object, TLinkAddress> CompileUnboxAbsAndConvertDelegate()
        {
            return DelegateHelpers.Compile<Func<object, TLinkAddress>>(emiter =>
            {
                Ensure.Always.IsUnsignedInteger<TLinkAddress>();
                emiter.LoadArgument(0);
                var signedVersion = NumericType<TLinkAddress>.SignedVersion;
                var signedVersionField = typeof(NumericType<TLinkAddress>).GetTypeInfo().GetField("SignedVersion", BindingFlags.Static | BindingFlags.Public);
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
    }
}
