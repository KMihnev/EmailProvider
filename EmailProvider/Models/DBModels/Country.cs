using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;
/// <summary>
/// Table for countries list
/// </summary>
public partial class Country : IEntity
{
    /// <summary>
    /// Country ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the country
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Country dialing code prefix
    /// </summary>
    public string PhoneNumberCode { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
