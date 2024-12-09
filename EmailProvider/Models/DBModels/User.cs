//Includes

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for users
/// </summary>
public partial class User : IEntity
{
    public User()
    {
        CountryId = 1;
        PhoneNumber = string.Empty;
        Name = string.Empty;
    }

    /// <summary>
    /// ID for users
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of user
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Email of user
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password of user
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Phone number of user
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Country id of user
    /// </summary>
    public int CountryId { get; set; }

    public virtual Country Country { get; set; }

    public virtual ICollection<IncomingMessage> IncomingMessages { get; set; } = new List<IncomingMessage>();

    public virtual ICollection<InnerMessage> InnerMessageReceivers { get; set; } = new List<InnerMessage>();

    public virtual ICollection<InnerMessage> InnerMessageSenders { get; set; } = new List<InnerMessage>();

    public virtual ICollection<OutgoingMessage> OutgoingMessages { get; set; } = new List<OutgoingMessage>();
}
