using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Shoply.Translation.Argument.Translation;

namespace Shoply.Translation.Persistence.Mongo.Context;

public class TranslationMongoDBContext
{
    private readonly IMongoDatabase _database;

    public TranslationMongoDBContext(IOptions<TranslateMongoConfiguration> options)
    {
        var settings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);

        settings.ReadPreference = ReadPreference.SecondaryPreferred;
        settings.WriteConcern = WriteConcern.WMajority;
        settings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
        settings.ConnectTimeout = TimeSpan.FromSeconds(10);
        settings.SocketTimeout = TimeSpan.FromSeconds(30);
        settings.MinConnectionPoolSize = 5;
        settings.MaxConnectionPoolSize = 50;

        settings.ClusterConfigurator = builder =>
        {
            builder.Subscribe<CommandStartedEvent>(e =>
            {
                Console.WriteLine($"MongoDB Command Started: {e.CommandName} - {e.Command}");
            });
        };

        var client = new MongoClient(settings);
        _database = client.GetDatabase(options.Value.Database);
    }

    public IMongoCollection<OutputTranslation> GetCollection(string collectionName)
    {
        return _database.GetCollection<OutputTranslation>(collectionName);
    }
}