using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using TLinkAddress = System.UInt64;

#pragma warning disable CA1822 // Mark members as static

namespace Platform.Data.Doublets.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class LinkAddressStructBenchmarks
    {
        [GlobalSetup]
        public static void Setup()
        {
        }

        [GlobalCleanup]
        public static void Cleanup() { }

        [Benchmark]
        public IList<TLinkAddress>? Struct()
        {
            return new LinkAddress<TLinkAddress>(1UL);
        }

        [Benchmark]
        public IList<TLinkAddress>? Array()
        {
            return new TLinkAddress[] {1UL};
        }

        [Benchmark]
        public IList<TLinkAddress>? List()
        {
            return new List<TLinkAddress>{1UL};
        }
    }
}
