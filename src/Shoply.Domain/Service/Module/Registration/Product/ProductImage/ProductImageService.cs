using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductImageService(IProductImageRepository repository, ITranslationService translationService, IProductImageValidateService productImageValidateService) : BaseService<IProductImageRepository, IProductImageValidateService, InputCreateProductImage, InputIdentifierProductImage, OutputProductImage, InputIdentityDeleteProductImage, ProductImageValidateDTO, ProductImageDTO, InternalPropertiesProductImageDTO, ExternalPropertiesProductImageDTO, AuxiliaryPropertiesProductImageDTO>(repository, productImageValidateService, translationService), IProductImageService
{
    #region Create
    public override async Task<BaseResult<List<OutputProductImage?>>> Create(List<InputCreateProductImage> listInputCreateProductImage)
    {
        List<ProductImageDTO> listOriginalProductImageDTO = await _repository.GetListByListIdentifier([.. (from i in listInputCreateProductImage select new InputIdentifierProductImage(i.FileName))]);

        var listCreate = (from i in listInputCreateProductImage
                          select new
                          {
                              InputCreateProductImage = i,
                              ListRepeatedInputCreateProductImage = listInputCreateProductImage.GetDuplicateItem(i, x => new { x.FileName }),
                              OriginalProductImageDTO = (from j in listOriginalProductImageDTO where j.ExternalPropertiesDTO.FileName == i.FileName select j).FirstOrDefault(),
                          }).ToList();

        List<ProductImageValidateDTO> listProductImageValidateDTO = [.. (from i in listCreate select new ProductImageValidateDTO().ValidateCreate(i.InputCreateProductImage, i.ListRepeatedInputCreateProductImage, i.OriginalProductImageDTO))];
        _validate.Create(listProductImageValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateProductImage.Count)
            return BaseResult<List<OutputProductImage?>>.Failure(errors);

        List<ProductImageDTO> listCreateProductImageDTO = [.. (from i in RemoveInvalid(listProductImageValidateDTO) select new ProductImageDTO().Create(i.InputCreate!))];
        return BaseResult<List<OutputProductImage?>>.Success(FromDTOToOutput(await _repository.Create(listCreateProductImageDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteProductImage> listInputIdentityDeleteProductImage)
    {
        List<ProductImageDTO> listOriginalProductImageDTO = await _repository.GetListByListId([.. (from i in listInputIdentityDeleteProductImage select i.Id)]);

        var listDelete = (from i in listInputIdentityDeleteProductImage
                          select new
                          {
                              InputIdentityDeleteProductImage = i,
                              ListRepeatedInputIdentityDeleteProductImage = listInputIdentityDeleteProductImage.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductImageDTO = (from j in listOriginalProductImageDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<ProductImageValidateDTO> listProductImageValidateDTO = [.. (from i in listDelete select new ProductImageValidateDTO().ValidateDelete(i.InputIdentityDeleteProductImage, i.ListRepeatedInputIdentityDeleteProductImage, i.OriginalProductImageDTO))];
        _validate.Delete(listProductImageValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProductImage.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductImageDTO> listDeletepdateProductImageDTO = [.. (from i in RemoveInvalid(listProductImageValidateDTO) select i.OriginalProductImageDTO)];
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductImageDTO), [.. successes, .. errors]);
    }
    #endregion
}