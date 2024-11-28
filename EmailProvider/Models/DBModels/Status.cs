using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for statuses
/// </summary>
public partial class Status : IEntity
{
    /// <summary>
    /// ID for statuses
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Status Value
    /// </summary>
    public string Value { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
