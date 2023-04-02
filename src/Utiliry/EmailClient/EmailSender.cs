using EmailClient.Config;
using EmailClient.Models.Email;
using EmailClient.Models.Responses;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;

namespace EmailClient
{
    public class EmailSender
    {
        private readonly EmailSenderConfiguration _emailSenderConfiguration;
        public EmailSender(EmailSenderConfiguration emailSenderConfiguration)
        {
            _emailSenderConfiguration = emailSenderConfiguration;
        }

        public async Task<EmailSendResult> Execute(List<EmailSendModel> messages)
        {
            var resultBuilder = new StringBuilder();

            foreach (var m in messages)
            {
                var apiKey = _emailSenderConfiguration.ApiKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_emailSenderConfiguration.From, _emailSenderConfiguration.FromName);

                var subject = m.Subject;
                var to = new EmailAddress(m.To, m.ToName);
                var plainTextContent = m.PlainTextContent;
                var htmlContent = m.HTMLContent;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);
                var msgResponse = await response.Body.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    resultBuilder.AppendLine($"Email Send To: {m.To} : {msgResponse}");
                }
                else
                {
                    return new EmailSendResult()
                    {
                        Success = false,
                        Message = $"Email Send Error: To: {m.To} : {msgResponse} : Succesfully Send to: {resultBuilder.ToString()}"
                    };
                }
            }

            return new EmailSendResult()
            {
                Success = true,
                Message = resultBuilder.ToString()
            };

        }
    }
}