using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;

public enum UserRoles
{
    UserRoleDefault,
    UserRoleAdministrator
}

/// <summary>
/// Table for system users
/// </summary>
public partial class UserRole : IEntity
{
    public UserRole()
    {
        Id = 0;
        Name = string.Empty;
    }

    /// <summary>
    /// User ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the user
    /// </summary>
    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
