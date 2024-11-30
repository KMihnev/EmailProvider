using EmailProvider.Enums;
using EmailServiceIntermediate.Enums;
using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for outgoing messages
/// </summary>
public partial class Message : IEntity
{
    public Message()
    {
        Status = EmailStatusProvider.GetNewStatus();
    }

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

    public int Status { get; set; }

    public int Direction { get; set; }

    /// <summary>
    /// Date in which the message was processed
    /// </summary>
    public DateTime DateOfCompletion { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
