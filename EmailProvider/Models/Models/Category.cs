namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for user-defined categories
/// </summary>
public partial class Category : IEntity
{
    /// <summary>
    /// Id of category
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of category
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Id of creator
    /// </summary>
    public int UserId { get; set; }

    public virtual ICollection<IncomingMessage> IncomingMessages { get; set; } = new List<IncomingMessage>();

    public virtual User User { get; set; } = null!;
}
