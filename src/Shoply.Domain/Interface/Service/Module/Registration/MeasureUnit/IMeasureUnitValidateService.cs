using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IMeasureUnitValidateService : IBaseValidateService
{
    void Create(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO);
    void Update(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO);
    void Delete(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO);

}