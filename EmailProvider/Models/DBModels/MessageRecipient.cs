using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for message recipients
/// </summary>
public partial class MessageRecipient : IEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of message
    /// </summary>
    public int MessageId { get; set; }

    /// <summary>
    /// Email address of recipient
    /// </summary>
    public string Email { get; set; } = null!;

    public bool IsOurUser { get; set; }

    public virtual Message Message { get; set; } = null!;
}
