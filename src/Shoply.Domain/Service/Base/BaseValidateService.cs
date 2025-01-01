using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Base
{
    public abstract class BaseValidateService<TValidateDTO>(ITranslationService translationService) : BaseValidate<TValidateDTO>(translationService), IBaseValidateService
        where TValidateDTO : BaseValidateDTO
    {
        public new void SetGuid(Guid guidSessionDataRequest)
        {
            _guidSessionDataRequest = guidSessionDataRequest;
            base.SetGuid(guidSessionDataRequest);
        }
    }
}