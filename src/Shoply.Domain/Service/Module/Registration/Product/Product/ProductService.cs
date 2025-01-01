using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductService(IProductRepository repository, ITranslationService translationService, IProductValidateService productValidateService) : BaseService<IProductRepository, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentifierProduct, OutputProduct, ProductValidateDTO, ProductDTO, InternalPropertiesProductDTO, ExternalPropertiesProductDTO, AuxiliaryPropertiesProductDTO>(repository, translationService), IProductService
{
    #region Create
    public override async Task<BaseResult<List<OutputProduct?>>> Create(List<InputCreateProduct> listInputCreateProduct)
    {
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListIdentifier((from i in listInputCreateProduct select new InputIdentifierProduct(i.Code)).ToList());

        var listCreate = (from i in listInputCreateProduct
                          select new
                          {
                              InputCreateProduct = i,
                              ListRepeatedInputCreateProduct = (from j in listInputCreateProduct where listInputCreateProduct.Count(x => x.Code == i.Code) > 1 select j).ToList(),
                              OriginalProductDTO = (from j in listOriginalProductDTO where j.ExternalPropertiesDTO.Code == i.Code select j).FirstOrDefault(),
                          }).ToList();

        List<ProductValidateDTO> listProductValidateDTO = (from i in listCreate select new ProductValidateDTO().ValidateCreate(i.InputCreateProduct, i.ListRepeatedInputCreateProduct, i.OriginalProductDTO)).ToList();
        productValidateService.Create(listProductValidateDTO);

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
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListId((from i in listInputIdentityUpdateProduct select i.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateProduct
                          select new
                          {
                              InputIdentityUpdateProduct = i,
                              ListRepeatedInputIdentityUpdateProduct = (from j in listInputIdentityUpdateProduct where listInputIdentityUpdateProduct.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalProductDTO = (from j in listOriginalProductDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<ProductValidateDTO> listProductValidateDTO = (from i in listUpdate select new ProductValidateDTO().ValidateUpdate(i.InputIdentityUpdateProduct, i.ListRepeatedInputIdentityUpdateProduct, i.OriginalProductDTO)).ToList();
        productValidateService.Update(listProductValidateDTO);

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
        List<ProductDTO> listOriginalProductDTO = await _repository.GetListByListId((from i in listInputIdentityDeleteProduct select i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteProduct
                          select new
                          {
                              InputIdentityDeleteProduct = i,
                              ListRepeatedInputIdentityDeleteProduct = (from j in listInputIdentityDeleteProduct where listInputIdentityDeleteProduct.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalProductDTO = (from j in listOriginalProductDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<ProductValidateDTO> listProductValidateDTO = (from i in listDelete select new ProductValidateDTO().ValidateDelete(i.InputIdentityDeleteProduct, i.ListRepeatedInputIdentityDeleteProduct, i.OriginalProductDTO)).ToList();
        productValidateService.Delete(listProductValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteProduct.Count)
            return BaseResult<bool>.Failure(errors);

        List<ProductDTO> listDeletepdateProductDTO = (from i in RemoveInvalid(listProductValidateDTO) select i.OriginalProductDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateProductDTO), [.. successes, .. errors]);
    }
    #endregion
}