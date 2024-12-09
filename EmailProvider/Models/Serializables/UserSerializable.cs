//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	UserSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class UserSerializable
    {
        public UserSerializable()
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
    }
}
