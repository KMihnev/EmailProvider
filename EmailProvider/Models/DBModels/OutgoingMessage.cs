using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for outgoing email details
/// </summary>
public partial class OutgoingMessage : IEntity
{
    /// <summary>
    /// ID of outgoing email
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Reference to message ID
    /// </summary>
    public int MessageId { get; set; }

    /// <summary>
    /// Sender user ID
    /// </summary>
    public int SenderId { get; set; }

    /// <summary>
    /// Receiver email address
    /// </summary>
    public string ReceiverEmail { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
