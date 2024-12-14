using MongoDB.Driver;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Translation.Argument.Translation;
using Shoply.Translation.Interface.Service;
using Shoply.Translation.Persistence.Mongo.Context;
using Shoply.Translation.Persistence.Redis.Context;

namespace Shoply.Translation.Service;

public class TranslationService(TranslationMongoDBContext mongoContext, TranslationRedisContext redisContext) : ITranslationService
{
    private readonly TranslationMongoDBContext _mongoContext = mongoContext;
    private readonly TranslationRedisContext _redisContext = redisContext;

    public async Task<string> TranslateAsync(string key, EnumLanguage language, params object[] args)
    {
        var redis = _redisContext.GetDatabase();
        string redisKey = $"translations:{key}:{language.GetMemberValue()}";

        string? translation = await redis.StringGetAsync(redisKey);
        if (!string.IsNullOrEmpty(translation))
            return args.Length > 0 ? string.Format(translation, args) : translation;

        var collection = _mongoContext.GetCollection("translations");
        var filter = Builders<OutputTranslation>.Filter.Eq("Key", key);
        var document = await collection.Find(filter).FirstOrDefaultAsync();

        if (document != null && document.TranslationLanguage.ContainsKey(language.GetMemberValue()))
        {
            string template = document.TranslationLanguage[language.GetMemberValue()];
            await redis.StringSetAsync(redisKey, template, TimeSpan.FromHours(6));
            return args.Length > 0 ? string.Format(template, args) : template;
        }

        return $"[{key}]";
    }

    public async Task UpdateTranslationAsync(string key, EnumLanguage language, string newTranslation)
    {
        var collection = _mongoContext.GetCollection("translations");

        var filter = Builders<OutputTranslation>.Filter.Eq(t => t.Key, key);

        var update = Builders<OutputTranslation>.Update
            .Set(t => t.TranslationLanguage[language.GetMemberValue()], newTranslation)
            .Set(t => t.ChangeDate, DateTime.UtcNow);

        await collection.UpdateOneAsync(filter, update);

        var redis = _redisContext.GetDatabase();
        string redisKey = $"translations:{key}:{language.GetMemberValue()}";
        await redis.StringSetAsync(redisKey, newTranslation, TimeSpan.FromHours(6));
    }
}