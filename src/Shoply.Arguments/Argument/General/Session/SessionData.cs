using Shoply.Arguments.Argument.General.Mapper;
using System.Collections.Concurrent;

namespace Shoply.Arguments.Argument.General.Session;

public class SessionData
{
    public static ConcurrentDictionary<Guid, SessionDataRequest> ListSessionDataRequest { get; private set; } = new();
    public static CustomMapper? Mapper { get; private set; }

    public static Guid Initialize()
    {
        return Add(new SessionDataRequest());
    }

    public static Guid Add(SessionDataRequest guidSessionDataRequest)
    {
        ListSessionDataRequest.TryAdd(guidSessionDataRequest.GuidSessionDataRequest, guidSessionDataRequest);
        return guidSessionDataRequest.GuidSessionDataRequest;
    }

    public static void SetMapper(CustomMapper mapper)
    {
        Mapper = mapper;
    }
}