//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	FileSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class FileSerializable
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public int MessageId { get; set; }
    }
}
