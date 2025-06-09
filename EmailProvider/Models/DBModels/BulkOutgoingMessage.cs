using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;
/// <summary>
/// Table for incoming message to be processed
/// </summary>
public partial class BulkOutgoingMessage : IEntity
{
    /// <summary>
    /// ID for incoming message to be processed
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Raw data
    /// </summary>
    public string RawData { get; set; } = null!;

    /// <summary>
    /// Time to be sent
    /// </summary>
    public DateTime? SentDate { get; set; }

    /// <summary>
    /// refrence to message
    /// </summary>
    public int OutgoingMessageId { get; set; }

    public virtual Message OutgoingMessage { get; set; } = null!;
}
