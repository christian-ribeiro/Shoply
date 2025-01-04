using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductService(IProductRepository repository, ITranslationService translationService, IProductValidateService productValidateService, IProductCategoryRepository productCategoryRepository, IMeasureUnitRepository measureUnitRepository, IBrandRepository brandRepository) : BaseService<IProductRepository, IProductValidateService, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentifierProduct, OutputProduct, ProductValidateDTO, ProductDTO, InternalPropertiesProductDTO, ExternalPropertiesProductDTO, AuxiliaryPropertiesProductDTO>(repository, productValidateService, translationService), IProductService
{
    #region Create
    public override async Task<BaseResult<List<OutputProduct?>>> Create(List<InputCreateProduct> listInputCreateProduct)
    {
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListIdentifier([.. (from i in listInputCreateProduct select new InputIdentifierProduct(i.Code))]);
        List<ProductCategoryDTO> listRelatedProductCategoryDTO = await productCategoryRepository.GetListByListId([.. (from i in listInputCreateProduct where i.ProductCategoryId != null select i.ProductCategoryId!.Value)]);
        List<MeasureUnitDTO> listRelatedMeasureUnitDTO = await measureUnitRepository.GetListByListId([.. (from i in listInputCreateProduct select i.MeasureUnitId)]);
        List<BrandDTO> listRelatedBrandDTO = await brandRepository.GetListByListId([.. (from i in listInputCreateProduct select i.BrandId)]);

        var listCreate = (from i in listInputCreateProduct
                          select new
                          {
                              InputCreateProduct = i,
                              ListRepeatedInputCreateProduct = listInputCreateProduct.GetDuplicateItem(i, x => new { x.Code }),
                              OriginalProductDTO = listOriginalProductDTO.FirstOrDefault(x => x.ExternalPropertiesDTO.Code == i.Code),
                              RelatedProductCategoryDTO = listRelatedProductCategoryDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.ProductCategoryId),
                              RelatedMeasureUnitDTO = listRelatedMeasureUnitDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.MeasureUnitId),
                              RelatedBrandDTO = listRelatedBrandDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.BrandId)
                          }).ToList();

        List<ProductValidateDTO> listProductValidateDTO = [.. (from i in listCreate select new ProductValidateDTO().ValidateCreate(i.InputCreateProduct, i.ListRepeatedInputCreateProduct, i.OriginalProductDTO, i.RelatedProductCategoryDTO, i.RelatedMeasureUnitDTO, i.RelatedBrandDTO))];
        _validate.Create(listProductValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateProduct.Count)
            return BaseResult<List<OutputProduct?>>.Failure(errors);

        List<ProductDTO> listCreateProductDTO = [.. (from i in RemoveInvalid(listProductValidateDTO) select new ProductDTO().Create(i.InputCreate!, new InternalPropertiesProductDTO(0)))];
        return BaseResult<List<OutputProduct?>>.Success(FromDTOToOutput(await _repository.Create(listCreateProductDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputProduct?>>> Update(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
    {
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListId([.. (from i in listInputIdentityUpdateProduct select i.Id)]);
        List<ProductCategoryDTO> listRelatedProductCategoryDTO = await productCategoryRepository.GetListByListId([.. (from i in listInputIdentityUpdateProduct where i.InputUpdate?.ProductCategoryId != null select i.InputUpdate!.ProductCategoryId!.Value)]);
        List<MeasureUnitDTO> listRelatedMeasureUnitDTO = await measureUnitRepository.GetListByListId([.. (from i in listInputIdentityUpdateProduct select i.InputUpdate!.MeasureUnitId)]);
        List<BrandDTO> listRelatedBrandDTO = await brandRepository.GetListByListId([.. (from i in listInputIdentityUpdateProduct select i.InputUpdate!.BrandId)]);

        var listUpdate = (from i in listInputIdentityUpdateProduct
                          select new
                          {
                              InputIdentityUpdateProduct = i,
                              ListRepeatedInputIdentityUpdateProduct = listInputIdentityUpdateProduct.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductDTO = listOriginalProductDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                              RelatedProductCategoryDTO = listRelatedProductCategoryDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.InputUpdate!.ProductCategoryId),
                              RelatedMeasureUnitDTO = listRelatedMeasureUnitDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.InputUpdate!.MeasureUnitId),
                              RelatedBrandDTO = listRelatedBrandDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.InputUpdate!.BrandId)
                          }).ToList();

        List<ProductValidateDTO> listProductValidateDTO = [.. (from i in listUpdate select new ProductValidateDTO().ValidateUpdate(i.InputIdentityUpdateProduct, i.ListRepeatedInputIdentityUpdateProduct, i.OriginalProductDTO, i.RelatedProductCategoryDTO, i.RelatedMeasureUnitDTO, i.RelatedBrandDTO))];
        _validate.Update(listProductValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateProduct.Count)
            return BaseResult<List<OutputProduct?>>.Failure(errors);

        List<ProductDTO> listUpdateProductDTO = [.. (from i in RemoveInvalid(listProductValidateDTO) select i.OriginalProductDTO!.Update(i.InputIdentityUpdate!.InputUpdate!))];
        return BaseResult<List<OutputProduct?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateProductDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct)
    {
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListId([.. (from i in listInputIdentityDeleteProduct select i.Id)]);

        var listDelete = (from i in listInputIdentityDeleteProduct
                          select new
                          {
                              InputIdentityDeleteProduct = i,
                              ListRepeatedInputIdentityDeleteProduct = listInputIdentityDeleteProduct.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductDTO = listOriginalProductDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<ProductValidateDTO> listProductValidateDTO = [.. (from i in listDelete select new ProductValidateDTO().ValidateDelete(i.InputIdentityDeleteProduct, i.ListRepeatedInputIdentityDeleteProduct, i.OriginalProductDTO))];
        _validate.Delete(listProductValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProduct.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductDTO> listDeletepdateProductDTO = [.. (from i in RemoveInvalid(listProductValidateDTO) select i.OriginalProductDTO)];
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductDTO), [.. successes, .. errors]);
    }
    #endregion
}