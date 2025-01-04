using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductCategoryService(IProductCategoryRepository repository, ITranslationService translationService, IProductCategoryValidateService productCategoryValidateService) : BaseService<IProductCategoryRepository, IProductCategoryValidateService, InputCreateProductCategory, InputUpdateProductCategory, InputIdentityUpdateProductCategory, InputIdentityDeleteProductCategory, InputIdentifierProductCategory, OutputProductCategory, ProductCategoryValidateDTO, ProductCategoryDTO, InternalPropertiesProductCategoryDTO, ExternalPropertiesProductCategoryDTO, AuxiliaryPropertiesProductCategoryDTO>(repository, productCategoryValidateService, translationService), IProductCategoryService
{
    #region Create
    public override async Task<BaseResult<List<OutputProductCategory?>>> Create(List<InputCreateProductCategory> listInputCreateProductCategory)
    {
        List<ProductCategoryDTO> listOriginalProductCategoryDTO = await _repository.GetListByListIdentifier(listInputCreateProductCategory.Select(x => new InputIdentifierProductCategory(x.Code)).ToList());

        var listCreate = (from i in listInputCreateProductCategory
                          select new
                          {
                              InputCreateProductCategory = i,
                              ListRepeatedInputCreateProductCategory = listInputCreateProductCategory.GetDuplicateItem(i, x => new { x.Code }),
                              OriginalProductCategoryDTO = listOriginalProductCategoryDTO.FirstOrDefault(x => x.ExternalPropertiesDTO.Code == i.Code),
                          }).ToList();

        List<ProductCategoryValidateDTO> listProductCategoryValidateDTO = listCreate.Select(x => new ProductCategoryValidateDTO().ValidateCreate(x.InputCreateProductCategory, x.ListRepeatedInputCreateProductCategory, x.OriginalProductCategoryDTO)).ToList();
        _validate.Create(listProductCategoryValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateProductCategory.Count)
            return BaseResult<List<OutputProductCategory?>>.Failure(errors);

        List<ProductCategoryDTO> listCreateProductCategoryDTO = (from i in RemoveInvalid(listProductCategoryValidateDTO) select new ProductCategoryDTO().Create(i.InputCreate!)).ToList();
        return BaseResult<List<OutputProductCategory?>>.Success(FromDTOToOutput(await _repository.Create(listCreateProductCategoryDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputProductCategory?>>> Update(List<InputIdentityUpdateProductCategory> listInputIdentityUpdateProductCategory)
    {
        List<ProductCategoryDTO> listOriginalProductCategoryDTO = await _repository.GetListByListId(listInputIdentityUpdateProductCategory.Select(x => x.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateProductCategory
                          select new
                          {
                              InputIdentityUpdateProductCategory = i,
                              ListRepeatedInputIdentityUpdateProductCategory = listInputIdentityUpdateProductCategory.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductCategoryDTO = listOriginalProductCategoryDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<ProductCategoryValidateDTO> listProductCategoryValidateDTO = listUpdate.Select(x => new ProductCategoryValidateDTO().ValidateUpdate(x.InputIdentityUpdateProductCategory, x.ListRepeatedInputIdentityUpdateProductCategory, x.OriginalProductCategoryDTO)).ToList();
        _validate.Update(listProductCategoryValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateProductCategory.Count)
            return BaseResult<List<OutputProductCategory?>>.Failure(errors);

        List<ProductCategoryDTO> listUpdateProductCategoryDTO = (from i in RemoveInvalid(listProductCategoryValidateDTO) select i.OriginalProductCategoryDTO!.Update(i.InputIdentityUpdate!.InputUpdate!)).ToList();
        return BaseResult<List<OutputProductCategory?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateProductCategoryDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteProductCategory> listInputIdentityDeleteProductCategory)
    {
        List<ProductCategoryDTO> listOriginalProductCategoryDTO = await _repository.GetListByListId(listInputIdentityDeleteProductCategory.Select(x => x.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteProductCategory
                          select new
                          {
                              InputIdentityDeleteProductCategory = i,
                              ListRepeatedInputIdentityDeleteProductCategory = listInputIdentityDeleteProductCategory.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductCategoryDTO = listOriginalProductCategoryDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<ProductCategoryValidateDTO> listProductCategoryValidateDTO = listDelete.Select(x => new ProductCategoryValidateDTO().ValidateDelete(x.InputIdentityDeleteProductCategory, x.ListRepeatedInputIdentityDeleteProductCategory, x.OriginalProductCategoryDTO)).ToList();
        _validate.Delete(listProductCategoryValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProductCategory.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductCategoryDTO> listDeletepdateProductCategoryDTO = (from i in RemoveInvalid(listProductCategoryValidateDTO) select i.OriginalProductCategoryDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductCategoryDTO), [.. successes, .. errors]);
    }
    #endregion
}