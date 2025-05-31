using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for message recipients
/// </summary>
public class UserMessageFolder : IEntity
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Time to be sent
    /// </summary>
    public int UserMessageId { get; set; }

    /// <summary>
    /// refrence to message
    /// </summary>
    public int FolderId { get; set; }

    public virtual Folder Folder { get; set; } = null!;

    public virtual UserMessage UserMessage { get; set; } = null!;
}

