namespace Shoply.Translation.Argument.Translation;

public class OutputTranslation(string key, DateTime creationDate, DateTime? changeDate, Dictionary<string, string> translationLanguage)
{
    public string Key { get; private set; } = key;
    public DateTime CreationDate { get; private set; } = creationDate;
    public DateTime? ChangeDate { get; private set; } = changeDate;
    public Dictionary<string, string> TranslationLanguage { get; private set; } = translationLanguage;
}