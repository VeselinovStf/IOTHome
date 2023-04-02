namespace LoraGatewayService.API.Config
{
    public class DBSetting
    {
        public DBSetting()
        {
            this.LORA_GATEWAY_DB_CONNECTION_STRING =
                string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_DB_CONNECTION_STRING"))
                ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_DB_CONNECTION_STRING must be set!")
                : Environment.GetEnvironmentVariable("LORA_GATEWAY_DB_CONNECTION_STRING");

            this.LORA_GATEWAY_DB_DATABASE_NAME =
                string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_DB_DATABASE_NAME"))
                ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_DB_DATABASE_NAME must be set!")
                : Environment.GetEnvironmentVariable("LORA_GATEWAY_DB_DATABASE_NAME");

            this.LORA_GATEWAY_DEVECE_COLLECTION_NAME =
                string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LORA_GATEWAY_DEVECE_COLLECTION_NAME"))
                ? throw new ConfigurationException("Environment Variable: LORA_GATEWAY_DEVECE_COLLECTION_NAME must be set!")
                : Environment.GetEnvironmentVariable("LORA_GATEWAY_DEVECE_COLLECTION_NAME");
        }

        public string LORA_GATEWAY_DB_CONNECTION_STRING { get; private set; }
        public string LORA_GATEWAY_DB_DATABASE_NAME { get; private set; }
        public string LORA_GATEWAY_DEVECE_COLLECTION_NAME { get; private set; }
    }
}
