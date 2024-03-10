namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for countries
/// </summary>
public partial class Country : IEntity
{
    /// <summary>
    /// Id of country
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of country
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Beginning code of phone number for country
    /// </summary>
    public string PhoneNumberCode { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
