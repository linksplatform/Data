using System.Runtime.CompilerServices;
using Platform.Converters;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Numbers.Raw
{
    public class RawNumberToAddressConverter<TLink> : IConverter<TLink>
    {
        static private readonly UncheckedConverter<long, TLink> _converter = UncheckedConverter<long, TLink>.Default;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TLink Convert(TLink source) => _converter.Convert(new Hybrid<TLink>(source).AbsoluteValue);
    }
}
