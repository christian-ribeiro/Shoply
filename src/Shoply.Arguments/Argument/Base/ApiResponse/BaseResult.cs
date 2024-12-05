namespace Shoply.Arguments.Argument.Base;

public class BaseResult<T>
{
    public bool IsSuccess { get; }
    public DetailedError? DetailedError { get; }
    public List<DetailedError>? ListDetailedError { get; }
    public T? Value { get; }

    private BaseResult(bool isSuccess, T? value, DetailedError? detailedError, List<DetailedError>? listDetailedError)
    {
        IsSuccess = isSuccess;
        Value = value;
        DetailedError = detailedError;
        ListDetailedError = listDetailedError;
    }

    public static BaseResult<T> Success(T value) => new(true, value, null, null);
    public static BaseResult<T> Failure(DetailedError detailedError) => new(false, default, detailedError, [detailedError]);
    public static BaseResult<T> Failure(List<DetailedError> listDetailedError) => new(false, default, null, listDetailedError);
}