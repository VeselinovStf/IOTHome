using AppLogger;
using LoraGatewayService.API.Config;
using MessageQuery;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>(r => new RabbitMQProducer(new MessegeQuerySetting()));
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(FileLogger<>));
builder.Services.AddSingleton<DBSetting>();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapPost("api/helium", async (
    HttpRequest request,
    IAppLogger<StartUp> _logger,
    IMessageProducer messageProducer,
    DBSetting databaseSetting) =>
{
    try
    {
        // Validate Request
        if (request == null || request.Body == null)
        {
            _logger.LogError($"Invalid Request!");

            return Results.BadRequest();
        }

        // Database conection
        var mongoClient = new MongoClient(
            databaseSetting.LORA_GATEWAY_DB_CONNECTION_STRING);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSetting.LORA_GATEWAY_DB_DATABASE_NAME);

        var deviceCollection = mongoDatabase.GetCollection<BsonDocument>(
            databaseSetting.LORA_GATEWAY_DEVECE_COLLECTION_NAME);

        // Parse Request
        string parsedRequestBody;
        using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
        {
            parsedRequestBody = await reader.ReadToEndAsync();
        }

        if (!string.IsNullOrWhiteSpace(parsedRequestBody))
        {
            // If Request message is valid insert in database
            BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(parsedRequestBody);
            await deviceCollection.InsertOneAsync(document);
            var insertedId = document
                .GetElement("_id")
                .Value
                .ToString();

            _logger.LogInformation($"Message Inserted!");

            // Send Message To MQ
            try
            {
                messageProducer.SendMessage(new
                {
                    Id = insertedId
                });

                _logger.LogInformation($"Message Send To MQ! : {insertedId}");
            }
            catch (Exception ex)
            {
                // Can't add to MQ
                _logger.LogError($"Can't add to Message Query! : {ex.Message} : {ex.StackTrace}");

                return Results.BadRequest();
            }
        }
        else
        {
            // If Message is invalid
            _logger.LogError($"Invalid Request Body Content/Can't Parse request body!");

            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        // General Exception
        _logger.LogError($"Can't Read Message! : {ex.Message} : {ex.StackTrace}");

        return Results.BadRequest();
    }

    // All done OK
    return Results.Ok();
});

app.Run();

// Added for unit Testing
public partial class StartUp { }

