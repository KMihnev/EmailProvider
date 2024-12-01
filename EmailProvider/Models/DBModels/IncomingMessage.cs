using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for incoming email details
/// </summary>
public partial class IncomingMessage : IEntity
{
    /// <summary>
    /// ID of incoming email
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Reference to message ID
    /// </summary>
    public int MessageId { get; set; }

    /// <summary>
    /// Receiver user ID
    /// </summary>
    public int ReceiverId { get; set; }

    /// <summary>
    /// Sender email address
    /// </summary>
    public string SenderEmail { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;
}
