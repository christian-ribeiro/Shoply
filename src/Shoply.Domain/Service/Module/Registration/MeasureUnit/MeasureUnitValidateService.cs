using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class MeasureUnitValidateService(ITranslationService translationService) : BaseValidateService<MeasureUnitValidateDTO>(translationService), IMeasureUnitValidateService
{
    public void Create(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Update(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Delete(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO)
    {
        throw new NotImplementedException();
    }
}