//Includes
using EmailProvider.Enums;
using EmailProvider.Models.Serializables;
using EmailServiceIntermediate.Enums;

namespace EmailServiceIntermediate.Models.Serializables
{

    //------------------------------------------------------
    //	MessageSerializable
    //------------------------------------------------------

    /// <summary> Модел отговарящ на Модела от базата данни - годен на сериализиране </summary>
    public class EmailViewModel
    {

        public EmailViewModel()
        {
 
            Files = new List<FileViewModel>();
            Status = EmailStatuses.EmailStatusNew;
        }

        public int Id { get; set; }
        public string FromEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;

        public DateTime DateOfRegistration { get; set; }
        public EmailStatuses Status { get; set; }
        public EmailDirections Direction { get; set; }

        public List<MessageRecipientSerializable> Recipients { get; set; } = new();
        public List<FileViewModel> Files { get; set; } = new();

    }
}
