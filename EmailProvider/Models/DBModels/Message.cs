using EmailProvider.Enums;
using EmailServiceIntermediate.Enums;
using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for storing message metadata and content
/// </summary>
public partial class Message : IEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Sender email address
    /// </summary>
    public string FromEmail { get; set; } = null!;

    /// <summary>
    /// Subject of the message
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Body/content of the message
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Date/time the message was sent
    /// </summary>
    public DateTime? DateOfRegistration { get; set; }

    /// <summary>
    /// Message direction (incoming, outgoing, internal)
    /// </summary>
    public EmailDirections Direction { get; set; }

    /// <summary>
    /// Message status (draft, sent, etc.)
    /// </summary>
    public EmailStatuses Status { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<MessageRecipient> MessageRecipients { get; set; } = new List<MessageRecipient>();

    public virtual ICollection<UserMessage> UserMessages { get; set; } = new List<UserMessage>();
}
