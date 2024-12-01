namespace Shoply.Arguments.Argument.General.Session;

public class SessionDataRequest
{
    public Guid GuidSessionDataRequest { get; }

    public SessionDataRequest(Guid guidSessionDataRequest)
    {
        GuidSessionDataRequest = guidSessionDataRequest;
    }
}