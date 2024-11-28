using System;
using System.Collections.Generic;

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
    public int? CountryId { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
}
