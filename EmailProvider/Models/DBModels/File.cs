using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for storing attachments/files linked to messages
/// </summary>
public partial class File : IEntity
{
    /// <summary>
    /// File ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// File name including extension
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Binary content of the file
    /// </summary>
    public byte[] Content { get; set; } = null!;

    /// <summary>
    /// Message ID the file is attached to
    /// </summary>
    public int MessageId { get; set; }

    public virtual Message Message { get; set; } = null!;
}
