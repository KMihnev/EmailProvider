//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	CategorySerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class CategorySerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
