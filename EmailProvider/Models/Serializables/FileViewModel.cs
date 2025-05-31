//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	FileSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class FileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Content { get; set; }
    }
}
