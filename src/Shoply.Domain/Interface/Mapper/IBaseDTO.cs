namespace Shoply.Domain.Interface.Mapper;

public interface IBaseDTO<TDTO, TOutput>
{
    TDTO GetDTO(TOutput output);
    TOutput GetOutput(TDTO dto);
}