using EmailProvider.Enums;
using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for user-defined folders
/// </summary>
public partial class Folder : IEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// User owning the folder
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Name of the folder
    /// </summary>
    public string Name { get; set; } = null!;

    public EmailDirections FolderDirection { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserMessage> UserMessages { get; set; } = new List<UserMessage>();
}
