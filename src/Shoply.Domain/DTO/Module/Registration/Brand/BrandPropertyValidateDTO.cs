using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class BrandPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateBrand>? ListRepeatedInputCreateBrand { get; private set; }
    public List<InputIdentityUpdateBrand>? ListRepeatedInputIdentityUpdateBrand { get; private set; }
    public List<InputIdentityDeleteBrand>? ListRepeatedInputIdentityDeleteBrand { get; private set; }
    public BrandDTO? OriginalBrandDTO { get; private set; }

    public BrandPropertyValidateDTO ValidateCreate(List<InputCreateBrand>? listRepeatedInputCreateBrand, BrandDTO? originalBrandDTO)
    {
        ListRepeatedInputCreateBrand = listRepeatedInputCreateBrand;
        OriginalBrandDTO = originalBrandDTO;
        return this;
    }

    public BrandPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateBrand>? listRepeatedInputIdentityUpdateBrand, BrandDTO originalBrandDTO)
    {
        ListRepeatedInputIdentityUpdateBrand = listRepeatedInputIdentityUpdateBrand;
        OriginalBrandDTO = originalBrandDTO;
        return this;
    }


    public BrandPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteBrand>? listRepeatedInputIdentityDeleteBrand, BrandDTO originalBrandDTO)
    {
        ListRepeatedInputIdentityDeleteBrand = listRepeatedInputIdentityDeleteBrand;
        OriginalBrandDTO = originalBrandDTO;
        return this;
    }
}