using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table to store per-user message state
/// </summary>
public partial class UserMessage : IEntity
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Message ID
    /// </summary>
    public int MessageId { get; set; }

    /// <summary>
    /// Whether the user has deleted the message
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Whether the message has been read
    /// </summary>
    public bool IsRead { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserMessageFolder> UserMessageFolders { get; set; } = new List<UserMessageFolder>();
}
