using MessageQuery;

namespace LoraGatewayService.API.Config
{
    public class MessegeQuerySetting : MQSettings
    {
        public MessegeQuerySetting()
        {
            base.HostName =
               string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_HOST_NAME"))
               ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_HOST_NAME must be set!")
               : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_HOST_NAME");

            base.RoutingKey =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_ROUTING_KEY"))
              ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_ROUTING_KEY must be set!")
              : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_ROUTING_KEY");

            base.UserName =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_USER_NAME"))
              ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_USER_NAME must be set!")
              : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_USER_NAME");

            base.Password =
              string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_PASSWORD"))
              ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_PASSWORD must be set!")
              : Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_PASSWORD");

            if (!int.TryParse(Environment.GetEnvironmentVariable("LORA_GATEWAY_MQ_PORT"), out int port))
            {
                throw new ConfigurationException("Environment Variable: LORA_GATEWAY_MQ_PORT must be set!");
            }

            base.Port = port;
        }
    }
}
