using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;
/// <summary>
/// Table for message files
/// </summary>
public partial class File : IEntity
{
    /// <summary>
    /// Id of file
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Content of file
    /// </summary>
    public byte[] Content { get; set; } = null!;

    /// <summary>
    /// Message which contains the file
    /// </summary>
    public int MessageId { get; set; }

    public virtual Message Message { get; set; } = null!;
}
