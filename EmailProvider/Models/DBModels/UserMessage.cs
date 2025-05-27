using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table to store per-user message state
/// </summary>
public partial class UserMessage
{
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Message ID
    /// </summary>
    public int MessageId { get; set; }

    /// <summary>
    /// Folder ID
    /// </summary>
    public int? FolderId { get; set; } = null!;

    /// <summary>
    /// Whether the user has deleted the message
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Whether the message has been read
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Whether the message is pinned
    /// </summary>
    public bool Pinned { get; set; }

    public virtual Folder? Folder { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
