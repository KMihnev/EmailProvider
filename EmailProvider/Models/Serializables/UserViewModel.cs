//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	UserSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class UserViewModel
    {
        public UserViewModel()
        {
            CountryId = 1;
            PhoneNumber = string.Empty;
            Name = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }

        public byte[]? Photo { get; set; }

        public int UserRoleId { get; set; }

        public string? PhotoBase64 => Photo != null ? Convert.ToBase64String(Photo) : null;

        public int PrefferedLanguageId {  get; set; }
    }
}
