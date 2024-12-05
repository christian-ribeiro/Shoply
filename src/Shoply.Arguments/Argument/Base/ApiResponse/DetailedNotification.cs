using Shoply.Arguments.Enum.Base;

namespace Shoply.Arguments.Argument.Base;

public class DetailedNotification
{
    public long? Key { get; private set; }
    public string? Identifier { get; private set; }
    public List<string>? ListMessage { get; private set; }
    public EnumNotificationType NotificationType { get; private set; }

    public DetailedNotification(long? key, string? identifier, List<string>? listMessage)
    {
        Key = key;
        Identifier = identifier;
        ListMessage = listMessage;
    }

    public DetailedNotification(string? identifier, List<string>? listMessage)
    {
        Identifier = identifier;
        ListMessage = listMessage;
    }

    public DetailedNotification(long? key, string? identifier, string? message)
    {
        Key = key;
        Identifier = identifier;
        ListMessage = [message];
    }

    public DetailedNotification(string? identifier, string message)
    {
        Identifier = identifier;
        ListMessage = [message];
    }

    public DetailedNotification(string message)
    {
        ListMessage = [message];
    }


    public DetailedNotification SetNotificationType(EnumNotificationType notificationType)
    {
        NotificationType = notificationType;
        return this;
    }
}