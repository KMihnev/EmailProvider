using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for incoming message to be processed
/// </summary>
public partial class BulkIncomingMessage : IEntity
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
    public DateTime? ReceivedDate { get; set; }

    /// <summary>
    /// refrence to message
    /// </summary>
    public int IncomingMessageId { get; set; }

    public virtual Message IncomingMessage { get; set; } = null!;
}
