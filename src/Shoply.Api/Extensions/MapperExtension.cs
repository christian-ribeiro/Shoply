using AutoMapper;
using Shoply.Arguments.Argument.General.Mapper;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.Mapper;
using Shoply.Infrastructure.Mapper;

namespace Shoply.Api.Extensions;

public static class MapperExtension
{
    public static IServiceCollection ConfigureMapper(this IServiceCollection services)
    {
        SessionData.SetMapper(new CustomMapper(
            new MapperConfiguration(config => { config.AddProfile(new MapperDTOOutput()); }).CreateMapper(),
            new MapperConfiguration(config => { config.AddProfile(new MapperEntityDTO()); }).CreateMapper()));

        return services;
    }
}