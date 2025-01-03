﻿namespace Shoply.Arguments.Argument.General.Session;

public class SessionDataRequest
{
    public Guid GuidSessionDataRequest { get; }
    public LoggedUser? LoggedUser { get; set; }
    public List<string>? ReturnProperty { get; set; }

    public SessionDataRequest()
    {
        GuidSessionDataRequest = Guid.NewGuid();
    }
}