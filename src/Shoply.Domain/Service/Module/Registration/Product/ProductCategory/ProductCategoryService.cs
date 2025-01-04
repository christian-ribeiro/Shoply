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
        List<ProductCategoryDTO> listOriginalProductCategoryDTO = await _repository.GetListByListIdentifier([.. (from i in listInputCreateProductCategory select new InputIdentifierProductCategory(i.Code))]);

        var listCreate = (from i in listInputCreateProductCategory
                          select new
                          {
                              InputCreateProductCategory = i,
                              ListRepeatedInputCreateProductCategory = listInputCreateProductCategory.GetDuplicateItem(i, x => new { x.Code }),
                              OriginalProductCategoryDTO = (from j in listOriginalProductCategoryDTO where j.ExternalPropertiesDTO.Code == i.Code select j).FirstOrDefault(),
                          }).ToList();

        List<ProductCategoryValidateDTO> listProductCategoryValidateDTO = [.. (from i in listCreate select new ProductCategoryValidateDTO().ValidateCreate(i.InputCreateProductCategory, i.ListRepeatedInputCreateProductCategory, i.OriginalProductCategoryDTO))];
        _validate.Create(listProductCategoryValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateProductCategory.Count)
            return BaseResult<List<OutputProductCategory?>>.Failure(errors);

        List<ProductCategoryDTO> listCreateProductCategoryDTO = [.. (from i in RemoveInvalid(listProductCategoryValidateDTO) select new ProductCategoryDTO().Create(i.InputCreate!))];
        return BaseResult<List<OutputProductCategory?>>.Success(FromDTOToOutput(await _repository.Create(listCreateProductCategoryDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputProductCategory?>>> Update(List<InputIdentityUpdateProductCategory> listInputIdentityUpdateProductCategory)
    {
        List<ProductCategoryDTO> listOriginalProductCategoryDTO = await _repository.GetListByListId([.. (from i in listInputIdentityUpdateProductCategory select i.Id)]);

        var listUpdate = (from i in listInputIdentityUpdateProductCategory
                          select new
                          {
                              InputIdentityUpdateProductCategory = i,
                              ListRepeatedInputIdentityUpdateProductCategory = listInputIdentityUpdateProductCategory.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductCategoryDTO = (from j in listOriginalProductCategoryDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<ProductCategoryValidateDTO> listProductCategoryValidateDTO = [.. (from i in listUpdate select new ProductCategoryValidateDTO().ValidateUpdate(i.InputIdentityUpdateProductCategory, i.ListRepeatedInputIdentityUpdateProductCategory, i.OriginalProductCategoryDTO))];
        _validate.Update(listProductCategoryValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateProductCategory.Count)
            return BaseResult<List<OutputProductCategory?>>.Failure(errors);

        List<ProductCategoryDTO> listUpdateProductCategoryDTO = [.. (from i in RemoveInvalid(listProductCategoryValidateDTO) select i.OriginalProductCategoryDTO!.Update(i.InputIdentityUpdate!.InputUpdate!))];
        return BaseResult<List<OutputProductCategory?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateProductCategoryDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteProductCategory> listInputIdentityDeleteProductCategory)
    {
        List<ProductCategoryDTO> listOriginalProductCategoryDTO = await _repository.GetListByListId([.. (from i in listInputIdentityDeleteProductCategory select i.Id)]);

        var listDelete = (from i in listInputIdentityDeleteProductCategory
                          select new
                          {
                              InputIdentityDeleteProductCategory = i,
                              ListRepeatedInputIdentityDeleteProductCategory = listInputIdentityDeleteProductCategory.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductCategoryDTO = (from j in listOriginalProductCategoryDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<ProductCategoryValidateDTO> listProductCategoryValidateDTO = [.. (from i in listDelete select new ProductCategoryValidateDTO().ValidateDelete(i.InputIdentityDeleteProductCategory, i.ListRepeatedInputIdentityDeleteProductCategory, i.OriginalProductCategoryDTO))];
        _validate.Delete(listProductCategoryValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProductCategory.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductCategoryDTO> listDeletepdateProductCategoryDTO = [.. (from i in RemoveInvalid(listProductCategoryValidateDTO) select i.OriginalProductCategoryDTO)];
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductCategoryDTO), [.. successes, .. errors]);
    }
    #endregion
}