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

    public static void RemoveSessionDataRequest(Guid guidSessionDataRequest)
    {
        ListSessionDataRequest.TryRemove(guidSessionDataRequest, out _);
    }

    public static void SetMapper(CustomMapper mapper)
    {
        Mapper = mapper;
    }

    public static void SetLoggedUser(Guid guidSessionDataRequest, LoggedUser loggedUser)
    {
        ListSessionDataRequest[guidSessionDataRequest].LoggedUser = loggedUser;
    }

    public static void SetReturnProperty(Guid guidSessionDataRequest, List<string>? returnProperty)
    {
        ListSessionDataRequest[guidSessionDataRequest].ReturnProperty = returnProperty;
    }

    public static LoggedUser? GetLoggedUser(Guid guidSessionDataRequest)
    {
        ListSessionDataRequest.TryGetValue(guidSessionDataRequest, out SessionDataRequest? value);
        return value?.LoggedUser;
    }

    public static List<string>? GetReturnProperty(Guid guidSessionDataRequest)
    {
        ListSessionDataRequest.TryGetValue(guidSessionDataRequest, out SessionDataRequest? value);
        return value?.ReturnProperty;
    }
}