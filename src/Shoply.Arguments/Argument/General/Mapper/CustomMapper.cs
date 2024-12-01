using AutoMapper;

namespace Shoply.Arguments.Argument.General.Mapper;

public class CustomMapper(IMapper mapperDTOOutput, IMapper MapperEntityDTO)
{
    public IMapper MapperDTOOutput { get; private set; } = mapperDTOOutput;
    public IMapper MapperEntityDTO { get; private set; } = MapperEntityDTO;
}