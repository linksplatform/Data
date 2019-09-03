using System;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data
{
    public struct LinkAddress<TLinkAddress> : IEquatable<LinkAddress<TLinkAddress>>, IList<TLinkAddress>
    {
        private static readonly EqualityComparer<TLinkAddress> _equalityComparer = EqualityComparer<TLinkAddress>.Default;

        public readonly TLinkAddress Index;

        public LinkAddress(TLinkAddress index) => Index = index;

        public TLinkAddress this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return Index;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set => throw new NotSupportedException();
        }

        public int Count => 1;

        public bool IsReadOnly => true;

        public void Add(TLinkAddress item) => throw new NotSupportedException();

        public void Clear() => throw new NotSupportedException();

        public bool Contains(TLinkAddress item) => _equalityComparer.Equals(item, Index) ? true : false;

        public void CopyTo(TLinkAddress[] array, int arrayIndex) => array[arrayIndex] = Index;

        public IEnumerator<TLinkAddress> GetEnumerator()
        {
            yield return Index;
        }

        public int IndexOf(TLinkAddress item) => _equalityComparer.Equals(item, Index) ? 0 : -1;

        public void Insert(int index, TLinkAddress item) => throw new NotSupportedException();

        public bool Remove(TLinkAddress item) => throw new NotSupportedException();

        public void RemoveAt(int index) => throw new NotSupportedException();

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Index;
        }

        public bool Equals(LinkAddress<TLinkAddress> other) => _equalityComparer.Equals(Index, other.Index);

        public static implicit operator TLinkAddress(LinkAddress<TLinkAddress> linkAddress) => linkAddress.Index;

        public static implicit operator LinkAddress<TLinkAddress>(TLinkAddress linkAddress) => new LinkAddress<TLinkAddress>(linkAddress);

        public override bool Equals(object obj) => obj is LinkAddress<TLinkAddress> linkAddress ? Equals(linkAddress) : false;

        public override int GetHashCode() => Index.GetHashCode();

        public override string ToString() => Index.ToString();
    }
}
