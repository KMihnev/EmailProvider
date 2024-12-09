//Includes

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
}
