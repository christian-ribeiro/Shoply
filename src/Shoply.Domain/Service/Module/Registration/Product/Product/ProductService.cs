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
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListIdentifier(listInputCreateProduct.Select(x => new InputIdentifierProduct(x.Code)).ToList());
        List<ProductCategoryDTO> listRelatedProductCategoryDTO = await productCategoryRepository.GetListByListId(listInputCreateProduct.Where(x => x.ProductCategoryId != null).Select(x => x.ProductCategoryId!.Value).ToList());
        List<MeasureUnitDTO> listRelatedMeasureUnitDTO = await measureUnitRepository.GetListByListId(listInputCreateProduct.Select(x => x.MeasureUnitId).ToList());
        List<BrandDTO> listRelatedBrandDTO = await brandRepository.GetListByListId(listInputCreateProduct.Select(x => x.BrandId).ToList());

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

        List<ProductValidateDTO> listProductValidateDTO = listCreate.Select(x => new ProductValidateDTO().ValidateCreate(x.InputCreateProduct, x.ListRepeatedInputCreateProduct, x.OriginalProductDTO, x.RelatedProductCategoryDTO, x.RelatedMeasureUnitDTO, x.RelatedBrandDTO)).ToList();
        _validate.Create(listProductValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateProduct.Count)
            return BaseResult<List<OutputProduct?>>.Failure(errors);

        List<ProductDTO> listCreateProductDTO = (from i in RemoveInvalid(listProductValidateDTO) select new ProductDTO().Create(i.InputCreate!, new InternalPropertiesProductDTO(0))).ToList();
        return BaseResult<List<OutputProduct?>>.Success(FromDTOToOutput(await _repository.Create(listCreateProductDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputProduct?>>> Update(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
    {
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListId(listInputIdentityUpdateProduct.Select(x => x.Id).ToList());
        List<ProductCategoryDTO> listRelatedProductCategoryDTO = await productCategoryRepository.GetListByListId(listInputIdentityUpdateProduct.Where(x => x.InputUpdate!.ProductCategoryId != null).Select(x => x.InputUpdate!.ProductCategoryId!.Value).ToList());
        List<MeasureUnitDTO> listRelatedMeasureUnitDTO = await measureUnitRepository.GetListByListId(listInputIdentityUpdateProduct.Select(x => x.InputUpdate!.MeasureUnitId).ToList());
        List<BrandDTO> listRelatedBrandDTO = await brandRepository.GetListByListId(listInputIdentityUpdateProduct.Select(x => x.InputUpdate!.BrandId).ToList());

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

        List<ProductValidateDTO> listProductValidateDTO = listUpdate.Select(x => new ProductValidateDTO().ValidateUpdate(x.InputIdentityUpdateProduct, x.ListRepeatedInputIdentityUpdateProduct, x.OriginalProductDTO, x.RelatedProductCategoryDTO, x.RelatedMeasureUnitDTO, x.RelatedBrandDTO)).ToList();
        _validate.Update(listProductValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateProduct.Count)
            return BaseResult<List<OutputProduct?>>.Failure(errors);

        List<ProductDTO> listUpdateProductDTO = (from i in RemoveInvalid(listProductValidateDTO) select i.OriginalProductDTO!.Update(i.InputIdentityUpdate!.InputUpdate!)).ToList();
        return BaseResult<List<OutputProduct?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateProductDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct)
    {
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListId(listInputIdentityDeleteProduct.Select(x => x.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteProduct
                          select new
                          {
                              InputIdentityDeleteProduct = i,
                              ListRepeatedInputIdentityDeleteProduct = listInputIdentityDeleteProduct.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalProductDTO = listOriginalProductDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<ProductValidateDTO> listProductValidateDTO = listDelete.Select(x => new ProductValidateDTO().ValidateDelete(x.InputIdentityDeleteProduct, x.ListRepeatedInputIdentityDeleteProduct, x.OriginalProductDTO)).ToList();
        _validate.Delete(listProductValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProduct.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductDTO> listDeletepdateProductDTO = (from i in RemoveInvalid(listProductValidateDTO) select i.OriginalProductDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductDTO), [.. successes, .. errors]);
    }
    #endregion
}