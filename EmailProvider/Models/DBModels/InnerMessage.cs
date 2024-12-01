using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for inner email details
/// </summary>
public partial class InnerMessage : IEntity
{
    /// <summary>
    /// ID of inner email
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
    /// Receiver user ID
    /// </summary>
    public int ReceiverId { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
