using System.ComponentModel.DataAnnotations.Schema;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for users
/// </summary>
public partial class User : IEntity
{
    public User()
    {
        Name = "";
        CountryId = 1;
        PhoneNumber = "";
    }

    /// <summary>
    /// ID for users
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of user
    /// </summary>
    public string Name { get; set; }

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
    [Column("PHONE_NUMBER")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Country id of user
    /// </summary>
    [Column("COUNTRY_ID")]
    public int CountryId { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual Country Country { get; set; }

    public virtual ICollection<IncomingMessage> IncomingMessageReceivers { get; set; } = new List<IncomingMessage>();

    public virtual ICollection<IncomingMessage> IncomingMessageSenders { get; set; } = new List<IncomingMessage>();

    public virtual ICollection<OutgoingMessage> OutgoingMessageReceivers { get; set; } = new List<OutgoingMessage>();

    public virtual ICollection<OutgoingMessage> OutgoingMessageSenders { get; set; } = new List<OutgoingMessage>();
}
