using System;
using System.Collections.Generic;
using System.Numerics;
using Platform.Ranges;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// Specifies the type of memory allocation for storage sections.
    /// </para>
    /// <para></para>
    /// </summary>
    public enum SectionAllocationMode
    {
        /// <summary>
        /// <para>Allocate sections in heap memory blocks.</para>
        /// <para></para>
        /// </summary>
        Heap,

        /// <summary>
        /// <para>Allocate sections using memory-mapped files (mmap).</para>
        /// <para></para>
        /// </summary>
        MemoryMapped,

        /// <summary>
        /// <para>Allocate sections in separate files.</para>
        /// <para></para>
        /// </summary>
        SeparateFiles
    }

    /// <summary>
    /// <para>
    /// Configuration options for the MapReduce combined links storage.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public class StorageConfiguration<TLinkAddress>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        /// <summary>
        /// <para>Gets or sets the maximum capacity per section. Default is 1MB worth of links.</para>
        /// <para></para>
        /// </summary>
        public int MaxSectionCapacity { get; set; } = 1024 * 1024;

        /// <summary>
        /// <para>Gets or sets the number of sections to create initially. If null, uses CPU core count + 1.</para>
        /// <para></para>
        /// </summary>
        public int? NumberOfSections { get; set; }

        /// <summary>
        /// <para>Gets or sets the allocation mode for storage sections.</para>
        /// <para></para>
        /// </summary>
        public SectionAllocationMode AllocationMode { get; set; } = SectionAllocationMode.Heap;

        /// <summary>
        /// <para>Gets or sets the base directory for file-based allocations.</para>
        /// <para></para>
        /// </summary>
        public string? BaseDirectory { get; set; }

        /// <summary>
        /// <para>Gets or sets the minimum internal references range override.</para>
        /// <para>When set, overrides the default internal references range minimum value.</para>
        /// <para></para>
        /// </summary>
        public TLinkAddress MinInternalReference { get; set; }

        /// <summary>
        /// <para>Gets or sets the maximum internal references range override.</para>
        /// <para>When set, overrides the default internal references range maximum value.</para>
        /// <para></para>
        /// </summary>
        public TLinkAddress MaxInternalReference { get; set; }

        /// <summary>
        /// <para>Gets or sets whether to enable external references support.</para>
        /// <para></para>
        /// </summary>
        public bool EnableExternalReferencesSupport { get; set; } = false;

        /// <summary>
        /// <para>Gets or sets the timeout for request processing in milliseconds.</para>
        /// <para></para>
        /// </summary>
        public int RequestTimeoutMs { get; set; } = 30000; // 30 seconds

        /// <summary>
        /// <para>Gets or sets whether to enable automatic section expansion when capacity is reached.</para>
        /// <para></para>
        /// </summary>
        public bool EnableAutoExpansion { get; set; } = true;

        /// <summary>
        /// <para>Gets or sets the target CPU utilization percentage for determining optimal thread count.</para>
        /// <para></para>
        /// </summary>
        public double TargetCpuUtilization { get; set; } = 0.8; // 80%

        /// <summary>
        /// <para>Gets or sets whether to enable performance monitoring and statistics collection.</para>
        /// <para></para>
        /// </summary>
        public bool EnablePerformanceMonitoring { get; set; } = false;

        /// <summary>
        /// <para>
        /// Gets the effective number of sections to use.
        /// </para>
        /// <para></para>
        /// </summary>
        public int EffectiveNumberOfSections => NumberOfSections ?? Environment.ProcessorCount + 1;

        /// <summary>
        /// <para>
        /// Gets the internal references range based on configuration.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="constants">
        /// <para>The links constants to use as a base.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The configured internal references range.</para>
        /// <para></para>
        /// </returns>
        public Range<TLinkAddress> GetInternalReferencesRange(LinksConstants<TLinkAddress> constants)
        {
            var min = EqualityComparer<TLinkAddress>.Default.Equals(MinInternalReference, default) ? constants.InternalReferencesRange.Minimum : MinInternalReference;
            var max = EqualityComparer<TLinkAddress>.Default.Equals(MaxInternalReference, default) ? constants.InternalReferencesRange.Maximum : MaxInternalReference;
            return new Range<TLinkAddress>(min, max);
        }

        /// <summary>
        /// <para>
        /// Validates the configuration and throws an exception if invalid.
        /// </para>
        /// <para></para>
        /// </summary>
        public void Validate()
        {
            if (MaxSectionCapacity <= 0)
                throw new ArgumentException("MaxSectionCapacity must be positive", nameof(MaxSectionCapacity));

            if (NumberOfSections.HasValue && NumberOfSections.Value <= 0)
                throw new ArgumentException("NumberOfSections must be positive when specified", nameof(NumberOfSections));

            if (RequestTimeoutMs <= 0)
                throw new ArgumentException("RequestTimeoutMs must be positive", nameof(RequestTimeoutMs));

            if (TargetCpuUtilization <= 0 || TargetCpuUtilization > 1.0)
                throw new ArgumentException("TargetCpuUtilization must be between 0 and 1", nameof(TargetCpuUtilization));

            if ((AllocationMode == SectionAllocationMode.MemoryMapped || AllocationMode == SectionAllocationMode.SeparateFiles) 
                && string.IsNullOrEmpty(BaseDirectory))
            {
                throw new ArgumentException("BaseDirectory must be specified for file-based allocation modes", nameof(BaseDirectory));
            }

            if (!EqualityComparer<TLinkAddress>.Default.Equals(MinInternalReference, default) && 
                !EqualityComparer<TLinkAddress>.Default.Equals(MaxInternalReference, default) && 
                MinInternalReference.CompareTo(MaxInternalReference) >= 0)
            {
                throw new ArgumentException("MinInternalReference must be less than MaxInternalReference", nameof(MinInternalReference));
            }
        }

        /// <summary>
        /// <para>
        /// Creates a default configuration instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>A new configuration instance with default values.</para>
        /// <para></para>
        /// </returns>
        public static StorageConfiguration<TLinkAddress> CreateDefault()
        {
            return new StorageConfiguration<TLinkAddress>();
        }

        /// <summary>
        /// <para>
        /// Creates a configuration optimized for high-throughput scenarios.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>A new configuration instance optimized for high throughput.</para>
        /// <para></para>
        /// </returns>
        public static StorageConfiguration<TLinkAddress> CreateHighThroughput()
        {
            return new StorageConfiguration<TLinkAddress>
            {
                MaxSectionCapacity = 16 * 1024 * 1024, // 16MB sections
                NumberOfSections = Environment.ProcessorCount * 2,
                AllocationMode = SectionAllocationMode.Heap,
                EnableAutoExpansion = true,
                TargetCpuUtilization = 0.9,
                EnablePerformanceMonitoring = true
            };
        }

        /// <summary>
        /// <para>
        /// Creates a configuration optimized for memory-efficient scenarios.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="baseDirectory">
        /// <para>The base directory for file storage.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A new configuration instance optimized for memory efficiency.</para>
        /// <para></para>
        /// </returns>
        public static StorageConfiguration<TLinkAddress> CreateMemoryEfficient(string baseDirectory)
        {
            return new StorageConfiguration<TLinkAddress>
            {
                MaxSectionCapacity = 256 * 1024, // 256KB sections
                NumberOfSections = Environment.ProcessorCount,
                AllocationMode = SectionAllocationMode.MemoryMapped,
                BaseDirectory = baseDirectory,
                EnableAutoExpansion = false,
                TargetCpuUtilization = 0.7,
                EnablePerformanceMonitoring = false
            };
        }
    }
}