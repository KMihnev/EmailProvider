//Includes

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	BulkOutgoingMessageSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class BulkOutgoingMessageSerializable
    {
        public int Id { get; set; }
        public string RawData { get; set; }
        public int? ScheduledDate { get; set; }
        public int OutgoingMessageId { get; set; }
    }
}
