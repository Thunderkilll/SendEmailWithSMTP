namespace SimpleEmailApp.Models
{
    public class EmailDto
    {
        public string Sender { get; set; } = string.Empty;
        public string Reciever { get; set; } = string.Empty;    
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
