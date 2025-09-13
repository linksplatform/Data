using System;
using System.Collections.Generic;
using System.Numerics;
using Xunit;
using Platform.Data.Exceptions;
using Platform.Delegates;

namespace Platform.Data.Tests
{
    /// <summary>
    /// Tests for ILinksExtensions methods
    /// </summary>
    public static class ILinksExtensionsTests
    {
        private class MockLinks : ILinks<ulong, LinksConstants<ulong>>
        {
            private readonly LinksConstants<ulong> _constants;

            public MockLinks()
            {
                _constants = new LinksConstants<ulong>(enableExternalReferencesSupport: true);
            }

            public LinksConstants<ulong> Constants => _constants;

            public ulong Count(IList<ulong>? restriction) => 0;

            public ulong Each(IList<ulong>? restriction, ReadHandler<ulong>? handler)
            {
                // Return Constants.Break to simulate no matching links found
                return _constants.Break;
            }

            public ulong Create(IList<ulong>? substitution, WriteHandler<ulong>? handler) => 0;

            public ulong Update(IList<ulong>? restriction, IList<ulong>? substitution, WriteHandler<ulong>? handler) => 0;

            public ulong Delete(IList<ulong>? restriction, WriteHandler<ulong>? handler) => 0;
        }

        /// <summary>
        /// Tests that GetLink throws ArgumentLinkDoesNotExistsException when link doesn't exist
        /// </summary>
        [Fact]
        public static void GetLinkThrowsExceptionWhenLinkDoesNotExist()
        {
            var links = new MockLinks();
            var nonExistentLinkId = 999UL;

            // The mock implementation returns Constants.Break from Each, which means no link was found
            // and the Setter.Result remains null, so GetLink should throw ArgumentLinkDoesNotExistsException
            var exception = Assert.Throws<ArgumentLinkDoesNotExistsException<ulong>>(() => 
                links.GetLink(nonExistentLinkId));

            // Verify the exception contains the correct link id (checking if it's in the message)
            Assert.Contains(nonExistentLinkId.ToString(), exception.Message);
        }

        /// <summary>
        /// Tests that GetLink works correctly for external references
        /// </summary>
        [Fact]
        public static void GetLinkWorksForExternalReferences()
        {
            var links = new MockLinks();
            var externalReference = new Hybrid<ulong>(0, true); // External reference

            // For external references, GetLink should return a Point without calling Each
            var result = links.GetLink(externalReference);

            Assert.NotNull(result);
            // The Point should contain the external reference value
            Assert.Equal(externalReference.Value, result[0]);
        }
    }
}