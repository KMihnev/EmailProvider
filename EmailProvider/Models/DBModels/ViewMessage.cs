//Includes

namespace EmailProvider.Models.DBModels
{
    /// <summary> Модел отговарящ на процедурата по търсене </summary>
    public class ViewMessage
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmails { get; set; }
        public List<string>? ReceiverEmailsList { get; set; } = new List<string>();
        public DateTime DateOfCompletion { get; set; }

        public int Status { get; set; }
    }
}
