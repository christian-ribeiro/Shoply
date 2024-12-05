namespace Shoply.Arguments.Argument.Base;

public class BaseResult<T>(bool isSuccess, List<DetailedError> listDetailedError, List<DetailedSuccess> listDetailedSuccess, T? value)
{
    public bool IsSuccess { get; private set; } = isSuccess;
    public List<DetailedError> ListDetailedError { get; private set; } = listDetailedError;
    public List<DetailedSuccess> ListDetailedSuccess { get; private set; } = listDetailedSuccess;
    public T? Value { get; private set; } = value;

    public static BaseResult<T> Success(T value) => new(true, [], [], value);
    public static BaseResult<T> Failure() => new(false, [], [], default);
    public static BaseResult<T> Failure(DetailedError detailedError) => new(false, [detailedError], [], default);
}

public static class BaseResultExtension
{
    public static BaseResult<T> AddSuccess<T>(this BaseResult<T> baseResult, DetailedSuccess detailedSuccess)
    {
        baseResult.ListDetailedSuccess.Add(detailedSuccess);
        return baseResult;
    }

    public static BaseResult<T> AddSuccess<T>(this BaseResult<T> baseResult, List<DetailedSuccess> listDetailedSuccess)
    {
        baseResult.ListDetailedSuccess.AddRange(listDetailedSuccess);
        return baseResult;
    }

    public static BaseResult<T> AddError<T>(this BaseResult<T> baseResult, DetailedError detailedError)
    {
        baseResult.ListDetailedError.Add(detailedError);
        return baseResult;
    }

    public static BaseResult<T> AddError<T>(this BaseResult<T> baseResult, List<DetailedError> listDetailedError)
    {
        baseResult.ListDetailedError.AddRange(listDetailedError);
        return baseResult;
    }
}