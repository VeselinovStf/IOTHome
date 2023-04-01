using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailClient
{
    public class EmailSender
    {
        public static async Task Execute(string message)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("chofexx@gmail.com", "Lora Integration");
            var subject = "Movement Has Been Detected";
            var to = new EmailAddress("chofexx@gmail.com", "Example User");
            var plainTextContent = "Movement Has Been Detected";
            var htmlContent = $"<strong>Movement Has Been Detected: {message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            var msgResponse = await response.Body.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(msgResponse);
                Console.WriteLine("Email Send");
            }
            else
            {
                Console.WriteLine("Email sending not Succesfull ...");
                Console.WriteLine(msgResponse);
            }
        }
    }
}