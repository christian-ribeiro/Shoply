using Microsoft.Extensions.Options;
using Shoply.Translation.Argument.Translation;
using StackExchange.Redis;

namespace Shoply.Translation.Persistence.Redis.Context;

public class TranslationRedisContext
{
    private readonly ConnectionMultiplexer _redis;

    public TranslationRedisContext(IOptions<TranslateRedisConfiguration> options)
    {
        var configOptions = ConfigurationOptions.Parse(options.Value.ConnectionString);
        configOptions.AbortOnConnectFail = false;
        configOptions.ReconnectRetryPolicy = new ExponentialRetry(5000);
        configOptions.ConnectTimeout = 5000;
        configOptions.SyncTimeout = 5000;
        configOptions.KeepAlive = 180;

        _redis = ConnectionMultiplexer.Connect(configOptions);
    }

    public IDatabase GetDatabase()
    {
        return _redis.GetDatabase();
    }

    public async Task ClearCacheAsync(string keyPattern)
    {
        var server = _redis.GetServer(_redis.GetEndPoints().First());
        foreach (var key in server.Keys(pattern: keyPattern))
        {
            await _redis.GetDatabase().KeyDeleteAsync(key);
        }
    }
}