using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Platform.Delegates;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Data.MultithreadedStorage
{
    /// <summary>
    /// <para>
    /// Represents the type of storage operation.
    /// </para>
    /// <para></para>
    /// </summary>
    public enum StorageOperationType
    {
        Count,
        Each,
        Create,
        Update,
        Delete
    }

    /// <summary>
    /// <para>
    /// Represents a request for a storage operation that can be processed by multiple sections.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public class StorageRequest<TLinkAddress>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        /// <summary>
        /// <para>Gets the unique identifier for this request.</para>
        /// <para></para>
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// <para>Gets the type of storage operation.</para>
        /// <para></para>
        /// </summary>
        public StorageOperationType OperationType { get; }

        /// <summary>
        /// <para>Gets the restriction for the operation.</para>
        /// <para></para>
        /// </summary>
        public IList<TLinkAddress>? Restriction { get; }

        /// <summary>
        /// <para>Gets the substitution for create/update operations.</para>
        /// <para></para>
        /// </summary>
        public IList<TLinkAddress>? Substitution { get; }

        /// <summary>
        /// <para>Gets the read handler for each operations.</para>
        /// <para></para>
        /// </summary>
        public ReadHandler<TLinkAddress>? ReadHandler { get; }

        /// <summary>
        /// <para>Gets the write handler for create/update/delete operations.</para>
        /// <para></para>
        /// </summary>
        public WriteHandler<TLinkAddress>? WriteHandler { get; }

        /// <summary>
        /// <para>Gets the task completion source for returning results.</para>
        /// <para></para>
        /// </summary>
        public TaskCompletionSource<TLinkAddress> CompletionSource { get; }

        /// <summary>
        /// <para>Gets the timestamp when this request was created.</para>
        /// <para></para>
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="StorageRequest{TLinkAddress}"/> class.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="operationType">
        /// <para>The type of storage operation.</para>
        /// <para></para>
        /// </param>
        /// <param name="restriction">
        /// <para>The restriction for the operation.</para>
        /// <para></para>
        /// </param>
        /// <param name="substitution">
        /// <para>The substitution for create/update operations.</para>
        /// <para></para>
        /// </param>
        /// <param name="readHandler">
        /// <para>The read handler for each operations.</para>
        /// <para></para>
        /// </param>
        /// <param name="writeHandler">
        /// <para>The write handler for create/update/delete operations.</para>
        /// <para></para>
        /// </param>
        public StorageRequest(
            StorageOperationType operationType,
            IList<TLinkAddress>? restriction = null,
            IList<TLinkAddress>? substitution = null,
            ReadHandler<TLinkAddress>? readHandler = null,
            WriteHandler<TLinkAddress>? writeHandler = null)
        {
            Id = Guid.NewGuid();
            OperationType = operationType;
            Restriction = restriction;
            Substitution = substitution;
            ReadHandler = readHandler;
            WriteHandler = writeHandler;
            CompletionSource = new TaskCompletionSource<TLinkAddress>();
            CreatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// <para>
    /// Represents the result of processing a storage request by a section.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TLinkAddress">
    /// <para>The type of link address.</para>
    /// <para></para>
    /// </typeparam>
    public class StorageResult<TLinkAddress>
        where TLinkAddress : IUnsignedNumber<TLinkAddress>, IComparable<TLinkAddress>
    {
        /// <summary>
        /// <para>Gets the request ID this result corresponds to.</para>
        /// <para></para>
        /// </summary>
        public Guid RequestId { get; }

        /// <summary>
        /// <para>Gets the section ID that processed this request.</para>
        /// <para></para>
        /// </summary>
        public int SectionId { get; }

        /// <summary>
        /// <para>Gets the result value.</para>
        /// <para></para>
        /// </summary>
        public TLinkAddress Value { get; }

        /// <summary>
        /// <para>Gets any exception that occurred during processing.</para>
        /// <para></para>
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// <para>Gets whether the operation was successful.</para>
        /// <para></para>
        /// </summary>
        public bool IsSuccess => Exception == null;

        /// <summary>
        /// <para>Gets the timestamp when this result was created.</para>
        /// <para></para>
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="StorageResult{TLinkAddress}"/> class.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="requestId">
        /// <para>The request ID this result corresponds to.</para>
        /// <para></para>
        /// </param>
        /// <param name="sectionId">
        /// <para>The section ID that processed this request.</para>
        /// <para></para>
        /// </param>
        /// <param name="value">
        /// <para>The result value.</para>
        /// <para></para>
        /// </param>
        /// <param name="exception">
        /// <para>Any exception that occurred during processing.</para>
        /// <para></para>
        /// </param>
        public StorageResult(Guid requestId, int sectionId, TLinkAddress value, Exception? exception = null)
        {
            RequestId = requestId;
            SectionId = sectionId;
            Value = value;
            Exception = exception;
            CreatedAt = DateTime.UtcNow;
        }
    }
}