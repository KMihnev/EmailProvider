//Includes
using EmailServiceIntermediate.Enums;

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	MessageSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class MessageSerializable
    {

        public MessageSerializable()
        {
            DateOfCompletion = DateTime.Now;
            Files = new List<FileSerializable>();
            Status = EmailStatusProvider.GetNewStatus();
        }
        public int Id { get; set; }
        public int SenderId { get; set; }
        public IEnumerable<string> ReceiverEmails { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }

        public string SenderEmail { get; set; }

        public DateTime DateOfCompletion { get; set; }
        public virtual ICollection<FileSerializable> Files { get; set; }

    }
}
