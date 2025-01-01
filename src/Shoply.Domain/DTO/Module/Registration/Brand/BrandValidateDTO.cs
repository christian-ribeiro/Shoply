using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class BrandValidateDTO : BrandPropertyValidateDTO
{
    public InputCreateBrand? InputCreate { get; private set; }
    public InputIdentityUpdateBrand? InputIdentityUpdate { get; private set; }
    public InputIdentityDeleteBrand? InputIdentityDelete { get; private set; }

    public BrandValidateDTO ValidateCreate(InputCreateBrand? inputCreate, List<InputCreateBrand>? listRepeatedInputCreateBrand, BrandDTO originalBrandDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(listRepeatedInputCreateBrand, originalBrandDTO);
        return this;
    }

    public BrandValidateDTO ValidateUpdate(InputIdentityUpdateBrand? inputIdentityUpdate, List<InputIdentityUpdateBrand>? listRepeatedInputIdentityUpdate, BrandDTO originalBrandDTO)
    {
        InputIdentityUpdate = inputIdentityUpdate;
        ValidateUpdate(listRepeatedInputIdentityUpdate, originalBrandDTO);
        return this;
    }

    public BrandValidateDTO ValidateDelete(InputIdentityDeleteBrand? inputIdentityDelete, List<InputIdentityDeleteBrand>? listRepeatedInputIdentityDelete, BrandDTO originalBrandDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalBrandDTO);
        return this;
    }
}