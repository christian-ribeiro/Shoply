namespace Shoply.Arguments.Argument.Base
{
    public class BaseResponseError(string errorMessage)
    {
        public string ErrorMessage { get; private set; } = errorMessage;
    }
}