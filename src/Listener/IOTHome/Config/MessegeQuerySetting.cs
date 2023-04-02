namespace IOTHome.Config
{
    public class MessegeQuerySetting
    {
        public MessegeQuerySetting()
        {
            this.HostName =
               string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_HOST_NAME"))
               ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_HOST_NAME must be set!")
               : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_HOST_NAME");

            this.RoutingKey =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_ROUTING_KEY"))
              ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_ROUTING_KEY must be set!")
              : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_ROUTING_KEY");

            this.UserName =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_USER_NAME"))
              ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_USER_NAME must be set!")
              : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_USER_NAME");

            this.Password =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_PASSWORD"))
              ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_PASSWORD must be set!")
              : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_PASSWORD");

            if (!int.TryParse(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_PORT"), out int port))
            {
                throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_PORT must be set!");
            }

            this.Port = port;
        }

        public string? HostName { get; private set; }
        public string? RoutingKey { get; private set; }
        public string? UserName { get; private set; }
        public string? Password { get; private set; }
        public int Port { get; private set; }
    }
}
