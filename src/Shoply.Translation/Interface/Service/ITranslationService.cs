using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Translation.Argument.Translation;

namespace Shoply.Translation.Interface.Service;

public interface ITranslationService
{
    string Translate(string key, EnumLanguage language, params object[] args);
    void UpdateTranslation(string key, EnumLanguage language, string newTranslation);
    void InsertTranslation(List<OutputTranslation> listTranslation);
}