//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	CountrySerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumberCode { get; set; }
    }
}
