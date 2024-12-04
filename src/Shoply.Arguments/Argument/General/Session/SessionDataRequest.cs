namespace Shoply.Arguments.Argument.General.Session;

public class SessionDataRequest
{
    public Guid GuidSessionDataRequest { get; }
    public LoggedUser? LoggedUser { get; set; }

    public SessionDataRequest()
    {
        GuidSessionDataRequest = Guid.NewGuid();
    }
}