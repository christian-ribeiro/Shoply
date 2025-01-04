using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class BrandService(IBrandRepository repository, ITranslationService translationService, IBrandValidateService brandValidateService) : BaseService<IBrandRepository, IBrandValidateService, InputCreateBrand, InputUpdateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, InputIdentifierBrand, OutputBrand, BrandValidateDTO, BrandDTO, InternalPropertiesBrandDTO, ExternalPropertiesBrandDTO, AuxiliaryPropertiesBrandDTO>(repository, brandValidateService, translationService), IBrandService
{
    #region Create
    public override async Task<BaseResult<List<OutputBrand?>>> Create(List<InputCreateBrand> listInputCreateBrand)
    {
        List<BrandDTO> listOriginalBrandDTO = await _repository.GetListByListIdentifier(listInputCreateBrand.Select(x => new InputIdentifierBrand(x.Code)).ToList());

        var listCreate = (from i in listInputCreateBrand
                          select new
                          {
                              InputCreateBrand = i,
                              ListRepeatedInputCreateBrand = listInputCreateBrand.GetDuplicateItem(i, x => new { x.Code }),
                              OriginalBrandDTO = listOriginalBrandDTO.FirstOrDefault(x => x.ExternalPropertiesDTO.Code == i.Code),
                          }).ToList();

        List<BrandValidateDTO> listBrandValidateDTO = listCreate.Select(x => new BrandValidateDTO().ValidateCreate(x.InputCreateBrand, x.ListRepeatedInputCreateBrand, x.OriginalBrandDTO)).ToList();
        _validate.Create(listBrandValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateBrand.Count)
            return BaseResult<List<OutputBrand?>>.Failure(errors);

        List<BrandDTO> listCreateBrandDTO = (from i in RemoveInvalid(listBrandValidateDTO) select new BrandDTO().Create(i.InputCreate!)).ToList();
        return BaseResult<List<OutputBrand?>>.Success(FromDTOToOutput(await _repository.Create(listCreateBrandDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputBrand?>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        List<BrandDTO> listOriginalBrandDTO = await _repository.GetListByListId(listInputIdentityUpdateBrand.Select(x => x.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateBrand
                          select new
                          {
                              InputIdentityUpdateBrand = i,
                              ListRepeatedInputIdentityUpdateBrand = listInputIdentityUpdateBrand.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalBrandDTO = listOriginalBrandDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<BrandValidateDTO> listBrandValidateDTO = listUpdate.Select(x => new BrandValidateDTO().ValidateUpdate(x.InputIdentityUpdateBrand, x.ListRepeatedInputIdentityUpdateBrand, x.OriginalBrandDTO)).ToList();
        _validate.Update(listBrandValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateBrand.Count)
            return BaseResult<List<OutputBrand?>>.Failure(errors);

        List<BrandDTO> listUpdateBrandDTO = (from i in RemoveInvalid(listBrandValidateDTO) select i.OriginalBrandDTO!.Update(i.InputIdentityUpdate!.InputUpdate!)).ToList();
        return BaseResult<List<OutputBrand?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateBrandDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand)
    {
        List<BrandDTO> listOriginalBrandDTO = await _repository.GetListByListId(listInputIdentityDeleteBrand.Select(x => x.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteBrand
                          select new
                          {
                              InputIdentityDeleteBrand = i,
                              ListRepeatedInputIdentityDeleteBrand = listInputIdentityDeleteBrand.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalBrandDTO = listOriginalBrandDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<BrandValidateDTO> listBrandValidateDTO = listDelete.Select(x => new BrandValidateDTO().ValidateDelete(x.InputIdentityDeleteBrand, x.ListRepeatedInputIdentityDeleteBrand, x.OriginalBrandDTO)).ToList();
        _validate.Delete(listBrandValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteBrand.Count)
            return BaseResult<bool>.Failure(errors);

        List<BrandDTO> listDeletepdateBrandDTO = (from i in RemoveInvalid(listBrandValidateDTO) select i.OriginalBrandDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateBrandDTO), [.. successes, .. errors]);
    }
    #endregion
}