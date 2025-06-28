using EmailProvider.Models.DBModels;
using System;
using System.Collections.Generic;

namespace EmailServiceIntermediate.Models;
/// <summary>
/// Table for countries list
/// </summary>
public partial class Country : IEntity
{
    public Country()
    {
        LanguageId = (int)Languages.LanguagesEnglish;
    }

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

    public int LanguageId { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual Language Language { get; set; } = null!;
}
