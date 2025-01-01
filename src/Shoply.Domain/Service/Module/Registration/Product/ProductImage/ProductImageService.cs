using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductImageService(IProductImageRepository repository, ITranslationService translationService) : BaseService<IProductImageRepository, InputCreateProductImage, InputIdentifierProductImage, OutputProductImage, InputIdentityDeleteProductImage, ProductImageValidateDTO, ProductImageDTO, InternalPropertiesProductImageDTO, ExternalPropertiesProductImageDTO, AuxiliaryPropertiesProductImageDTO, EnumValidateProcessGeneric>(repository, translationService), IProductImageService
{
    internal override async Task ValidateProcess(List<ProductImageValidateDTO> listProductImageValidateDTO, EnumValidateProcessGeneric processType)
    {
        switch (processType)
        {
            case EnumValidateProcessGeneric.Create:
                break;
            case EnumValidateProcessGeneric.Delete:
                break;
        }
    }

    #region Create
    public override async Task<BaseResult<List<OutputProductImage?>>> Create(List<InputCreateProductImage> listInputCreateProductImage)
    {
        List<ProductImageDTO> listOriginalProductImageDTO = await _repository.GetListByListIdentifier((from i in listInputCreateProductImage select new InputIdentifierProductImage(i.FileName)).ToList());

        var listCreate = (from i in listInputCreateProductImage
                          select new
                          {
                              InputCreateProductImage = i,
                              ListRepeatedInputCreateProductImage = (from j in listInputCreateProductImage where listInputCreateProductImage.Count(x => x.FileName == i.FileName) > 1 select j).ToList(),
                              OriginalProductImageDTO = (from j in listOriginalProductImageDTO where j.ExternalPropertiesDTO.FileName == i.FileName select j).FirstOrDefault(),
                          }).ToList();

        List<ProductImageValidateDTO> listProductImageValidateDTO = (from i in listCreate select new ProductImageValidateDTO().ValidateCreate(i.InputCreateProductImage, i.ListRepeatedInputCreateProductImage, i.OriginalProductImageDTO)).ToList();
        await ValidateProcess(listProductImageValidateDTO, EnumValidateProcessGeneric.Create);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateProductImage.Count)
            return BaseResult<List<OutputProductImage?>>.Failure(errors);

        List<ProductImageDTO> listCreateProductImageDTO = (from i in RemoveInvalid(listProductImageValidateDTO) select new ProductImageDTO().Create(i.InputCreateProductImage!)).ToList();
        return BaseResult<List<OutputProductImage?>>.Success(FromDTOToOutput(await _repository.Create(listCreateProductImageDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteProductImage> listInputIdentityDeleteProductImage)
    {
        List<ProductImageDTO> listOriginalProductImageDTO = await _repository.GetListByListId((from i in listInputIdentityDeleteProductImage select i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteProductImage
                          select new
                          {
                              InputIdentityDeleteProductImage = i,
                              ListRepeatedInputIdentityDeleteProductImage = (from j in listInputIdentityDeleteProductImage where listInputIdentityDeleteProductImage.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalProductImageDTO = (from j in listOriginalProductImageDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<ProductImageValidateDTO> listProductImageValidateDTO = (from i in listDelete select new ProductImageValidateDTO().ValidateDelete(i.InputIdentityDeleteProductImage, i.ListRepeatedInputIdentityDeleteProductImage, i.OriginalProductImageDTO)).ToList();
        await ValidateProcess(listProductImageValidateDTO, EnumValidateProcessGeneric.Delete);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProductImage.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductImageDTO> listDeletepdateProductImageDTO = (from i in RemoveInvalid(listProductImageValidateDTO) select i.OriginalProductImageDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductImageDTO), [.. successes, .. errors]);
    }
    #endregion
}