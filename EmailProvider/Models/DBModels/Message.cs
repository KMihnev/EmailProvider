//Includes

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for emails
/// </summary>
public partial class Message : IEntity
{
    /// <summary>
    /// ID of the message
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Subject of the message
    /// </summary>
    public string Subject { get; set; } = null!;

    /// <summary>
    /// Content of the message
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Current status of the email
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Date in which the message was processed
    /// </summary>
    public DateTime DateOfCompletion { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<IncomingMessage> IncomingMessages { get; set; } = new List<IncomingMessage>();

    public virtual ICollection<InnerMessage> InnerMessages { get; set; } = new List<InnerMessage>();

    public virtual ICollection<OutgoingMessage> OutgoingMessages { get; set; } = new List<OutgoingMessage>();
}
