using System;
using System.Collections.Generic;

namespace EmailProviderServer.Models;

/// <summary>
/// Table for outgoing messages
/// </summary>
public partial class OutgoingMessage
{
    /// <summary>
    /// ID received message
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID for sender
    /// </summary>
    public int SenderId { get; set; }

    /// <summary>
    /// ID for receiver
    /// </summary>
    public int ReceiverId { get; set; }

    /// <summary>
    /// Subject of message
    /// </summary>
    public string Subject { get; set; } = null!;

    /// <summary>
    /// Content of message
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// ID of file in message
    /// </summary>
    public int? FileId { get; set; }

    public string Status { get; set; }

    public bool IsDraft { get; set; }

    /// <summary>
    /// Date in which the message will be send
    /// </summary>
    public DateTime DateOfSend { get; set; }

    public virtual ICollection<BulkOutgoingMessage> BulkOutgoingMessages { get; set; } = new List<BulkOutgoingMessage>();

    public virtual File? File { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
