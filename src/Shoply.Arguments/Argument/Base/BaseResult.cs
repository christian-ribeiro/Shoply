namespace Shoply.Arguments.Argument.Base
{
    public class BaseResult<T>
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }
        public T? Value { get; }

        private BaseResult(bool isSuccess, T? value, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static BaseResult<T> Success(T value) => new(true, value, null);
        public static BaseResult<T> Failure(string errorMessage) => new(false, default, errorMessage);
    }
}