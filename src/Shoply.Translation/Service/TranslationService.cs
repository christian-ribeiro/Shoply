using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Translation.Argument.Translation;
using Shoply.Translation.Interface.Service;
using Shoply.Translation.Persistence.Mongo.Context;
using Shoply.Translation.Persistence.Redis.Context;

namespace Shoply.Translation.Service;

public class TranslationService(IOptions<FeatureFlags> featureFlags, TranslationMongoDBContext mongoContext, TranslationRedisContext redisContext) : ITranslationService
{
    private readonly TranslationMongoDBContext _mongoContext = mongoContext;
    private readonly TranslationRedisContext _redisContext = redisContext;
    private readonly bool useRedis = featureFlags.Value.UseRedis;

    public string Translate(string key, EnumLanguage language, params object[] args)
    {
        var redis = useRedis ? _redisContext.GetDatabase() : default;
        string redisKey = $"translations:{language.GetMemberValue()}:{key}";

        if (useRedis)
        {
            string? translation = redis!.StringGet(redisKey);
            if (!string.IsNullOrEmpty(translation))
                return args.Length > 0 ? string.Format(translation, args) : translation;
        }

        var collection = _mongoContext.GetCollection("translations");
        var filter = Builders<OutputTranslation>.Filter.Eq("Key", key);
        var document = collection.Find(filter).FirstOrDefault();

        if (document != null && document.Translation.ContainsKey(language.GetMemberValue()))
        {
            string template = document.Translation[language.GetMemberValue()];
            if (useRedis)
                redis!.StringSet(redisKey, template, TimeSpan.FromHours(6));
            return args.Length > 0 ? string.Format(template, args) : template;
        }

        return $"[{key}]";
    }

    public void UpdateTranslation(string key, EnumLanguage language, string newTranslation)
    {
        var collection = _mongoContext.GetCollection("translations");

        var filter = Builders<OutputTranslation>.Filter.Eq(t => t.Key, key);

        var update = Builders<OutputTranslation>.Update
            .Set(t => t.Translation[language.GetMemberValue()], newTranslation)
            .Set(t => t.ChangeDate, DateTime.UtcNow);

        collection.UpdateOne(filter, update);

        var redis = _redisContext.GetDatabase();
        string redisKey = $"translations:{language.GetMemberValue()}:{key}";
        redis.StringSet(redisKey, newTranslation, TimeSpan.FromHours(6));
    }

    public void InsertTranslation(List<OutputTranslation> listTranslation)
    {
        var collection = _mongoContext.GetCollection("translations");
        collection.InsertMany(listTranslation);
    }
}