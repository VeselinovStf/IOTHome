namespace EmailClient.Models.Email
{
    public class EmailSendModel
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string PlainTextContent { get; set; }

        public string HTMLContent { get; set; }
    }
}
