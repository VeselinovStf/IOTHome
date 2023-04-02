using EmailClient.Config;

namespace IOTHome.Config
{
    public class EmailSenderConfig : EmailSenderConfiguration
    {
        public EmailSenderConfig()
        {
            base.ApiKey =
             string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("EMAIL_SENDER_API_KEY"))
             ? throw new ConfigurationException("Environment Variable: EMAIL_SENDER_API_KEY must be set!")
             : Environment.GetEnvironmentVariable("EMAIL_SENDER_API_KEY");

            base.From =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("EMAIL_SENDER_FROM_EMAIL"))
              ? throw new ConfigurationException("Environment Variable: EMAIL_SENDER_FROM_EMAIL must be set!")
              : Environment.GetEnvironmentVariable("EMAIL_SENDER_FROM_EMAIL");

            base.FromName =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("EMAIL_SENDER_FROM_NAME_EMAIL"))
              ? throw new ConfigurationException("Environment Variable: EMAIL_SENDER_FROM_NAME_EMAIL must be set!")
              : Environment.GetEnvironmentVariable("EMAIL_SENDER_FROM_NAME_EMAIL");
        }
    }
}
