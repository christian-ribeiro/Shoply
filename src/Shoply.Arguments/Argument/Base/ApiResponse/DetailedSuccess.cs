namespace Shoply.Arguments.Argument.Base;

public class DetailedSuccess
{
    public long? Id { get; private set; }
    public string? Identifier { get; private set; }
    public string? Message { get; private set; }

    public DetailedSuccess(long id, string? identifier, string message)
    {
        Id = id;
        Identifier = identifier;
        Message = message;
    }
}