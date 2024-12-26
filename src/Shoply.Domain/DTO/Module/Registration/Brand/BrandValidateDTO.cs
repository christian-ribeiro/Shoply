using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class BrandValidateDTO : BrandPropertyValidateDTO
{
    public InputCreateBrand? InputCreateBrand { get; private set; }
    public InputIdentityUpdateBrand? InputIdentityUpdateBrand { get; private set; }
    public InputIdentityDeleteBrand? InputIdentityDeleteBrand { get; private set; }

    public BrandValidateDTO ValidateCreate(InputCreateBrand? inputCreateBrand, List<InputCreateBrand>? listRepeatedInputCreateBrand, BrandDTO originalBrandDTO)
    {
        InputCreateBrand = inputCreateBrand;
        ValidateCreate(listRepeatedInputCreateBrand, originalBrandDTO);
        return this;
    }

    public BrandValidateDTO ValidateUpdate(InputIdentityUpdateBrand? inputIdentityUpdateBrand, List<InputIdentityUpdateBrand>? listRepeatedInputIdentityUpdateBrand, BrandDTO originalBrandDTO)
    {
        InputIdentityUpdateBrand = inputIdentityUpdateBrand;
        ValidateUpdate(listRepeatedInputIdentityUpdateBrand, originalBrandDTO);
        return this;
    }

    public BrandValidateDTO ValidateDelete(InputIdentityDeleteBrand? inputIdentityDeleteBrand, List<InputIdentityDeleteBrand>? listRepeatedInputIdentityDeleteBrand, BrandDTO originalBrandDTO)
    {
        InputIdentityDeleteBrand = inputIdentityDeleteBrand;
        ValidateDelete(listRepeatedInputIdentityDeleteBrand, originalBrandDTO);
        return this;
    }
}