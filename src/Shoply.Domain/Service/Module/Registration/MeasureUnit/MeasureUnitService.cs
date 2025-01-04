using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class MeasureUnitService(IMeasureUnitRepository repository, ITranslationService translationService, IMeasureUnitValidateService measureUnitValidateService) : BaseService<IMeasureUnitRepository, IMeasureUnitValidateService, InputCreateMeasureUnit, InputUpdateMeasureUnit, InputIdentityUpdateMeasureUnit, InputIdentityDeleteMeasureUnit, InputIdentifierMeasureUnit, OutputMeasureUnit, MeasureUnitValidateDTO, MeasureUnitDTO, InternalPropertiesMeasureUnitDTO, ExternalPropertiesMeasureUnitDTO, AuxiliaryPropertiesMeasureUnitDTO>(repository, measureUnitValidateService, translationService), IMeasureUnitService
{
    #region Create
    public override async Task<BaseResult<List<OutputMeasureUnit?>>> Create(List<InputCreateMeasureUnit> listInputCreateMeasureUnit)
    {
        List<MeasureUnitDTO> listOriginalMeasureUnitDTO = await _repository.GetListByListIdentifier(listInputCreateMeasureUnit.Select(x => new InputIdentifierMeasureUnit(x.Code)).ToList());

        var listCreate = (from i in listInputCreateMeasureUnit
                          select new
                          {
                              InputCreateMeasureUnit = i,
                              ListRepeatedInputCreateMeasureUnit = listInputCreateMeasureUnit.GetDuplicateItem(i, x => new { x.Code }),
                              OriginalMeasureUnitDTO = listOriginalMeasureUnitDTO.FirstOrDefault(x => x.ExternalPropertiesDTO.Code == i.Code),
                          }).ToList();

        List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO = listCreate.Select(x => new MeasureUnitValidateDTO().ValidateCreate(x.InputCreateMeasureUnit, x.ListRepeatedInputCreateMeasureUnit, x.OriginalMeasureUnitDTO)).ToList();
        _validate.Create(listMeasureUnitValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateMeasureUnit.Count)
            return BaseResult<List<OutputMeasureUnit?>>.Failure(errors);

        List<MeasureUnitDTO> listCreateMeasureUnitDTO = (from i in RemoveInvalid(listMeasureUnitValidateDTO) select new MeasureUnitDTO().Create(i.InputCreate!)).ToList();
        return BaseResult<List<OutputMeasureUnit?>>.Success(FromDTOToOutput(await _repository.Create(listCreateMeasureUnitDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputMeasureUnit?>>> Update(List<InputIdentityUpdateMeasureUnit> listInputIdentityUpdateMeasureUnit)
    {
        List<MeasureUnitDTO> listOriginalMeasureUnitDTO = await _repository.GetListByListId(listInputIdentityUpdateMeasureUnit.Select(x => x.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateMeasureUnit
                          select new
                          {
                              InputIdentityUpdateMeasureUnit = i,
                              ListRepeatedInputIdentityUpdateMeasureUnit = listInputIdentityUpdateMeasureUnit.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalMeasureUnitDTO = listOriginalMeasureUnitDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO = listUpdate.Select(x => new MeasureUnitValidateDTO().ValidateUpdate(x.InputIdentityUpdateMeasureUnit, x.ListRepeatedInputIdentityUpdateMeasureUnit, x.OriginalMeasureUnitDTO)).ToList();
        _validate.Update(listMeasureUnitValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateMeasureUnit.Count)
            return BaseResult<List<OutputMeasureUnit?>>.Failure(errors);

        List<MeasureUnitDTO> listUpdateMeasureUnitDTO = (from i in RemoveInvalid(listMeasureUnitValidateDTO) select i.OriginalMeasureUnitDTO!.Update(i.InputIdentityUpdate!.InputUpdate!)).ToList();
        return BaseResult<List<OutputMeasureUnit?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateMeasureUnitDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteMeasureUnit> listInputIdentityDeleteMeasureUnit)
    {
        List<MeasureUnitDTO> listOriginalMeasureUnitDTO = await _repository.GetListByListId(listInputIdentityDeleteMeasureUnit.Select(x => x.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteMeasureUnit
                          select new
                          {
                              InputIdentityDeleteMeasureUnit = i,
                              ListRepeatedInputIdentityDeleteMeasureUnit = listInputIdentityDeleteMeasureUnit.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalMeasureUnitDTO = listOriginalMeasureUnitDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO = listDelete.Select(x => new MeasureUnitValidateDTO().ValidateDelete(x.InputIdentityDeleteMeasureUnit, x.ListRepeatedInputIdentityDeleteMeasureUnit, x.OriginalMeasureUnitDTO)).ToList();
        _validate.Delete(listMeasureUnitValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteMeasureUnit.Count)
            return BaseResult<bool>.Failure(errors);

        List<MeasureUnitDTO> listDeletepdateMeasureUnitDTO = (from i in RemoveInvalid(listMeasureUnitValidateDTO) select i.OriginalMeasureUnitDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateMeasureUnitDTO), [.. successes, .. errors]);
    }
    #endregion
}