using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Translation.Argument.Translation;

namespace Shoply.Translation.Interface.Service;

public interface  ITranslationService
{
    Task<string> TranslateAsync(string key, EnumLanguage language, params object[] args);
    Task UpdateTranslationAsync(string key, EnumLanguage language, string newTranslation);
    Task InsertTranslationAsync(List<OutputTranslation> listTranslation);
}