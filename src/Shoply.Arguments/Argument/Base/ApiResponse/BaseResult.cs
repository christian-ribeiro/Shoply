using Shoply.Arguments.Enum.Base;

namespace Shoply.Arguments.Argument.Base;

public class BaseResult<T>(bool isSuccess, List<DetailedNotification> listNotification, T? value)
{
    public bool IsSuccess { get; private set; } = isSuccess;
    public List<DetailedNotification> ListNotification { get; private set; } = listNotification;
    public T? Value { get; private set; } = value;

    public static BaseResult<T> Success(T value) => new(true, [], value);
    public static BaseResult<T> Failure() => new(false, [], default);
}

public static class BaseResultExtension
{
    public static BaseResult<T> AddSuccess<T>(this BaseResult<T> baseResult, DetailedNotification notification)
    {
        baseResult.ListNotification.Add(notification.SetSuccess());
        return baseResult;
    }

    public static BaseResult<T> AddSuccess<T>(this BaseResult<T> baseResult, List<DetailedNotification> listNotification)
    {
        baseResult.ListNotification.AddRange(listNotification.SetSuccess());
        return baseResult;
    }

    public static BaseResult<T> AddError<T>(this BaseResult<T> baseResult, DetailedNotification notification)
    {
        baseResult.ListNotification.Add(notification.SetError());
        return baseResult;
    }

    public static BaseResult<T> AddError<T>(this BaseResult<T> baseResult, List<DetailedNotification> listNotification)
    {
        baseResult.ListNotification.AddRange(listNotification.SetError());
        return baseResult;
    }

    public static BaseResult<T> AddAlert<T>(this BaseResult<T> baseResult, DetailedNotification notification)
    {
        baseResult.ListNotification.Add(notification.SetAlert());
        return baseResult;
    }

    public static BaseResult<T> AddAlert<T>(this BaseResult<T> baseResult, List<DetailedNotification> listNotification)
    {
        baseResult.ListNotification.AddRange(listNotification.SetAlert());
        return baseResult;
    }

    public static DetailedNotification SetSuccess(this DetailedNotification detailedNotification)
    {
        _ = detailedNotification.SetNotificationType(EnumNotificationType.Success);
        return detailedNotification;
    }

    public static List<DetailedNotification> SetSuccess(this List<DetailedNotification> listDetailedNotification)
    {
        _ = (from i in listDetailedNotification select i.SetNotificationType(EnumNotificationType.Success)).ToList();
        return listDetailedNotification;
    }

    public static DetailedNotification SetError(this DetailedNotification detailedNotification)
    {
        _ = detailedNotification.SetNotificationType(EnumNotificationType.Error);
        return detailedNotification;
    }

    public static List<DetailedNotification> SetError(this List<DetailedNotification> listDetailedNotification)
    {
        _ = (from i in listDetailedNotification select i.SetNotificationType(EnumNotificationType.Error)).ToList();
        return listDetailedNotification;
    }

    public static DetailedNotification SetAlert(this DetailedNotification detailedNotification)
    {
        _ = detailedNotification.SetNotificationType(EnumNotificationType.Alert);
        return detailedNotification;
    }

    public static List<DetailedNotification> SetAlert(this List<DetailedNotification> listDetailedNotification)
    {
        _ = (from i in listDetailedNotification select i.SetNotificationType(EnumNotificationType.Alert)).ToList();
        return listDetailedNotification;
    }
}