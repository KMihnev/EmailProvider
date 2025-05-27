//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	BulkIncomingMessageSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class BulkIncomingMessageSerializable
    {
        public int Id { get; set; }
        public string RawData { get; set; }
    }
}
