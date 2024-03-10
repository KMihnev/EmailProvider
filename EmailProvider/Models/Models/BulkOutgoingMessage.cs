using EmailProvider.Models.Models;
using System;
using System.Collections.Generic;

namespace EmailProviderServer.Models;

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
    public int? ScheduledDate { get; set; }

    /// <summary>
    /// refrence to message
    /// </summary>
    public int OutgoingMessageId { get; set; }

    public virtual OutgoingMessage OutgoingMessage { get; set; } = null!;
}
