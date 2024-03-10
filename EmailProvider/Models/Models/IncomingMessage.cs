using EmailProvider.Models.Models;
using System;
using System.Collections.Generic;

namespace EmailProviderServer.Models;

/// <summary>
/// Table for incoming messages
/// </summary>
public partial class IncomingMessage : IEntity
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

    /// <summary>
    /// Date in which the message was be received
    /// </summary>
    public DateTime DateOfReceive { get; set; }

    /// <summary>
    /// Category of the message
    /// </summary>
    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual File? File { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
