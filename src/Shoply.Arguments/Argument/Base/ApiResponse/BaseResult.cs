using Shoply.Arguments.Enum.Base;

namespace Shoply.Arguments.Argument.Base;

public class BaseResult<T>(bool isSuccess, List<DetailedNotification> listNotification, T? value)
{
    public bool IsSuccess { get; private set; } = isSuccess;
    public List<DetailedNotification> ListNotification { get; private set; } = listNotification;
    public T? Value { get; private set; } = value;

    public static BaseResult<T> Success(T value) => new(true, [], value);
    public static BaseResult<T> Failure() => new(false, [], default);
    public static BaseResult<T> Success(T value, List<DetailedNotification> ListNotification) => new(true, ListNotification, value);
    public static BaseResult<T> Failure(List<DetailedNotification> listNotification) => new(false, listNotification, default);
    public static BaseResult<T> Success(T value, DetailedNotification notification) => new(true, [notification], value);
    public static BaseResult<T> Failure(DetailedNotification notification) => new(false, [notification], default);

}