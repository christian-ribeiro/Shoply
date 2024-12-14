using MongoDB.Bson;

namespace Shoply.Translation.Argument.Translation;

public class OutputTranslation(ObjectId _id, string key, DateTime creationDate, DateTime? changeDate, Dictionary<string, string> translationLanguage)
{
    public ObjectId _id { get; private set; } = _id;
    public string Key { get; private set; } = key;
    public DateTime CreationDate { get; private set; } = creationDate;
    public DateTime? ChangeDate { get; private set; } = changeDate;
    public Dictionary<string, string> TranslationLanguage { get; private set; } = translationLanguage;
}