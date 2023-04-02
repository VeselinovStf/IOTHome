# IOT Home Projects

## Current Concept

- Place IOT device on something that you nead to track.
- When a IOT device is moved he sends a package to Helium Network.
- Helium Network sends this package to LoraGatewayService.
- LoraGatewayService saves this package to DB and makes MQ request.
- IOTHome listens for messages from MQ.
- When IOTHome get the message, sends EMail that notifies that device is sending packages.

## Content

- dockerfiles 
    - gateway - dockerfile for gateway
    - listener - dockerfile for listener
- src
    - Gateway
        - LoraGatewayService.API - Gateway service between External Lorawan Network and local/server services
    - Listener
        - IOTHOME - Listenes for MQ messages and does an action
    - Utility
        - AppLogger - Light weight application loger, consists of console and file logger
        - EmailClient - SendGrid Client that sends emails
        - MessageQuery - MQ Library that does the send MQ messages
- docker-compose.win.dev.yaml - dev file for windows environment
- docker-compose.raspi.dev.yaml - dev file for raspi environment
- .env - all projects environment 

## Dev Environment

- Windows
    - docker-compose --env-file .env -f docker-compose.win.dev.yaml up --build
- Raspi
    - docker-compose --env-file .env -f docker-compose.raspi.dev.yaml up --build

## Environment Variables

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

## Test

### Using Postman 

[POST] http://localhost:8090/api/helium 

```
{
    "asd":"asd2"
}
```

### Result 

```
lora-gateway              |       [ INFO ][ 04/02/2023 12:56:20 ] : Message Inserted!
lora-gateway              |       [ INFO ][ 04/02/2023 12:56:20 ] : Message Send To MQ! : 64297b745129d8c2bf77cfb0
iot-home                  |       Email Send To: chofexx@gmail.com :
```