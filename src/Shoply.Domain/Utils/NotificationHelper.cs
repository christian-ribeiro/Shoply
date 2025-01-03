using Shoply.Arguments.Argument.Base;
using System.Collections.Concurrent;

namespace Shoply.Domain.Utils;

public static class NotificationHelper
{
    public static ConcurrentDictionary<Guid, ConcurrentDictionary<string, List<DetailedNotification>>> ListNotification { get; private set; } = [];

    public static void Add(Guid guidSessionDataRequest, string key, DetailedNotification detailedNotification)
    {
        ListNotification.TryGetValue(guidSessionDataRequest, out ConcurrentDictionary<string, List<DetailedNotification>>? value);

        value ??= [];

        var existingNotification = value.GetOrAdd(key, _ => [new(key, [], detailedNotification.NotificationType)]);
        var notification = existingNotification.FirstOrDefault();
        if (notification != null)
        {
            notification.ListMessage ??= [];
            notification.ListMessage.AddRange(detailedNotification.ListMessage ?? []);
        }

        ListNotification[guidSessionDataRequest] = value!;
    }

    public static List<DetailedNotification>? Get(Guid guidSessionDataRequest, string key)
    {
        if (ListNotification.TryGetValue(guidSessionDataRequest, out ConcurrentDictionary<string, List<DetailedNotification>>? value))
            if (value != null && value.TryGetValue(key, out List<DetailedNotification>? notification))
                return notification;

        return default;
    }

    public static List<DetailedNotification> Get(Guid guidSessionDataRequest)
    {
        if (ListNotification.TryGetValue(guidSessionDataRequest, out ConcurrentDictionary<string, List<DetailedNotification>>? value))
            return [.. (from i in value ?? [] from j in i.Value ?? [] select j)];

        return [];
    }

    public static void Remove(Guid guidSessionDataRequest)
    {
        ListNotification.TryRemove(guidSessionDataRequest, out _);
    }
}