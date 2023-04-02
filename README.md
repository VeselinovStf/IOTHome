# IOT Home Projects

# Lorawan Gateway Service

## Lorawan .NET Gateway Service

- Parses request from LoraWan network and save it to database

## Dev Environment

- Windows
    - docker-compose -f docker-compose.win.dev.yaml up
- Raspi
    - docker-compose -f docker-compose.raspi.dev.yaml up

### Environment Variables

```
EMAIL_SENDER_API_KEY=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
EMAIL_SENDER_FROM_EMAIL=chofexx@gmail.com
EMAIL_SENDER_FROM_NAME_EMAIL=Lora Integration
LORA_GATEWAY_MQ_HOST_NAME=lora-rabbitmq
LORA_GATEWAY_MQ_ROUTING_KEY=lora
LORA_GATEWAY_MQ_USER_NAME=guest
LORA_GATEWAY_MQ_PASSWORD=guest
LORA_GATEWAY_MQ_PORT=5672
ASPNETCORE_ENVIRONMENT=Production
LORA_GATEWAY_DB_CONNECTION_STRING=mongodb://root:example@lora-mongo:27017
LORA_GATEWAY_DB_DATABASE_NAME=LoraGateway
LORA_GATEWAY_DEVECE_COLLECTION_NAME=Devices
LOG_FILE_PATH=Logs/log.txt
LORA_INTEGRATION_PORT=8090
```