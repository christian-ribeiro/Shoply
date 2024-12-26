using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class BrandService(IBrandRepository repository, ITranslationService translationService) : BaseService<IBrandRepository, InputCreateBrand, InputUpdateBrand, InputIdentifierBrand, OutputBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, BrandValidateDTO, BrandDTO, InternalPropertiesBrandDTO, ExternalPropertiesBrandDTO, AuxiliaryPropertiesBrandDTO, EnumValidateProcessGeneric>(repository, translationService), IBrandService
{
    internal override async Task ValidateProcess(List<BrandValidateDTO> listBrandValidateDTO, EnumValidateProcessGeneric processType)
    {
        switch (processType)
        {
            case EnumValidateProcessGeneric.Create:
                break;
            case EnumValidateProcessGeneric.Update:
                break;
            case EnumValidateProcessGeneric.Delete:
                break;
        }
    }

    #region Create
    public override async Task<BaseResult<List<OutputBrand?>>> Create(List<InputCreateBrand> listInputCreateBrand)
    {
        List<BrandDTO> listOriginalBrandDTO = await _repository.GetListByListIdentifier((from i in listInputCreateBrand select new InputIdentifierBrand(i.Code)).ToList());

        var listCreate = (from i in listInputCreateBrand
                          select new
                          {
                              InputCreateBrand = i,
                              ListRepeatedInputCreateBrand = (from j in listInputCreateBrand where listInputCreateBrand.Count(x => x.Code == i.Code) > 1 select j).ToList(),
                              OriginalBrandDTO = (from j in listOriginalBrandDTO where j.ExternalPropertiesDTO.Code == i.Code select j).FirstOrDefault(),
                          }).ToList();

        List<BrandValidateDTO> listBrandValidateDTO = (from i in listCreate select new BrandValidateDTO().ValidateCreate(i.InputCreateBrand, i.ListRepeatedInputCreateBrand, i.OriginalBrandDTO)).ToList();
        await ValidateProcess(listBrandValidateDTO, EnumValidateProcessGeneric.Create);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateBrand.Count)
            return BaseResult<List<OutputBrand?>>.Failure(errors);

        List<BrandDTO> listCreateBrandDTO = (from i in RemoveInvalid(listBrandValidateDTO) select new BrandDTO().Create(i.InputCreateBrand!)).ToList();
        return BaseResult<List<OutputBrand?>>.Success(FromDTOToOutput(await _repository.Create(listCreateBrandDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputBrand?>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        List<BrandDTO> listOriginalBrandDTO = await _repository.GetListByListId((from i in listInputIdentityUpdateBrand select i.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateBrand
                          select new
                          {
                              InputIdentityUpdateBrand = i,
                              ListRepeatedInputIdentityUpdateBrand = (from j in listInputIdentityUpdateBrand where listInputIdentityUpdateBrand.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalBrandDTO = (from j in listOriginalBrandDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<BrandValidateDTO> listBrandValidateDTO = (from i in listUpdate select new BrandValidateDTO().ValidateUpdate(i.InputIdentityUpdateBrand, i.ListRepeatedInputIdentityUpdateBrand, i.OriginalBrandDTO)).ToList();
        await ValidateProcess(listBrandValidateDTO, EnumValidateProcessGeneric.Update);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateBrand.Count)
            return BaseResult<List<OutputBrand?>>.Failure(errors);

        List<BrandDTO> listUpdateBrandDTO = (from i in RemoveInvalid(listBrandValidateDTO) select i.OriginalBrandDTO!.Update(i.InputIdentityUpdateBrand!.InputUpdate!)).ToList();
        return BaseResult<List<OutputBrand?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateBrandDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand)
    {
        List<BrandDTO> listOriginalBrandDTO = await _repository.GetListByListId((from i in listInputIdentityDeleteBrand select i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteBrand
                          select new
                          {
                              InputIdentityDeleteBrand = i,
                              ListRepeatedInputIdentityDeleteBrand = (from j in listInputIdentityDeleteBrand where listInputIdentityDeleteBrand.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalBrandDTO = (from j in listOriginalBrandDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<BrandValidateDTO> listBrandValidateDTO = (from i in listDelete select new BrandValidateDTO().ValidateDelete(i.InputIdentityDeleteBrand, i.ListRepeatedInputIdentityDeleteBrand, i.OriginalBrandDTO)).ToList();
        await ValidateProcess(listBrandValidateDTO, EnumValidateProcessGeneric.Delete);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteBrand.Count)
            return BaseResult<bool>.Failure(errors);

        List<BrandDTO> listDeletepdateBrandDTO = (from i in RemoveInvalid(listBrandValidateDTO) select i.OriginalBrandDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateBrandDTO), [.. successes, .. errors]);
    }
    #endregion
}