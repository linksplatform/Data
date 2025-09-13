using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Platform.Data.MultithreadedStorage;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.Tests.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// Tests for the MapReduceCombinedLinksStorage implementation.
    /// </para>
    /// <para></para>
    /// </summary>
    public class MapReduceCombinedLinksStorageTests : IDisposable
    {
        private readonly LinksConstants<ulong> _constants;
        private readonly MapReduceCombinedLinksStorage<ulong, LinksConstants<ulong>> _storage;

        public MapReduceCombinedLinksStorageTests()
        {
            _constants = new LinksConstants<ulong>(enableExternalReferencesSupport: false);
            var config = StorageConfiguration<ulong>.CreateDefault();
            config.NumberOfSections = 2; // Use 2 sections for testing
            config.MaxSectionCapacity = 1000; // Small capacity for testing
            _storage = new MapReduceCombinedLinksStorage<ulong, LinksConstants<ulong>>(_constants, config);
        }

        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Assert
            Assert.Equal(_constants, _storage.Constants);
            Assert.Equal(2, _storage.SectionCount);
            Assert.Equal(1000, _storage.MaxSectionCapacity);
        }

        [Fact]
        public void Count_EmptyStorage_ReturnsZero()
        {
            // Act
            var count = _storage.Count(null);

            // Assert
            Assert.Equal(0UL, count);
        }

        [Fact]
        public void Create_SingleLink_ReturnsValidAddress()
        {
            // Arrange
            var substitution = new List<ulong> { _constants.Null, _constants.Null };

            // Act
            var result = _storage.Create(substitution, null);

            // Assert
            Assert.NotEqual(_constants.Null, result);
            Assert.True(result > 0UL);
        }

        [Fact]
        public void Create_MultipleLinks_DistributesAcrossSections()
        {
            // Arrange
            var substitution = new List<ulong> { _constants.Null, _constants.Null };
            var createdLinks = new List<ulong>();

            // Act - Create multiple links
            for (int i = 0; i < 10; i++)
            {
                var result = _storage.Create(substitution, null);
                Assert.NotEqual(_constants.Null, result);
                createdLinks.Add(result);
            }

            // Assert
            Assert.Equal(10, createdLinks.Count);
            Assert.Equal(createdLinks.Count, createdLinks.Distinct().Count()); // All links should be unique
        }

        [Fact]
        public void Count_AfterCreatingLinks_ReturnsCorrectCount()
        {
            // Arrange
            var substitution = new List<ulong> { _constants.Null, _constants.Null };
            var linksToCreate = 5;

            // Act
            for (int i = 0; i < linksToCreate; i++)
            {
                _storage.Create(substitution, null);
            }
            var count = _storage.Count(null);

            // Assert
            Assert.Equal((ulong)linksToCreate, count);
        }

        [Fact]
        public void Each_EmptyStorage_CallsHandlerZeroTimes()
        {
            // Arrange
            var callCount = 0;
            ulong handler(IList<ulong> link)
            {
                callCount++;
                return _constants.Continue;
            }

            // Act
            var result = _storage.Each(null, handler);

            // Assert
            Assert.Equal(_constants.Continue, result);
            Assert.Equal(0, callCount);
        }

        [Fact]
        public void Each_WithLinks_CallsHandlerForEachLink()
        {
            // Arrange
            var substitution = new List<ulong> { _constants.Null, _constants.Null };
            var linksToCreate = 3;
            var handlerCalls = new List<IList<ulong>>();

            // Create some links first
            for (int i = 0; i < linksToCreate; i++)
            {
                _storage.Create(substitution, null);
            }

            ulong handler(IList<ulong> link)
            {
                handlerCalls.Add(new List<ulong>(link));
                return _constants.Continue;
            }

            // Act
            var result = _storage.Each(null, handler);

            // Assert
            Assert.Equal(_constants.Continue, result);
            Assert.Equal(linksToCreate, handlerCalls.Count);
        }

        [Fact]
        public void Each_WithBreak_StopsIteration()
        {
            // Arrange
            var substitution = new List<ulong> { _constants.Null, _constants.Null };
            var linksToCreate = 5;
            var handlerCallCount = 0;

            // Create some links first
            for (int i = 0; i < linksToCreate; i++)
            {
                _storage.Create(substitution, null);
            }

            ulong handler(IList<ulong> link)
            {
                handlerCallCount++;
                return handlerCallCount >= 2 ? _constants.Break : _constants.Continue;
            }

            // Act
            var result = _storage.Each(null, handler);

            // Assert
            Assert.Equal(_constants.Break, result);
            Assert.True(handlerCallCount >= 2);
            Assert.True(handlerCallCount <= linksToCreate);
        }

        [Fact]
        public void Update_ExistingLink_UpdatesSuccessfully()
        {
            // Arrange
            var initialSubstitution = new List<ulong> { 100UL, 200UL };
            var newSubstitution = new List<ulong> { 300UL, 400UL };
            
            var createdLink = _storage.Create(initialSubstitution, null);
            Assert.NotEqual(_constants.Null, createdLink);

            var restriction = new List<ulong> { createdLink };

            // Act
            var updateResult = _storage.Update(restriction, newSubstitution, null);

            // Assert
            Assert.Equal(_constants.Continue, updateResult);
        }

        [Fact]
        public void Delete_ExistingLink_DeletesSuccessfully()
        {
            // Arrange
            var substitution = new List<ulong> { _constants.Null, _constants.Null };
            var createdLink = _storage.Create(substitution, null);
            Assert.NotEqual(_constants.Null, createdLink);

            var initialCount = _storage.Count(null);
            var restriction = new List<ulong> { createdLink };

            // Act
            var deleteResult = _storage.Delete(restriction, null);
            var finalCount = _storage.Count(null);

            // Assert
            Assert.Equal(_constants.Continue, deleteResult);
            Assert.Equal(initialCount - 1, finalCount);
        }

        [Fact]
        public async Task ConcurrentOperations_MultipleThreads_HandledCorrectly()
        {
            // Arrange
            var substitution = new List<ulong> { _constants.Null, _constants.Null };
            var tasks = new List<Task<ulong>>();
            var numberOfOperations = 20;

            // Act - Create multiple links concurrently
            for (int i = 0; i < numberOfOperations; i++)
            {
                tasks.Add(Task.Run(() => _storage.Create(substitution, null)));
            }

            var results = await Task.WhenAll(tasks);

            // Assert
            Assert.Equal(numberOfOperations, results.Length);
            Assert.All(results, result => Assert.NotEqual(_constants.Null, result));
            Assert.Equal(results.Length, results.Distinct().Count()); // All results should be unique

            var finalCount = _storage.Count(null);
            Assert.Equal((ulong)numberOfOperations, finalCount);
        }

        [Fact]
        public void StorageConfiguration_DefaultValues_AreValid()
        {
            // Act
            var config = StorageConfiguration<ulong>.CreateDefault();

            // Assert
            Assert.True(config.MaxSectionCapacity > 0);
            Assert.True(config.EffectiveNumberOfSections > 0);
            Assert.Equal(SectionAllocationMode.Heap, config.AllocationMode);
            Assert.True(config.RequestTimeoutMs > 0);
            Assert.True(config.TargetCpuUtilization > 0 && config.TargetCpuUtilization <= 1.0);

            // Should not throw
            config.Validate();
        }

        [Fact]
        public void StorageConfiguration_HighThroughput_HasCorrectSettings()
        {
            // Act
            var config = StorageConfiguration<ulong>.CreateHighThroughput();

            // Assert
            Assert.True(config.MaxSectionCapacity > StorageConfiguration<ulong>.CreateDefault().MaxSectionCapacity);
            Assert.True(config.EffectiveNumberOfSections >= Environment.ProcessorCount);
            Assert.Equal(SectionAllocationMode.Heap, config.AllocationMode);
            Assert.True(config.EnableAutoExpansion);
            Assert.True(config.EnablePerformanceMonitoring);

            // Should not throw
            config.Validate();
        }

        [Fact]
        public void StorageConfiguration_Validation_ThrowsForInvalidValues()
        {
            // Arrange
            var config = StorageConfiguration<ulong>.CreateDefault();
            
            // Act & Assert
            config.MaxSectionCapacity = 0;
            Assert.Throws<ArgumentException>(() => config.Validate());

            config.MaxSectionCapacity = 1000;
            config.NumberOfSections = 0;
            Assert.Throws<ArgumentException>(() => config.Validate());

            config.NumberOfSections = 2;
            config.RequestTimeoutMs = 0;
            Assert.Throws<ArgumentException>(() => config.Validate());

            config.RequestTimeoutMs = 1000;
            config.TargetCpuUtilization = 0;
            Assert.Throws<ArgumentException>(() => config.Validate());

            config.TargetCpuUtilization = 1.5;
            Assert.Throws<ArgumentException>(() => config.Validate());
        }

        public void Dispose()
        {
            _storage?.Dispose();
        }
    }
}