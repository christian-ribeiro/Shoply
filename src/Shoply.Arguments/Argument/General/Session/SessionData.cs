using AutoMapper;
using Shoply.Arguments.Argument.General.Mapper;
using System.Collections.Concurrent;

namespace Shoply.Arguments.Argument.General.Session;

public class SessionData
{
    public static ConcurrentDictionary<Guid, SessionDataRequest> ListSessionDataRequest { get; private set; } = new();
    public static CustomMapper? Mapper { get; private set; }

    public static void SetMapper(CustomMapper mapper)
    {
        Mapper = mapper;
    }
}