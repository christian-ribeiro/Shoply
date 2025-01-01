using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class BrandPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateBrand>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateBrand>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteBrand>? ListRepeatedInputIdentityDelete { get; private set; }
    public BrandDTO? OriginalBrandDTO { get; private set; }

    public BrandPropertyValidateDTO ValidateCreate(List<InputCreateBrand>? listRepeatedInputCreate, BrandDTO? originalBrandDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreate;
        OriginalBrandDTO = originalBrandDTO;
        return this;
    }

    public BrandPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateBrand>? listRepeatedInputIdentityUpdate, BrandDTO originalBrandDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdate;
        OriginalBrandDTO = originalBrandDTO;
        return this;
    }


    public BrandPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteBrand>? listRepeatedInputIdentityDelete, BrandDTO originalBrandDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalBrandDTO = originalBrandDTO;
        return this;
    }
}