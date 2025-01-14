using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductImageService(IProductImageRepository repository, ITranslationService translationService, IProductImageValidateService productImageValidateService, IProductRepository productRepository) : BaseService<IProductImageRepository, IProductImageValidateService, InputCreateProductImage, InputIdentifierProductImage, OutputProductImage, InputIdentityDeleteProductImage, ProductImageValidateDTO, ProductImageDTO, InternalPropertiesProductImageDTO, ExternalPropertiesProductImageDTO, AuxiliaryPropertiesProductImageDTO>(repository, productImageValidateService, translationService), IProductImageService
{
    #region Create
    public override async Task<BaseResult<List<OutputProductImage?>>> Create(List<InputCreateProductImage> listInputCreateProductImage)
    {
        List<ProductImageDTO> listOriginalProductImageDTO = await _repository.GetListByListIdentifier(listInputCreateProductImage.Select(x => new InputIdentifierProductImage(x.FileName)).ToList());
        List<ProductDTO> listRelatedProductDTO = await productRepository.GetListByListId(listInputCreateProductImage.Select(x => x.ProductId).ToList());

        var listCreate = (from i in listInputCreateProductImage
                          select new
                          {
                              InputCreateProductImage = i,
                              ListRepeatedInputCreateProductImage = listInputCreateProductImage.GetDuplicateItem(i, x => new { x.FileName }),
                              OriginalProductImageDTO = listOriginalProductImageDTO.FirstOrDefault(x => x.ExternalPropertiesDTO.FileName == i.FileName),
                              RelatedProductDTO = listRelatedProductDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.ProductId),
                          }).ToList();

        List<ProductImageValidateDTO> listProductImageValidateDTO = listCreate.Select(x => new ProductImageValidateDTO().ValidateCreate(x.InputCreateProductImage, x.ListRepeatedInputCreateProductImage, x.OriginalProductImageDTO, x.RelatedProductDTO)).ToList();
        _validate.Create(listProductImageValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateProductImage.Count)
            return BaseResult<List<OutputProductImage?>>.Failure(errors);

        var s3Client = new AmazonS3Client("", "", RegionEndpoint.USEast2);

        var listCreateProductImageDTO = (from i in RemoveInvalid(listProductImageValidateDTO)
                                         let putRequest = new PutObjectRequest
                                         {
                                             BucketName = "development-shoply",
                                             Key = i.InputCreate!.FileName,
                                             InputStream = new MemoryStream(i.InputCreate.File),
                                             ContentType = i.InputCreate!.ContentType
                                         }
                                         select new
                                         {
                                             PutRequest = putRequest,
                                             ProductImageDTO = new ProductImageDTO().Create(new ExternalPropertiesProductImageDTO(i.InputCreate!.FileName, i.InputCreate!.FileLength, i.InputCreate.ProductId), new InternalPropertiesProductImageDTO(""))
                                         }).ToList();

        var tasks = listCreateProductImageDTO.Select(i =>
        {
            return s3Client.PutObjectAsync(i.PutRequest);
        }).ToList();

        await Task.WhenAll(tasks);

        return BaseResult<List<OutputProductImage?>>.Success(FromDTOToOutput(await _repository.Create(listCreateProductImageDTO.Select(x => x.ProductImageDTO).ToList()))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteProductImage> listInputIdentityDeleteProductImage)
    {
        List<ProductImageDTO> listOriginalProductImageDTO = await _repository.GetListByListId(listInputIdentityDeleteProductImage.Select(x => x.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteProductImage
                          select new
                          {
                              InputIdentityDeleteProductImage = i,
                              ListRepeatedInputIdentityDeleteProductImage = listInputIdentityDeleteProductImage.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductImageDTO = listOriginalProductImageDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<ProductImageValidateDTO> listProductImageValidateDTO = listDelete.Select(x => new ProductImageValidateDTO().ValidateDelete(x.InputIdentityDeleteProductImage, x.ListRepeatedInputIdentityDeleteProductImage, x.OriginalProductImageDTO)).ToList();
        _validate.Delete(listProductImageValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProductImage.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductImageDTO> listDeletepdateProductImageDTO = (from i in RemoveInvalid(listProductImageValidateDTO) select i.OriginalProductImageDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductImageDTO), [.. successes, .. errors]);
    }
    #endregion
}