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
    public class EmailServiceModel
    {

        public EmailServiceModel()
        {
 
            Files = new List<FileViewModel>();
        }

        public int Id { get; set; }
        public string FromEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime DateOfRegistration { get; set; }

        public string Recipient { get; set; }
        public List<FileViewModel> Files { get; set; } = new();

    }
}
