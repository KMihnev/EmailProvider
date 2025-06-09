using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

/// <summary>
/// Table for system users
/// </summary>
public partial class User : IEntity
{
    public User()
    {
        Id = 0;
        Name = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        PhoneNumber = string.Empty;
        CountryId = 0;
        Folders = new List<Folder>();
        UserMessages = new List<UserMessage>();
        Country = null;
    }

    /// <summary>
    /// User ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the user
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Email address of the user
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Password hash or secret
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// User phone number
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Foreign key to country
    /// </summary>
    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Folder> Folders { get; set; }

    public virtual ICollection<UserMessage> UserMessages { get; set; }
}
